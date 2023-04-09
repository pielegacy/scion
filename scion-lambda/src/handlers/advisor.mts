import processor from "../processors/advisorProcessor.js";

export default async function handler() {
    await processor();
}