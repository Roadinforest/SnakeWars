import { Room, Client} from "@colyseus/core";
import { Delayed } from "@colyseus/core";
import { GameRoomState } from "./schema/GameRoomState";
import { Vector2Schema } from "./schema/Vector2Schema";
import { StaticData } from "../services/staticData";
import { DataSaver } from "../DataSave/DataSaver"
import { Schema, MapSchema, type } from "@colyseus/schema";
import { PlayerSchema } from "./schema/PlayerSchema";

export class GameRoom extends Room<GameRoomState> {
    readonly startApplesCount: number = 50;

    // 直接地图长度即可，不用/2
    readonly SingleMapSize: number = 40;
    readonly DoubleMapSize: number = 90;
    readonly MultiMapSize: number = 130;
    readonly MultiLives: number = 3;
    readonly LeftMinute: number = 5;
    dataSaver: DataSaver;

    GameType: number = -1;

    public delayedTimeout!: Delayed;//倒计时


    async onCreate(options: any) {
        console.log("Game Room created!")
        const staticData = new StaticData();
        staticData.initialize();
        this.setState(new GameRoomState(staticData));
        this.maxClients = 10;

        this.onMessage("move", (client, data) => {
            const position = new Vector2Schema(data.position.x, data.position.y);
            this.state.movePlayer(client.sessionId, position);
        });

        this.onMessage("collectApple", (client, data) => {
            this.state.collectApple(client.sessionId, data.appleId);
        })

        this.onMessage("snakeDeath", (client, data) => {
            this.state.processSnakeDeath(data.snakeId, data.positions);
        })

        if (options != null && options.type == 0) {
            this.state.setMapSize(this.SingleMapSize);
            this.GameType = 0;
            console.log("This is a single-player game!!!");
        }
        else if (options != null && options.type == 1) {
            this.state.setMapSize(this.DoubleMapSize);
            this.GameType = 1;
            console.log("This is a double-player game!!!");
        }
        else if (options == null) {
            this.state.setMapSize(this.DoubleMapSize);
            this.GameType = 1;
            console.log("This is a double-player game!!!");
        }
        else if (options != null && options.type == 2) {
            console.log("This is a multi-player game!!!");
            this.GameType = 2;
            this.state.setMapSize(this.MultiMapSize);
            this.state.setLife(this.MultiLives);//设置三条生命
            this.countDown(this.LeftMinute);//设置结束倒计时
        }

        for (var i = 0; i < this.startApplesCount; i++) {
            this.state.createAppleAtRandomPosition();
        }
        this.dataSaver = await new DataSaver();
    }

    countDown(minutes : number) {
        this.clock.start();
        // Set a timeout that will execute after 5 minutes
        this.clock.setTimeout(() => {
            this.gameOver();
        }, minutes * 60 * 1000); // 5 minutes in milliseconds

        this.clock.setInterval(() => {
            this.state.decreaseLeftTime(1000);
        }, 1000);
    }

    async gameOver() {
        console.log("Game Over");
        this.state.showResult();

        let GameTypeName;
        switch (this.GameType) {
            case 0: GameTypeName = "Single Mode" 
            break
            case 1: GameTypeName="Double Mode"
            break
            case 2: GameTypeName="Multi Mode"
                break
            default: GameTypeName="Unknown"
        }

        const _results = this.state.results;
        console.log("The size of results is "+_results.size)
        for (let entry of _results.entries()) {
            //console.log(`Key: ${entry[1].username}, Value: ${entry[1].score}`);
            this.dataSaver.saveGameRecord(GameTypeName, this.roomId, entry[1].username, entry[1].score);
        }
        //此处调用数据库写入逻辑
        this.state.endGame();
    }

    onAuth(client: Client, options: any, req: any) {
        return true;
    }

    onJoin(client: Client, options: any) {
        if (options == null) console.log("options is null");
        console.log(options);
        console.log(client.sessionId, "joined!", options.username);
        this.state.createPlayer(client.sessionId, options.username);
    }

    onLeave(client: Client, consented: boolean) {
        console.log(client.sessionId, "left!");
        this.state.removePlayer(client.sessionId);
    }

    onDispose() {
        this.gameOver();
        console.log("GameRoom", this.roomId, "disposing...");
    }
}
