import { Schema, type } from "@colyseus/schema";

export class LefttimeSchema extends Schema {
    @type("number") leftTime : number;

    constructor(_leftTime : number) {
        super();
        this.leftTime = _leftTime;
    }
}
