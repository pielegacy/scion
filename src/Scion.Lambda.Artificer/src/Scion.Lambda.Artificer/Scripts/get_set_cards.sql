WITH CARDS AS (
	SELECT DATA::JSONB -> 'data' -> 'cards' "data" 
	FROM external_data.sets
	WHERE code = 'MOM'
	LIMIT 1
)
SELECT 
	card."identifiers"::JSONB ->> 'mtgjsonV4Id' id,
	card."identifiers"::JSONB ->> 'multiverseId' multiverse_id,
	card."name" name,
	card."setCode" set_code,
	card."rarity" rarity,
	card."colorIdentity" color_identity,
	card."manaValue" mana_value,
	card."manaCost" mana_cost,
	card."type" type,
	card."text" text
FROM cards, JSONB_TO_RECORDSET(cards.data) 
AS CARD(
	"name" TEXT, 
	"colorIdentity" TEXT[], 
	"manaValue" DECIMAL,
	"manaCost" TEXT,
	"type" TEXT,
	"identifiers" JSONB,
	"text" TEXT,
	"rarity" TEXT,
	"setCode" TEXT
)
ORDER BY name
LIMIT 50;