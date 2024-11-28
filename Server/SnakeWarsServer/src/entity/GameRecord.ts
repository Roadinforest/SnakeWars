import { Entity, PrimaryGeneratedColumn, Column } from "typeorm"
import "reflect-metadata"

@Entity()
export class GameRecord{

    @PrimaryGeneratedColumn({type:'int'})
    id: number

    @Column({ type: 'varchar', length: 255 })
    user_name: string

    @Column({type:'int'})
    score: number

    @Column({ type: 'varchar', length: 255 })
    game_mode: string

    @Column({ type: 'varchar', length: 255 })
    game_id: string 

    @Column({ type: 'timestamp' })
    time: Date

    constructor(gameRecord?: Partial<GameRecord>) {
        if (gameRecord) {
            Object.assign(this, gameRecord);
        }
    }
}
