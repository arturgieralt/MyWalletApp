import { Currency } from "./Currency";

export interface AccountSummary {
    id: number;
    name: string;
    balance: number;
    transactionCount: number;
    currency: Currency;
    createdOn: Date;
}