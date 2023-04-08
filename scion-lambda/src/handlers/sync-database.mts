import { mapToInternalCard } from "../utils/mapping.js";
import { fetchCardsFromSet } from "../utils/mtgjson.js"
import { createDatabaseClient, provisionCardsTable, insertCards } from "../utils/storage.js";
import { ScheduledEvent, Context } from "aws-lambda";

interface SyncDatabaseHandlerDetails {
    sets: string[];
}

export const handler = async (event: ScheduledEvent<SyncDatabaseHandlerDetails>, _context: Context | null) => {
    const client = createDatabaseClient();
    await provisionCardsTable(client, true);
    
    const marchOfTheMachineCards = await fetchCardsFromSet("MOM");
    await insertCards(client, marchOfTheMachineCards.map(card => mapToInternalCard(card)));
}
