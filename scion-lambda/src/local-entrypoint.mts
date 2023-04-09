import processor from "./processors/wizardProcessor.js";
import { createDatabaseClient } from "./utils/storage.js";

const client = createDatabaseClient();

await processor({
    client,
    setCode: "BRO",
})