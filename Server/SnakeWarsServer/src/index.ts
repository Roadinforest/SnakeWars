/**
 * IMPORTANT:
 * ---------
 * Do not manually edit this file if you'd like to host your server on Colyseus Cloud
 *
 * If you're self-hosting (without Colyseus Cloud), you can manually
 * instantiate a Colyseus Server as documented here:
 *
 * See: https://docs.colyseus.io/server/api/#constructor-options
 */
import { listen } from "@colyseus/tools";

// Import Colyseus config
import app from "./app.config";

// Create and listen on 2567 (or PORT environment variable.)
listen(app);

/* test For TypeOrm */
//import { AppDataSource } from "./data-source"
//import { GameUser } from "./entity/GameUser"
//import { User } from "./entity/User"
//import { GameRecord } from "./entity/GameRecord"
//import { DataSaver } from "./DataSave/DataSaver"

//// Ê¹ÓÃÊ¾Àý
//const dataSaver = new DataSaver();

//dataSaver.saveGameUser(new GameUser({
//    name: "Timber",
//    password: "12345678910"
//})).then(savedGameUser => {
//    console.log("Saved game user:", savedGameUser);
//}).catch(error => {
//    console.error("Error saving game user:", error);
//});

//dataSaver.saveGameRecord(new GameRecord({
//    game_id: "test_id_4",
//    game_mode: "Single",
//    score: 100,
//    time: new Date(),
//    user_name: "Timber"
//})).then(savedGameRecord => {
//    console.log("Saved game record:", savedGameRecord);
//}).catch(error => {
//    console.error("Error saving game record:", error);
//});
