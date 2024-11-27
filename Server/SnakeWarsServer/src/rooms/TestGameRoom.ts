import { Room, Client } from "@colyseus/core";
import { GameRoomState } from "./schema/GameRoomState";
import { StaticData } from "../services/staticData";
import { matchMaker } from "@colyseus/core";
import { GameRoom } from "./GameRoom";
import { RoomListingData } from "colyseus";

export class TestGameRoom extends Room<GameRoomState> {

	private static DoublewaitingClients: Map<string, {
		client: Client,
		options: any
	} > = new Map(); // �ȴ�����

	private static MultiwaitingClients: Map<string, {
		client: Client,
		options: any
	} > = new Map(); // ���˵ȴ�����

	private _MultiRoom : RoomListingData<any>;//���˷�Ψһ��ʶ


	onCreate(options: any) {
		console.log("TestGame Room created!");

		// ����һ��GameRoomState
        const staticData = new StaticData();
        staticData.initialize();
        this.setState(new GameRoomState(staticData));
	}

	onAuth(client: Client, options: any, req: any) {
		return true;
	}

	async onJoin(client: Client, options: any) {
		console.log(client.sessionId, "join TestRoom!", options.username);

		// ˫��ģʽƥ���߼�
		if (options.type === 1) {
			if (TestGameRoom.DoublewaitingClients.size ==1) {
				// ����еȴ��еĿͻ��ˣ�ȡ����һ��
				const [waitingClientId, waitingClientOptions] = TestGameRoom.DoublewaitingClients.entries().next().value;
				TestGameRoom.DoublewaitingClients.delete(waitingClientId);

				console.log(`Pairing ${waitingClientId} with ${client.sessionId}`);
				if (waitingClientOptions.client == null) console.log("Waiting Client is null");
				console.log(`Logs ${waitingClientOptions.options.type} `);

				// ��������
				const room = await matchMaker.createRoom("GameRoom", {
					type: 1,
					player1: waitingClientId,
					player2: client.sessionId,
				});

				const reservation1 = await matchMaker.reserveSeatFor(room, waitingClientOptions.options);
				const reservation2 = await matchMaker.reserveSeatFor(room, options);

				console.log(`Double Room created: ${room.roomId}`);

				// �� seat reservations ���͸��ͻ���
				waitingClientOptions.client.send("seatReservation", reservation1);
				client.send("seatReservation", reservation2);

				console.log(`Double-player room created: ${room.roomId}`);
			}
			else {
				// ���û�еȴ��Ŀͻ��ˣ�����ǰ�ͻ��˼���ȴ�����
				TestGameRoom.DoublewaitingClients.set(client.sessionId, { client, options });
				console.log(`${client.sessionId} is now waiting.`);
			}
		}

		// ����ģʽƥ���߼�
		else if (options.type === 2) {
			//����Ѿ��з��䣬�����
			if (this._MultiRoom != null)
			{
				const reservation = await matchMaker.reserveSeatFor(this._MultiRoom, options);
				client.send("seatReservation", reservation);
			}

			//û�з��䣬��ȴ��˵������ٴ���
			if (TestGameRoom.MultiwaitingClients.size >= 2) {
				// ȡ���ȴ��е�ǰ�����ͻ���
				const iterator = TestGameRoom.MultiwaitingClients.entries();
				const [waitingClient1Id, waitingClient1] = iterator.next().value;
				const [waitingClient2Id, waitingClient2] = iterator.next().value;

				// �ӵȴ��������Ƴ�
				TestGameRoom.MultiwaitingClients.delete(waitingClient1Id);
				TestGameRoom.MultiwaitingClients.delete(waitingClient2Id);

				console.log(`Pairing ${waitingClient1Id}, ${waitingClient2Id}, and ${client.sessionId}`);

				// ��������
				const room = await matchMaker.createRoom("GameRoom", {
					type: 2,
					player1: waitingClient1Id,
					player2: waitingClient2Id,
					player3: client.sessionId,
				});

				const reservation1 = await matchMaker.reserveSeatFor(room, waitingClient1.options);
				const reservation2 = await matchMaker.reserveSeatFor(room, waitingClient2.options);
				const reservation3 = await matchMaker.reserveSeatFor(room, options);


				// �� seat reservations ���͸��ͻ���
				waitingClient1.client.send("seatReservation", reservation1);
				waitingClient2.client.send("seatReservation", reservation2);
				client.send("seatReservation", reservation3);

				console.log(`Multi-player room created: ${room.roomId}`);
			} else {
				// ����ȴ��������㣬�򽫵�ǰ��Ҽ���ȴ�����
				TestGameRoom.MultiwaitingClients.set(client.sessionId, { client, options });
				console.log(`${client.sessionId} is now waiting for multi-player pairing.`);
			}
		}

		else {
			console.log(client.sessionId+" Enter with error type");
		}
	}

	onLeave(client: Client, consented: boolean) {
		console.log(client.sessionId, "left in TestGameRoom!");

		// ����ͻ����ڵȴ������У��Ƴ�
		if (TestGameRoom.DoublewaitingClients.has(client.sessionId)) {
			TestGameRoom.DoublewaitingClients.delete(client.sessionId);
			console.log(`${client.sessionId} removed from waiting queue.`);
		}
		else if (TestGameRoom.MultiwaitingClients.has(client.sessionId)) {
			TestGameRoom.MultiwaitingClients.delete(client.sessionId);
			console.log(`${client.sessionId} removed from waiting queue.`);
		}
		//this.state.removePlayer(client.sessionId);
	}

	onDispose() {
		console.log("TestGameRoom", this.roomId, "disposing...");
	}
}
