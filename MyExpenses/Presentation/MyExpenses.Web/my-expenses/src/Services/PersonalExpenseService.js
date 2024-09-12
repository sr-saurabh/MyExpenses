import { post,get, put, remove } from '../Services/HttpProviders';

export const addPersonalExpense = (expenseData) => {
    return post('PersonalExpense', expenseData);
};

export const getCategories = (appUserId) => {
    return get(`PersonalExpense/categories/${appUserId}`);
};

export const getPersonalExpenses = (appUserId) => {
    return get(`PersonalExpense/get-all-user-expense/${appUserId}`);
};

export const getPersonalExpense = (id) => {
    return get(`PersonalExpense/${id}`);
};

export const getPersonalExpenseSummary=(appuserId)=>{
    return get(`PersonalExpense/get-expense-summary/${appuserId}`);
}

export const updatePersonalExpense = (expenseId, expenseData) => {
    return put(`PersonalExpense/update/${expenseId}`, expenseData);
};

export const deletePersonalExpense = (id) => {
    return remove(`PersonalExpense/delete/${id}`);
};

export const getFilteredResults = (appUserId, filterData) => {
    return post(`PersonalExpense/filter/${appUserId}`, filterData);
};