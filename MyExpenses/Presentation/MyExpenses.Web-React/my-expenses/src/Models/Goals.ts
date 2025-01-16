export interface Goal {
    id:number,
    year: number,
    month: number,
    budget: number,
    totalSpent: number,
    categoryId: number,
    categoryName: string
}
export interface UpdateGoal {
    userId:number
    id:number,
    year: number,
    month: number,
    budget: number,
    categoryId: number,
}
export interface GoalSummary {
    year: number,
    month: number,
    budget: number,
    targetSpent:number
}
