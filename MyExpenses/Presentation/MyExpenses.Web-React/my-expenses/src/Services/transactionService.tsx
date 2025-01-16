
import { get, post } from './HttpProviders';

export const getAllTransactions=(userId:number)=>{
    return get(`PersonalExpense/get-all-user-expense/${userId}`);
}
export const getById=(expenseId:number)=>{
    return get(`PersonalExpense/${expenseId}`);
}


export const getByWeeklyExpenses=(userId:number, isCurrent:boolean)=>{
    return get(`PersonalExpense/weekly-summary/${userId}?isCurrentWeek=${isCurrent}`);
}


/// gor goal expense summary
export const getGoalExpenseSummary=(userId:number)=>{
    return get(`Goal/goal-expense-summary/${userId}`);
}