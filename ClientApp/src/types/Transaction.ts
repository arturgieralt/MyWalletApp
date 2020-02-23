import Category from "./Category";
import { Currency } from "./Currency";
import TransactionType from "./TransactionType";

type Transaction = {
    id: number;
    name: string;
    accountId: number;
    total: number;
    date: string;
    transactionType: TransactionType;
    tags: string[];
    category: Category;
    currency: Currency;
    latitude?: number;
    longitude?: number;
}

export default Transaction