import { fetchCardsFromSet } from "../utils/mtgjson.js"
import { createDatabaseClient, provisionTable } from "../utils/storage.js";

export const handler = async () => {
    const client = await createDatabaseClient();
    await provisionTable(client);
    
    const marchOfTheMachineCards = await fetchCardsFromSet("MOM");
    
    for (const card of marchOfTheMachineCards) {
        
    }
}
