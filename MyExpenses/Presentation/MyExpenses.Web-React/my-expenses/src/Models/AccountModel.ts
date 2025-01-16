export interface AccountModel{
    id: number,
    appUserId: number,
    accountName: string,
    accountNumber: string,
    branchName: string,
    ifsc: string,
    accountType: AccountType,
    balance: number

}

export interface CreateAccountModel{
    appUserId: number,
    accountName: string,
    accountNumber: string,
    branchName: string,
    ifsc: string,
    accountType: AccountType,
    balance: number
}

export interface UpdateAccountModel{
    id: number,
    appUserId: number,
    accountName: string,
    accountNumber: string,
    branchName: string,
    ifsc: string,
    accountType: AccountType
}

export enum AccountType{
    Saving= 1,
    Current=2,
    Salary= 3,
    FixedDeposit=4 ,
    RecurringDeposit= 5,
    NRI= 6
}

