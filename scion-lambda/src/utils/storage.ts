import { DynamoDBClient, CreateTableCommand,  } from "@aws-sdk/client-dynamodb";

export const createDatabaseClient = (): DynamoDBClient => {
    if (process.env.USE_DYNAMO_LOCAL) {
        return new DynamoDBClient({
            endpoint: "http://localhost:8000",
            //TODO PROVIDER
        });
    }

    throw new Error("No DynamoDb configuration for current environment.");
}

export const provisionTable = async (client: DynamoDBClient): Promise<void> => {
    await client.send(new CreateTableCommand({
        TableName: "cards",
        KeySchema: [
            {
                AttributeName: "id",
                KeyType: "string"
            }
        ],
        AttributeDefinitions: [],
    }));
}