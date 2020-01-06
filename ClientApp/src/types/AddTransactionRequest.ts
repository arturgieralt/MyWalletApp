import TransactionType from "./TransactionType";

export default class AddTransactionRequest {
    constructor(
        public name: string,
        public accountId: number,
        public date: Date,
        public total: number,
        public transactionType: TransactionType,
        public categoryId?: number
    ){}
}