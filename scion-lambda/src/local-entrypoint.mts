import { handler } from "./handlers/sync-database.mjs"
import { config } from "dotenv";

config();
console.log(process.env.TEST)
await handler();