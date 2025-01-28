import React, { useEffect, useState } from 'react'
import { useNavigate } from 'react-router-dom';
import { GoalExpenseSummary } from '../../Models/Transactions.tsx';
import { AppUser } from '../../Models/AppUser';
import Statistics from '../../Components/statistics/statistics.tsx';
import { getGoalExpenseSummary } from '../../Services/transactionService.tsx';
import { getIconForCategory } from '../../Services/sharedService.tsx';
import ComponentWrapper from '../../Components/Content-Wrapper/ContentWrapper.tsx';
import { getPersonalExpenses } from '../../Services/PersonalExpenseService.js';
import { Activity } from '../../Models/ActivityModel.ts';
import moment from 'moment';

function Expenses() {
    const navigate = useNavigate();
    const [goalExpenseSummary, setGoalExpenseSummary] = useState<GoalExpenseSummary[] | null>(null);
    const [appUser, setAppUser] = useState<AppUser | null>(null)
    var [expenses, setExpenses] = useState<Activity[]>([]);

    useEffect(() => {
        const userProfile = localStorage.getItem('profileData');
        if (userProfile !== undefined && userProfile !== null) {
            let appUser: AppUser = JSON.parse(userProfile);
            setAppUser(appUser);


            getGoalExpenseSummary(appUser.id).then((response) => {
                if (response.status === 200) {
                    setGoalExpenseSummary(response.data);
                }
            })

            getExpenses(appUser.id);
        }
    }, []);

    const getIcon = (categoryName: string) => {
        return getIconForCategory(categoryName);
    }

    const getComparison = (previous: number, current: number) => {
        var value = 0;
        if (previous === current)
            value = 100;
        else if (previous === 0)
            value = current;
        else if (current === 0)
            value = 0;
        else
            value = ((current * 100) / previous) - 100;
        return (<>
            <p className={value > 0 ? 'text-danger' : 'text-success'}>{value}%
                <span className='ms-1'><i className={`pi pi-${getComparedIcon(previous, current)}`} style={{ fontSize: '12px' }}></i></span>
            </p>
        </>)
    }


    const getComparedIcon = (previous: number, current: number) => {

        if (previous === current)
            return ' text-success';
        if (current === 0)
            return 'arrow-down';
        if (previous === 0)
            return 'arrow-up';
        var percentage = ((current * 100) / previous) - 100;
        return percentage < 0 ? 'arrow-down' : percentage > 0 ? 'arrow-up' : '';
    }
    const getExpenseByCategory = (categoryName: string) => {
        var expense = expenses.filter(e => e.category === categoryName);
        if (expense.length == 0) {
            return (<span className='text-center'>No expense found</span>)
        }
        return (
            <div className='d-flex flex-column gap-1'>
                {expense.map((e) => (
                    <div key={e.id} className='border-bottom d-flex justify-content-between p-1'>
                        <div>
                            <span className='fs-14px fw-medium text-secondary'>{e.description}</span>
                        </div>
                        <div className='d-flex flex-column align-items-end'>
                            <p className='mb-0 fs-12px'>{e.amount.toLocaleString('en-US', { style: 'currency', currency: 'INR' })}</p>
                            <p className='mb-0 fs-12px'>{moment(e.date).format("DD MMM, yyyy")}</p>
                        </div>
                    </div>
                ))}
            </div>
        )
    }


    const getExpenses = (appUserId) => {
        getPersonalExpenses(appUserId)
            .then((response) => {
                setExpenses(response.data);
            })
            .catch((error) => {
                console.log(error);
            });
    }
    // template
    return (
        <div>
            <div className='d-flex flex-column gap-2'>
                <p className="mb-1 card-header-text">Expense Comparison</p>
                {appUser && <Statistics userId={appUser.id}></Statistics>}
            </div>



            <div className='d-flex flex-column gap-2 mt-4'>
                <div className='d-flex justify-content-between'>
                    <p className="mb-1 card-header-text">Expenses Breakdown</p>
                    <span>*Compare to last month</span>
                </div>
                <div className='d-flex ms-1 row flex-wrap gap-3'>
                    {
                        goalExpenseSummary?.map((ge) => (
                            <div key={ge.id} className='card border-0 col-3 d-flex category-name flex-grow-1 p-0'>

                                {/* header */}
                                <div className="card-header d-flex gap-2 bg-body-secondary">
                                    <ComponentWrapper height='48px' parentClass='bg-dark-subtle' width='40px' icon={getIcon(ge.categoryName)}></ComponentWrapper>
                                    <div className='d-flex justify-content-between flex-grow-1'>
                                        {/* Render goal expense summary details here */}
                                        <div>
                                            <p className='mb-0 text-secondary fw-medium'>{ge.categoryName}</p>
                                            <p className='mb-0'>Rs. {ge.currentMonthExpense}</p>

                                        </div>
                                        <div className='d-flex flex-column align-items-end'>

                                            {getComparison(ge.previousMonthExpense, ge.currentMonthExpense)}
                                            <p className="mb-0">Compared to last month</p>
                                        </div>
                                    </div>
                                </div>
                                {/* content */}
                                <div className='p-3'>
                                    {getExpenseByCategory(ge.categoryName)}
                                </div>
                            </div>
                        ))
                    }
                </div>
            </div>
        </div>
    )
}

export default Expenses;
