import { UpdateGoal } from '../Models/Goals';
import { get, post, put } from './HttpProviders';

export const getAllCategoryGoals = (userId:number, month:number, year:number ) => {
    return get(`Goal/user/${userId}/month/${month}/year/${year}`)
}
export const getGoalsSummary = (userId:number, month:number, year:number ) => {
    return get(`Goal/get-summary/user/${userId}/month/${month}/year/${year}`)
}

export const getAllCategories = (userId:number, month:number, year:number) => {

}


export const updateGoal=(userId:number, updateGoal:UpdateGoal)=>{
    return put(`Goal/user/${userId}`, updateGoal);
}