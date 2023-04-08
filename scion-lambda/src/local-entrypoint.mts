import { syncDatabaseProcessor } from "./processors/syncDatabaseProcessor.js";

await syncDatabaseProcessor({
    sets: [
        "BRO",
        "MOM"
    ]
})