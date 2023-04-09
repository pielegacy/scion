import { CardSet, ResponseContainer, Set, SetList } from "../types/mtgjson/index.js";

const formatUrl = (path: string): string => `https://mtgjson.com/api/v5${path}`;

const isSetCode = (code: string) => code.length === 3;

export async function fetchSetLists(): Promise<SetList[]> {
    try {
        const response = await fetch(formatUrl(`/SetList.json`), {
            method: "GET",
        });
        const body = await response.json() as ResponseContainer<SetList[]>;
        return body.data;
    } catch (ex) {
        console.error({
            message: `Failed to retrieve setlists`,
            ex,
        })
        throw ex;
    }
}

export async function fetchCardsFromSet(code: string): Promise<CardSet[]> {
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
            ex,
        });

        throw ex;
    }
}