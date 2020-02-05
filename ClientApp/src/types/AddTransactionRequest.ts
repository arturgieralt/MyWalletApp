import TransactionType from "./TransactionType";

export default class AddTransactionRequest {
    private latitude?: number;
    private longitude?: number;

    constructor(
        public name: string,
        public accountId: number,
        public date: Date,
        public total: number,
        public transactionType: TransactionType,
        public tags: string[],
        public categoryId?: number
    ){}

    addCoordinates(latitude: number, longitute: number) {
        this.latitude = latitude;
        this.longitude = longitute;
    }
}