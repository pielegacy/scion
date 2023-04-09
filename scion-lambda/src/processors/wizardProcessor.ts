import { DynamoDBClient } from "@aws-sdk/client-dynamodb";
import { mapToInternalCard } from "../utils/mapping.js";
import { fetchCardsFromSet } from "../utils/mtgjson.js";
import { createDatabaseClient, insertCards, provisionCardsTable } from "../utils/storage.js";

interface WizardProcessorOptions {
    client: DynamoDBClient;
    setCode: string;
}

/**
 * Based on the provided configuration, sync MTG Sets into the scion database.
 */
export default async function wizardProcessor({ setCode, client }: WizardProcessorOptions) {
    await provisionCardsTable(client, true);
    const setCards = await fetchCardsFromSet(setCode);
    await insertCards(client, setCards.map(card => mapToInternalCard(card)));
}