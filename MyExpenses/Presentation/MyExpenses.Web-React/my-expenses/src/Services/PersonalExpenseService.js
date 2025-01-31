import { post,get, put, remove } from '../Services/HttpProviders';

export const addPersonalExpense = (expenseData) => {
    return post('Activity', expenseData);
};

export const getCategories = () => {
    return get(`Category`);
};

export const getPersonalExpenses = (appUserId) => {
    return get(`Activity/get-all-user-expense/${appUserId}`);
};

export const getPersonalExpense = (id) => {
    return get(`Activity/${id}`);
};

export const getPersonalExpenseSummary=(appuserId)=>{
    return get(`Activity/get-expense-summary/${appuserId}`);
}

export const updatePersonalExpense = (expenseId, expenseData) => {
    return put(`Activity/${expenseId}`, expenseData);
};

export const deletePersonalExpense = (id) => {
    return remove(`Activity/${id}`);
};

export const getFilteredResults = (appUserId, filterData) => {
    return post(`Activity/filter/${appUserId}`, filterData);
};