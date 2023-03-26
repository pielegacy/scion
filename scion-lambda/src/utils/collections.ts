export default function batchArray<TItem>(source: TItem[], batchSize: number): TItem[][] {
    const result: TItem[][] = [];
    let currentBatch = [];

    for (const item of source) {
        if (currentBatch.push(item) === batchSize) {
            result.push(currentBatch);
            currentBatch = [];
        }
    }

    if (currentBatch.length > 0) {
        result.push(currentBatch);
    }

    return result;
}