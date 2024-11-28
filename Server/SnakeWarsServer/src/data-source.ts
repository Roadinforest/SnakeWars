import "reflect-metadata"
import { DataSource } from "typeorm"
import { User } from "./entity/User"
import { GameUser } from "./entity/GameUser"
import { GameRecord } from "./entity/GameRecord"


export const AppDataSource = new DataSource({
    type: "mysql",
    host: "localhost",
    port: 3306,
    username: "visitor",
    password: "123456",
    database: "test",
    synchronize: true,
    logging: false,
    entities: [User,GameRecord,GameUser],
    migrations: [],
    subscribers: [],
})
