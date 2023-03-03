import { CardSet, ResponseContainer, Set } from "../types/mtgjson.js";

const formatUrl = (path: string): string => `https://mtgjson.com/api/v5${path}`;

const isSetCode = (code: string) => code.length === 3;

export const fetchCardsFromSet = async (code: string): Promise<CardSet[]> => {
    try {
        if (!isSetCode(code)) {
            throw new Error(`Invalid set code '${code}'`);
        }
        
        const response = await fetch(formatUrl(`/${code}.json`), {
            method: "GET",
        });

        const body = await response.json() as ResponseContainer<Set>;
        return body.data.cards;
    } catch (ex) {
        console.error({
            message: `Failed to retrieve cards for '${code}'`,
            ex
        })

        throw ex;
    }
}