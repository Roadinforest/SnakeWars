import { Schema, MapSchema, type } from "@colyseus/schema";
import { PlayerSchema } from "./PlayerSchema";
import { Vector2Schema } from "./Vector2Schema";
import { StaticData } from "../../services/staticData";
import { AppleSchema } from "./AppleSchema";
import { LefttimeSchema } from "./LefttimeSchema";

export class GameRoomState extends Schema {
    mapSize: number = 140;//地图大小
    readonly scorePerApple: number = 1;
    readonly maxApplesOnRoom: number = 150;

    @type({ map: PlayerSchema }) players = new MapSchema<PlayerSchema>();
    //lives = new Map<string,number>();
    @type({ map: PlayerSchema }) results= new MapSchema<PlayerSchema>();//结束时记录玩家数据
    @type({ map: AppleSchema}) apples = new MapSchema<AppleSchema>();
    @type(LefttimeSchema) leftTime = new LefttimeSchema(5*60*1000);

    staticData: StaticData;
    lastAppleId: number = 0;
    processedDeaths: Set<string>;
    //leftTime : number = 5*60*1000;//如果是多人模式，就会有倒计时为5min
    gameState : boolean = true;//游戏模式，true为正常模式，false为结束模式
    lifePerPlayer: number = 1;//游戏模式，true为正常模式，false为结束模式

    constructor(staticData: StaticData) {
        super();
        this.staticData = staticData;
        this.processedDeaths = new Set<string>();
    }

    // 结束游戏
    endGame() {
        this.gameState = false;
        //后面追加addResult的逻辑
    }

    setLeftTime(time : number) {
        this.leftTime.leftTime=time;
    }

    decreaseLeftTime(deltaTime : number) {
        this.leftTime.leftTime-=deltaTime;
        //console.log("leftTime:",this.leftTime.leftTime);
    }


    setMapSize(size: number) {
        this.mapSize = size;
    }

    // 存储对局结果，为后面存数据做准备
    addResult(result: PlayerSchema) {
        this.results.set(result.username, result);
    }

    // 设置每个玩家的生命条数
    setLife(lives: number) {
        this.lifePerPlayer = lives;
    }

    createAppleAtRandomPosition() : AppleSchema {
        return this.createApple(this.getSpawnPoint(this.mapSize));
    }

    createApple(position: Vector2Schema) : AppleSchema {
        const data = new AppleSchema(position);
        this.apples.set(String(this.lastAppleId), data);
        this.lastAppleId++;
        return data;
    }

    collectApple(sessionId: string, appleId: string) {
        const player = this.players.get(sessionId);
        const apple = this.apples.get(appleId);
        player.addScore(this.scorePerApple);
        
        if (this.apples.size > this.maxApplesOnRoom) {
            this.apples.delete(appleId);
        } else {
            apple.position = this.getSpawnPoint(this.mapSize);
        }
    }

    createPlayer(sessionId: string, username: string): PlayerSchema {
        const player = new PlayerSchema(username, this.getSpawnPoint(this.mapSize), this.getRandomSkinId(), 1);
        player.setLife(this.lifePerPlayer);
        this.players.set(sessionId, player);
        return player;
    }

    removePlayer(sessionId: string) {
            const player = this.players.get(sessionId);
            

        // 玩家死亡,进行记录
        if (player.lives == 1) {
            this.results.set(player.username, player);
            if (this.players.has(sessionId)) {
                this.players.delete(sessionId);
            }
            return;
        }
        else {
            player.lives--;
            this.createPlayer(sessionId, player.username);
        }
    }

    processSnakeDeath(snakeId: string, positions: any) {
        if (this.processedDeaths.has(snakeId))
            return;

        this.removePlayer(snakeId);
        this.processedDeaths.add(snakeId);
        this.removeIdFromProcessedDeathAfterDelay(snakeId, 10_000);

        for (var i = 0; i < positions.length; i++) {
            const worldPosition = new Vector2Schema(positions[i].x, positions[i].y);
            this.createApple(worldPosition);
        }
    }

    async removeIdFromProcessedDeathAfterDelay(snakeId: string, delayInMilliseconds: number) {
        await new Promise(resolve => setTimeout(resolve, delayInMilliseconds));
        this.processedDeaths.delete(snakeId);
    }

    getRandomSkinId(): number {
        return Math.floor(Math.random() * this.staticData.getAvailableSkinCount());
    }

    getSpawnPoint(size: number): Vector2Schema {
        const x = Math.floor(Math.random() * size) - size / 2;
        const y = Math.floor(Math.random() * size) - size / 2;
        return new Vector2Schema(x, y);
    }

    showResult() {
        for (let entry of this.results.entries()) {
            console.log(`Key: ${entry[1].username}, Value: ${entry[1].score}`);
        }
    }

    movePlayer(sessionId: string, targetPosition: Vector2Schema) {
        const player = this.players.get(sessionId);
        player.position = targetPosition;
    }
}