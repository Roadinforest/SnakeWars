import { Room, Client } from "@colyseus/core";
import { Delayed } from "@colyseus/core";
import { GameRoomState } from "./schema/GameRoomState";
import { Vector2Schema } from "./schema/Vector2Schema";
import { StaticData } from "../services/staticData";

export class GameRoom extends Room<GameRoomState> {
    readonly startApplesCount: number = 50;

    // 直接地图长度即可，不用/2
    readonly DoubleMapSize: number = 90;
    readonly MultiMapSize: number = 130;
    readonly MultiLives: number = 3;
    readonly LeftMinute: number = 5;

    public delayedTimeout!: Delayed;//倒计时


    onCreate(options: any) {
        console.log("Game Room created!")
        const staticData = new StaticData();
        staticData.initialize();

        this.setState(new GameRoomState(staticData));

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

        //双人模式
        if (options != null && options.type == 1) {
            this.state.setMapSize(this.DoubleMapSize);
            console.log("This is a double-player game!!!");
        }
        //多人模式
        else if (options != null && options.type == 2) {
            console.log("This is a multi-player game!!!");
            this.state.setMapSize(this.MultiMapSize);
            this.state.setLife(this.MultiLives);//设置三条生命
            this.countDown(this.LeftMinute);//设置结束倒计时
        }

        for (var i = 0; i < this.startApplesCount; i++) {
            this.state.createAppleAtRandomPosition();
        }
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

    gameOver() {
        console.log("Game Over");
        this.state.endGame();
    }

    onAuth(client: Client, options: any, req: any) {
        return true;
    }

    onJoin(client: Client, options: any) {
        console.log(client.sessionId, "joined!", options.username);
        this.state.createPlayer(client.sessionId, options.username);
    }

    onLeave(client: Client, consented: boolean) {
        console.log(client.sessionId, "left!");
        this.state.removePlayer(client.sessionId);
    }

    onDispose() {
        console.log("room", this.roomId, "disposing...");
    }
}
