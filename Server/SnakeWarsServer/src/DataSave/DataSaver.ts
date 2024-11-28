import { AppDataSource } from "../data-source";
import { GameUser } from "../entity/GameUser";
import { GameRecord } from "../entity/GameRecord";
import { EntityManager } from "typeorm";

export class DataSaver {
    private manager: EntityManager | null=null;
    private isCreate: boolean=false;

    constructor() {
        AppDataSource.initialize().then(manager => {
            this.manager = manager.manager;
            this.isCreate=true;
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

    async saveGameUser(gameUser: GameUser): Promise<GameUser> {
        await this.waitForInitialization();
        if (!this.manager) {
            throw new Error("DataSaver is not initialized yet.");
        }
        return this.manager.save(gameUser);
    }

    async saveGameRecord(gameRecord: GameRecord): Promise<GameRecord> {
        await this.waitForInitialization();
        if (!this.manager) {
            throw new Error("DataSaver is not initialized yet.");
        }
        return this.manager.save(gameRecord);
    }
}