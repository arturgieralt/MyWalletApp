enum TransactionType {
    Expense = 0,
    Income
}

export default TransactionType;

export interface ITransactionType {
    id: number;
    name: string;
}

export const  getTransactionTypes = (): ITransactionType[] => {
    const keys =  Object.keys(TransactionType);
    return keys.map(k => (
    {
        id: TransactionType[k],
        name: k 
    }));
}