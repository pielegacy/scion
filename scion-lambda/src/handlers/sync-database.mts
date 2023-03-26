import { mapToInternalCard } from "../utils/mapping.js";
import { fetchCardsFromSet } from "../utils/mtgjson.js"
import { createDatabaseClient, provisionCardsTable, insertCards } from "../utils/storage.js";

export const handler = async () => {
    const client = createDatabaseClient();
    await provisionCardsTable(client, true);
    
    const marchOfTheMachineCards = await fetchCardsFromSet("MOM");
    await insertCards(client, marchOfTheMachineCards.map(card => mapToInternalCard(card)));
}
