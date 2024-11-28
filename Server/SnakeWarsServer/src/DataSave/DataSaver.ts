import { AppDataSource } from "../data-source";
import { GameUser } from "../entity/GameUser";
import { GameRecord } from "../entity/GameRecord";
import { EntityManager } from "typeorm";

export class DataSaver {
    private manager: EntityManager | null = null;
    private isCreate: boolean = false;

    constructor() {
        if (AppDataSource.isInitialized) {
            this.manager = AppDataSource.createEntityManager();
            this.isCreate = true;
            return;
        }
        AppDataSource.initialize().then(manager => {
            this.manager = manager.manager;
            this.isCreate = true;
            console.log("DataSaver initialized successfully.");
        }).catch(error => {
            console.error("Error initializing DataSaver:", error);
        });
    }


    async waitForInitialization(): Promise<void> {
        while (!this.isCreate) {
            await new Promise(resolve => setTimeout(resolve, 100));
        }
    }

    //    async saveGameUser(gameUser: GameUser): Promise<GameUser> {
    //        await this.waitForInitialization();
    //        if (!this.manager) {
    //            throw new Error("DataSaver is not initialized yet.");
    //        }
    //        return this.manager.save(gameUser);
    //    }

    //    async saveGameRecord(gameRecord: GameRecord): Promise<GameRecord> {
    //        await this.waitForInitialization();
    //        if (!this.manager) {
    //            throw new Error("DataSaver is not initialized yet.");
    //        }
    //        return this.manager.save(gameRecord);
    //    }


    async saveGameRecord(gameType: string, gameId: string, userName: string, score: number): Promise<GameRecord> {
        await this.waitForInitialization();
        if (!this.manager) {
            throw new Error("DataSaver is not initialized yet.");
        }

        const gameRecord = new GameRecord();
        gameRecord.score = score;
        gameRecord.user_name = userName;
        gameRecord.game_id = gameId;
        gameRecord.game_mode = gameType;
        gameRecord.time = new Date();

        //AppDataSource.destroy();//关闭连接
        console.log("Save one record");
        return this.manager.save(gameRecord);
    }

    async saveGameUser(userName: string, passWard: string): Promise<GameUser> {
        await this.waitForInitialization();
        if (!this.manager) {
            throw new Error("DataSaver is not initialized yet.");
        }

        const gameUser = new GameUser();
        gameUser.name = userName;
        gameUser.password = passWard;

        console.log("Save one game user");
        //AppDataSource.destroy();//关闭连接
        return this.manager.save(gameUser);
    }


}