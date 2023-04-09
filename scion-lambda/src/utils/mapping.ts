import { CardSet } from "../types/mtgjson/index.js";
import { CardData } from "../types/scion.js";

export const mapToInternalCard = (card: CardSet): CardData => ({
    id: card.identifiers.mtgjsonV4Id!,
    name: card.name,
    setCode: card.setCode, 
})