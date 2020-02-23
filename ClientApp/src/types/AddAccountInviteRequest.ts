export class AddAccountInviteRequest {
    constructor(
        public accountId: number,
        public email: string,
        public transactionRead: boolean,
        public transactionWrite: boolean,
        public accountWrite: boolean,
        public accountDelete: boolean
    ) {}
}