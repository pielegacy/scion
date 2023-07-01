import { test, expect } from '@playwright/test';

test('Check Games Portal Pricing', async ({ page }) => {
    const cardName = process.env.CARD_NAME;
    if (cardName == null) {
        throw new Error("No card name provided");
    }

    const cardNameEncoded = encodeURIComponent(cardName)
    await page.goto(`https://gamesportal.com.au/search?type=product&options%5Bprefix%5D=last&q=${cardNameEncoded}`);

    let hasNextPage = true;
    let prices: number[] = [];

    // while (hasNextPage) {
    const pagePrice = await page.$$eval('.product-price__price', prices => {
        let lowestPrice: number | null = null;
        for (const price of prices) {
            const priceText = (price as HTMLElement).innerText;
            const possiblePrice = parseFloat(priceText.replace('$', ''));
            if (!isNaN(possiblePrice) && (lowestPrice == null || lowestPrice > possiblePrice)) {
                lowestPrice = possiblePrice;
            }
        }
        return lowestPrice;
    });
    if (pagePrice) {
        prices.push(pagePrice);
    }
    hasNextPage = await page.$$eval(".pag_next a", nextPageButtons => {
        return nextPageButtons[0] != null;
    });

    if (hasNextPage) {
        await page
            .locator(".pag_next a")
            .click();
        await page.waitForTimeout(1000);
    }
    // }

    const result = prices.reduce((lowest, current) => {
        if (lowest === -1 || lowest > current) {
            return current;
        }
        return lowest;
    }, -1)

    console.log(`Card Name: ${cardName}`);
    console.log(`Detected Lowest Price: ${result}`)
});