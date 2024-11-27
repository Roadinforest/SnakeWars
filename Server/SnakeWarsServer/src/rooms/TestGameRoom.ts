import { Room, Client } from "@colyseus/core";
import { Delayed } from "@colyseus/core";
import { GameRoomState } from "./schema/GameRoomState";
import { Vector2Schema } from "./schema/Vector2Schema";
import { StaticData } from "../services/staticData";
import { matchMaker } from "@colyseus/core";
import { GameRoom } from "./GameRoom";

export class TestGameRoom extends Room<GameRoomState> {

	private static DoublewaitingClients: Map<string, {
		client: Client,
		options: any
	} > = new Map(); // �ȴ�����

	private static MultiwaitingClients: Map<string, [Client, any]> = new Map(); // �ȴ�����


	onCreate(options: any) {
		console.log("TestGame Room created!");

        const staticData = new StaticData();
        staticData.initialize();
        this.setState(new GameRoomState(staticData));

		//this.onMessage("move", (client, data) => {
		//    const position = new Vector2Schema(data.position.x, data.position.y);
		//    this.state.movePlayer(client.sessionId, position);
		//});

		//this.onMessage("collectApple", (client, data) => {
		//    this.state.collectApple(client.sessionId, data.appleId);
		//});

		//this.onMessage("snakeDeath", (client, data) => {
		//    this.state.processSnakeDeath(data.snakeId, data.positions);
		//});

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
			if (TestGameRoom.MultiwaitingClients.size >= 2) {
				// ȡ���ȴ��е�ǰ�����ͻ���
				const iterator = TestGameRoom.MultiwaitingClients.entries();
				const [waitingClient1Id, waitingClient1Options] = iterator.next().value;
				const [waitingClient2Id, waitingClient2Options] = iterator.next().value;

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

				// �ֶ��������ͻ��˼���ͬһ������
				room.onJoin(waitingClient1Options.client, waitingClient1Options.options);
				room.onJoin(waitingClient2Options.client, waitingClient2Options.options);
				room.onJoin(client, options);

				console.log(`Multi-player room created: ${room.roomId}`);
			} else {
				// ����ȴ��������㣬�򽫵�ǰ��Ҽ���ȴ�����
				TestGameRoom.MultiwaitingClients.set(client.sessionId, [ client, options ]);
				console.log(`${client.sessionId} is now waiting for multi-player pairing.`);
			}
		}

		else {
			//this.state.createPlayer(client.sessionId, options.username);
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
