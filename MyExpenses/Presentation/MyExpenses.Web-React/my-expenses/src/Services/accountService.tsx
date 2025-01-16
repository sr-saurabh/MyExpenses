import { CreateAccountModel } from '../Models/AccountModel';
import { get, post } from './HttpProviders';

export const addNewAccount = (createAccount:CreateAccountModel) => {
  return post(`Account`,createAccount)
}

export const getAccounts = (userId: number) => {
  return get(`Account/getAll?userId=${userId}`)
}
export const getAccount = (accountId: number) => {
  return get(`Account/?accountId=${accountId}`)
}


