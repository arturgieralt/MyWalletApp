import Category from "./Category";
import { Currency } from "./Currency";
import TransactionType from "./TransactionType";

export default interface Transaction {
    id: number;
    name: string;
    accountId: number;
    total: number;
    date: Date;
    transactionType: TransactionType;
    tags: string[];
    category: Category;
    currency: Currency;
    latitude?: number;
    longitude?: number;
}