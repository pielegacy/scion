import { DynamoDBClient, CreateTableCommand } from "@aws-sdk/client-dynamodb";

export const createClient = (): DynamoDBClient => {
    if (process.env.USE_DYNAMO_LOCAL) {
        return new DynamoDBClient({
            endpoint: "http://localhost:8000"
        });
    }

    throw new Error("No DynamoDb configuration for current environment.");
}

export const provisionTable = (client: DynamoDBClient) => {
    client.send(new CreateTableCommand({
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