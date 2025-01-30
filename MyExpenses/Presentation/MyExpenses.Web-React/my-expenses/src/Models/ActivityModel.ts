import { TransactionType } from "./Transactions";

export interface Activity {
    id: number,
    categoryId: number,
    category: string,
    description: string,
    date: Date,
    amount: number,
    transactionType: TransactionType,
    accountId: number
}

export interface ActivityByCategory{
    category:{
        key:string,
        value: Activity[]
    }
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
export interface UpdateActivityModel extends CreateActivity {
    id:number,
}