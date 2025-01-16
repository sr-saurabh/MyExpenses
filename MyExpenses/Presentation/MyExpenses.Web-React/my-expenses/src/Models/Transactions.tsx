
export interface Transaction {
    id: number,
    category: string,
    description: string,
    date: Date,
    amount: number,
    type: string,
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