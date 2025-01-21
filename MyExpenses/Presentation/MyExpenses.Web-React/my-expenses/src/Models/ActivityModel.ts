import { TransactionType } from "./Transactions";

export interface Activity {
    id: number,
    category: string,
    description: string,
    date: Date,
    amount: number,
    transactionType: TransactionType,
}
export interface CreateActivity {
    category: string,
    description: string,
    date: Date,
    amount: number,
    type: TransactionType,
    categoryId: number,
    accountId: number,
    appUserId: number,
}