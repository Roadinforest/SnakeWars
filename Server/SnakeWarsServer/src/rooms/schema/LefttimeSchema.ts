import { Schema, type } from "@colyseus/schema";

export class LefttimeSchema extends Schema {
    leftTime : number;

    constructor(_leftTime : number) {
        super();
        this.leftTime = _leftTime;
    }
}
