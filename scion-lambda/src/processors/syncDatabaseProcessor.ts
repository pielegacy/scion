import { mapToInternalCard } from "../utils/mapping.js";
import { fetchCardsFromSet } from "../utils/mtgjson.js";
import { createDatabaseClient, insertCards, provisionCardsTable } from "../utils/storage.js";

interface SyncDatabaseProcessorOptions {
    sets: string[];
}

/**
 * Based on the provided configuration, sync MTG Sets into the scion database.
 */
export async function syncDatabaseProcessor({ sets }: SyncDatabaseProcessorOptions) {
    const client = createDatabaseClient();
    await provisionCardsTable(client, true);

    for (const setCode of sets) {
        const marchOfTheMachineCards = await fetchCardsFromSet(setCode);
        await insertCards(client, marchOfTheMachineCards.map(card => mapToInternalCard(card)));
    }
}