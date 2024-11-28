import { Entity, PrimaryGeneratedColumn, Column } from "typeorm"
import "reflect-metadata"

@Entity()
export class GameUser {

    @PrimaryGeneratedColumn({type:'int'})
    id: number

    @Column({ type: 'varchar', length: 255 })
    name: string

    @Column({ type: 'varchar', length: 255 })
    password: string

    constructor(gameUser?: Partial<GameUser>) {
        if (gameUser) {
            Object.assign(this, gameUser);
        }
    }
}
