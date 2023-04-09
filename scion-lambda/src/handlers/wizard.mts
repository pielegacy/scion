import { mapToInternalCard } from "../utils/mapping.js";
import { fetchCardsFromSet } from "../utils/mtgjson.js"
import { createDatabaseClient, insertCards } from "../utils/storage.js";
import { ScheduledEvent, Context } from "aws-lambda";
import processor from "../processors/wizardProcessor.js";

interface WizardHandlerDetails {
    setCode: string;
}

const client = createDatabaseClient();

export async function handler(event: ScheduledEvent<WizardHandlerDetails>, _context: Context | null) {
    const { setCode } = event.detail;
    
    await processor({
        client,
        setCode,
    });
}
