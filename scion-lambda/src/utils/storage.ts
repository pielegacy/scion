import { DynamoDBClient, CreateTableCommand, CreateTableInput, DescribeTableCommand, DeleteTableCommand, BatchWriteItemCommand, BatchWriteItemCommandInput, WriteRequest, AttributeValue, } from "@aws-sdk/client-dynamodb";
import { CardData } from "../types/scion.js";
import batchArray from "./collections.js";

const BATCH_SIZE_SAVE_DEFAULT = "10"
const BATCH_SIZE_SAVE = parseInt(process.env.BATCH_SIZE_SAVE ?? BATCH_SIZE_SAVE_DEFAULT);
const TABLE_NAME_CARDS = "cards";

export const createDatabaseClient = (): DynamoDBClient => {
    if (process.env.USE_DYNAMO_LOCAL) {
        return new DynamoDBClient({
            endpoint: "http://localhost:8000",
            //TODO PROVIDER
        });
    }

    throw new Error("No DynamoDb configuration for current environment.");
}

export const provisionCardsTable = async (client: DynamoDBClient, force: boolean = false): Promise<void> => {
    const createTableDefinition: CreateTableInput = {
        TableName: TABLE_NAME_CARDS,
        AttributeDefinitions: [
            {
                AttributeName: "id",
                AttributeType: "S",
            },
            {
                AttributeName: "setCode",
                AttributeType: "S",
            },
        ],
        KeySchema: [
            {
                AttributeName: "id",
                KeyType: "HASH"
            },
            {
                AttributeName: "setCode",
                KeyType: "RANGE",
            },
        ],
        BillingMode: "PAY_PER_REQUEST",
    };

    let tableExists: boolean = false;

    try {
        if ((await client.send(new DescribeTableCommand({
            TableName: TABLE_NAME_CARDS,
        }))).Table != null) {
            tableExists = true;
        }
    } catch {
        tableExists = false;
    }

    if (tableExists) {
        if (!force) {
            console.debug(`Cards table already exists, skipping creation`);
            return;
        } else {
            await client.send(new DeleteTableCommand({
                TableName: TABLE_NAME_CARDS,
            }))
        }
    }

    await client.send(new CreateTableCommand(createTableDefinition));
}

export const insertCards = async (client: DynamoDBClient, cards: CardData[]) => {
    const batches = batchArray(cards, BATCH_SIZE_SAVE);
    for (const batch of batches) {
        const writeCommandInput: BatchWriteItemCommandInput = {
            RequestItems: {
            }
        };

        writeCommandInput.RequestItems![TABLE_NAME_CARDS] = batch.map<WriteRequest>(card => ({
            PutRequest: {
                Item: mapCardToRecord(card),
            }
        }))

        await client.send(new BatchWriteItemCommand(writeCommandInput))
    }
}

const mapCardToRecord = (card: CardData): Record<string, AttributeValue> => ({
    "id": { "S": card.id },
    "setCode": { "S": card.setCode },
    "name": { "S": card.name },
})