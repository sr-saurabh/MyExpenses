
import { get, post } from './HttpProviders';

export const getAllActivities=(userId:number)=>{
    return get(`Activity/get-all-user-expense/${userId}`);
}
export const getById=(expenseId:number)=>{
    return get(`Activity/${expenseId}`);
}


export const getByWeeklyExpenses=(userId:number, isCurrent:boolean)=>{
    return get(`Activity/weekly-summary/${userId}?isCurrentWeek=${isCurrent}`);
}


/// gor goal expense summary
export const getGoalExpenseSummary=(userId:number)=>{
    return get(`Goal/goal-expense-summary/${userId}`);
}


export const getAllTransactions=(accountNumber:number)=>
{
    return get(`Transaction/account/${accountNumber}`);
}