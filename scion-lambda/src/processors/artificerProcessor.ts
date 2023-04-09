import { DynamoDBClient } from "@aws-sdk/client-dynamodb";
import { provisionCardsTable } from "../utils/storage.js";

interface ArtificerProcessorOptions {
    client: DynamoDBClient;
    force?: boolean;
}

export default async function artificerProcessor({ client, force } : ArtificerProcessorOptions) {
    await provisionCardsTable(client, force);
}