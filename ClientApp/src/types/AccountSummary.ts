import { Currency } from "./Currency";

export type AccountSummary = {
    id: number;
    name: string;
    balance: number;
    transactionCount: number;
    currency: Currency;
    createdOn: Date;
}