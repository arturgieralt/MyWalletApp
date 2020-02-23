export type AccountInvite = {
        id: number;
        invited: UserResult;
        invitedBy: UserResult;
        accountId: number;
        accountName: string;
        transactionRead: boolean;
        transactionWrite: boolean;
        accountDelete: boolean;
        accountWrite: boolean;
}

export type UserResult = {
    id: string;
    name: string;
}