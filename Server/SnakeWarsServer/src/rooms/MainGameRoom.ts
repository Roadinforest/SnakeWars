import { Room, Client } from "@colyseus/core";
import { GameRoomState } from "./schema/GameRoomState";
import { StaticData } from "../services/staticData";
import { matchMaker } from "@colyseus/core";
import { GameRoom } from "./GameRoom";
import { RoomListingData } from "colyseus";

export class MainGameRoom extends Room<GameRoomState> {

	private static DoublewaitingClients: Map<string, {
		client: Client,
		options: any
	} > = new Map(); 

	private static MultiwaitingClients: Map<string, {
		client: Client,
		options: any
	} > = new Map();

	private _MultiRoom : RoomListingData<any>;//多人房唯一标识


	onCreate(options: any) {
		console.log("MainGameRoomcreated!");

        const staticData = new StaticData();
        staticData.initialize();
        this.setState(new GameRoomState(staticData));
	}

	onAuth(client: Client, options: any, req: any) {
		return true;
	}

	async onJoin(client: Client, options: any) {
		console.log(client.sessionId, "join MainGameRoom!", options.username);

		// 单人模式逻辑
		if (options.type === 0) {
				const room = await matchMaker.createRoom("GameRoom", {
					type: 0,
					player1: client,
				});
				const reservation = await matchMaker.reserveSeatFor(room, options);
				console.log(`Single Room created: ${room.roomId}`);

				// 将 seat reservations 发送给客户端
				client.send("seatReservation", reservation);

				console.log(`Single-player room created: ${room.roomId}`);
		}
		// 双人模式匹配逻辑
		else if (options.type === 1) {
			if (MainGameRoom.DoublewaitingClients.size ==1) {
				// 如果有等待中的客户端，取出第一个
				const [waitingClientId, waitingClientOptions] = MainGameRoom.DoublewaitingClients.entries().next().value;
				MainGameRoom.DoublewaitingClients.delete(waitingClientId);

				console.log(`Pairing ${waitingClientId} with ${client.sessionId}`);
				if (waitingClientOptions.client == null) console.log("Waiting Client is null");
				console.log(`Logs ${waitingClientOptions.options.type} `);

				// 创建房间
				const room = await matchMaker.createRoom("GameRoom", {
					type: 1,
					player1: waitingClientId,
					player2: client.sessionId,
				});

				const reservation1 = await matchMaker.reserveSeatFor(room, waitingClientOptions.options);
				const reservation2 = await matchMaker.reserveSeatFor(room, options);

				console.log(`Double Room created: ${room.roomId}`);

				// 将 seat reservations 发送给客户端
				waitingClientOptions.client.send("seatReservation", reservation1);
				client.send("seatReservation", reservation2);

				console.log(`Double-player room created: ${room.roomId}`);
			}
			else {
				// 如果没有等待的客户端，将当前客户端加入等待队列
				MainGameRoom.DoublewaitingClients.set(client.sessionId, { client, options });
				console.log(`${client.sessionId} is now waiting.`);
			}
		}

		// 多人模式匹配逻辑
		else if (options.type === 2) {
			//如果已经有房间，则加入
			if (this._MultiRoom != null)
			{
				const reservation = await matchMaker.reserveSeatFor(this._MultiRoom, options);
				client.send("seatReservation", reservation);
			}

			//没有房间，则等待人到齐了再创建
			if (MainGameRoom.MultiwaitingClients.size >= 2) {
				// 取出等待中的前两个客户端
				const iterator = MainGameRoom.MultiwaitingClients.entries();
				const [waitingClient1Id, waitingClient1] = iterator.next().value;
				const [waitingClient2Id, waitingClient2] = iterator.next().value;

				// 从等待队列中移除
			 MainGameRoom.MultiwaitingClients.delete(waitingClient1Id);
			 MainGameRoom.MultiwaitingClients.delete(waitingClient2Id);

				console.log(`Pairing ${waitingClient1Id}, ${waitingClient2Id}, and ${client.sessionId}`);

				// 创建房间
				const room = await matchMaker.createRoom("GameRoom", {
					type: 2,
					player1: waitingClient1Id,
					player2: waitingClient2Id,
					player3: client.sessionId,
				});

				const reservation1 = await matchMaker.reserveSeatFor(room, waitingClient1.options);
				const reservation2 = await matchMaker.reserveSeatFor(room, waitingClient2.options);
				const reservation3 = await matchMaker.reserveSeatFor(room, options);


				// 将 seat reservations 发送给客户端
				waitingClient1.client.send("seatReservation", reservation1);
				waitingClient2.client.send("seatReservation", reservation2);
				client.send("seatReservation", reservation3);

				console.log(`Multi-player room created: ${room.roomId}`);
			} else {
				// 如果等待人数不足，则将当前玩家加入等待队列
			 MainGameRoom.MultiwaitingClients.set(client.sessionId, { client, options });
				console.log(`${client.sessionId} is now waiting for multi-player pairing.`);
			}
		}

		else {
			console.log(client.sessionId+" Enter with error type");
		}
	}

	onLeave(client: Client, consented: boolean) {
		console.log(client.sessionId, "left in MainGameRoom!");

		// 如果客户端在等待队列中，移除
		if ( MainGameRoom.DoublewaitingClients.has(client.sessionId)) {
		 MainGameRoom.DoublewaitingClients.delete(client.sessionId);
			console.log(`${client.sessionId} removed from waiting queue.`);
		}
		else if (MainGameRoom.MultiwaitingClients.has(client.sessionId)) {
			MainGameRoom.MultiwaitingClients.delete(client.sessionId);
			console.log(`${client.sessionId} removed from waiting queue.`);
		}
	}

	onDispose() {
		console.log("MainGameRoom", this.roomId, "disposing...");
	}
}
