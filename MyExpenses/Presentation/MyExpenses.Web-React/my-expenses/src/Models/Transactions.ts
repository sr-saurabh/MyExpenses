
export interface Transaction {
    id: number,
    date: Date,
    amount: number,
    transactionType: TransactionType,
}

export interface WeeklyExpense {
    startDate: Date,
    endDate: Date,
    expenses: number[],
}


export interface GoalExpenseSummary {
    id: number,
    categoryName: string,
    currentMonthExpense: number,
    previousMonthExpense: number,
}

export enum TransactionType{
    Credit=0,
    Debit=1
}