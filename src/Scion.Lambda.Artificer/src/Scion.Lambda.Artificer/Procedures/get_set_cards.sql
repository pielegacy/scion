WITH CARDS AS (
	SELECT DATA::JSONB -> 'data' -> 'cards' "data" 
	FROM external_data.sets
	WHERE code = 'AER'
	LIMIT 1
)
SELECT 
	card."identifiers"::JSONB ->> 'mtgjsonV4Id' id,
	card."identifiers"::JSONB ->> 'multiverseId' multiverse_id,
	card."name" name,
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
	"text" TEXT
)
ORDER BY name;