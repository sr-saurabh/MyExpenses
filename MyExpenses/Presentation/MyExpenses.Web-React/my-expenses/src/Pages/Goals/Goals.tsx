import React, { useEffect, useState } from 'react'
import GoalsCard from '../../Components/GoalsCard/GoalsCard.tsx';
import { Goal, GoalSummary, UpdateGoal } from '../../Models/Goals.tsx';
import TargetForm, { Category } from '../../Components/TargetForm/TargetForm.tsx';
import { AppUser } from '../../Models/AppUser.ts';
import { getAllCategoryGoals, getGoalsSummary, updateGoal } from '../../Services/categoryService.tsx';
import { Dialog } from 'primereact/dialog';
import { getDailyExpenseSummaryForMonth, getGoalExpenseSummary } from '../../Services/transactionService.tsx';
import { CategoryBudget, DailyActivitySummary, GoalExpenseSummary } from '../../Models/Transactions.ts';
import { Chart } from 'primereact/chart';
import { Button } from 'primereact/button';
import { getIconForCategory } from '../../Services/sharedService.tsx';
import ComponentWrapper from '../../Components/Content-Wrapper/ContentWrapper.tsx';
import { getCategories } from '../../Services/PersonalExpenseService.js';


const Goals = () => {
    const [goalSummary, setGoalSummary] = useState<GoalSummary | null>(null);
    const [visible, setVisible] = useState<boolean>(false);
    const [goals, setGoals] = useState<Goal[] | null>(null);
    const [appUser, setAppUser] = useState<AppUser | null>(null)
    const [selectedCategory, setSelectedCategory] = useState<string | undefined>(undefined);

    const [goalExpenseSummary, setGoalExpenseSummary] = useState<GoalExpenseSummary[] | null>(null);

    const [categoryBudgetList, setCategoryBudgetList] = useState<CategoryBudget[]>([]);

    const [dailyExpenseSummary, setDailyExpenseSummary] = useState<DailyActivitySummary[]>([]);
    const [categories, setCategories] = useState<Category[]>([]);

    const [chartData, setChartData] = useState({});
    const [chartOptions, setChartOptions] = useState({});
    const [selectedDateFromGoals, setSelectedDateFromGoals] = useState<Date>();
    const [selectedDateFromChart, setSelectedDateFromChart] = useState<Date>();
    const [selectedDate, setSelectedDate] = useState<Date>();



    const date = new Date();
    const documentStyle = getComputedStyle(document.documentElement);
    const textColor = documentStyle.getPropertyValue('--text-color');
    const textColorSecondary = documentStyle.getPropertyValue('--text-color-secondary');
    const surfaceBorder = documentStyle.getPropertyValue('--surface-border');

    useEffect(() => {
        const options = {
            maintainAspectRatio: false,
            aspectRatio: 1,
            plugins: {
                legend: {
                    labels: {
                        color: textColor
                    }
                }
            },
            scales: {
                x: {
                    ticks: {
                        color: textColorSecondary
                    },
                    grid: {
                        color: surfaceBorder
                    }
                },
                y: {
                    ticks: {
                        color: textColorSecondary
                    },
                    grid: {
                        color: surfaceBorder
                    }
                }
            }
        };


        setSelectedDateFromGoals(date);


        const userProfile = localStorage.getItem('profileData');
        if (userProfile !== undefined && userProfile !== null) {
            let appUser: AppUser = JSON.parse(userProfile);
            setAppUser(appUser);

            //fetching the account details of particular user

            getGoalExpenseSummary(appUser.id).then((response) => {
                if (response.status === 200) {
                    setGoalExpenseSummary(response.data);
                }
            })
            getAllCategoryGoals(appUser.id, date.getMonth() + 1, date.getFullYear()).then((response) => {
                if (response.status === 200) {
                    setGoals(response.data);
                }
            })
            getGoalsSummary(appUser.id, date.getMonth() + 1, date.getFullYear()).then((response) => {
                if (response.status === 200) {
                    setGoalSummary(response.data);
                }
            })
            fetchGetDailyExpenseSummaryForMonth(appUser.id, date.getMonth() + 1);
        }

        // #region :chart configuration
        setChartOptions(options);
        // endregion

    }, []);

    // const fetchGetDailyExpenseSummaryForMonth = (appUserId: number, month: number) => {

    //     getDailyExpenseSummaryForMonth(appUserId, month).then((res) => {
    //         console.log(res)
    //         if (res.status === 200) {
    //             console.log(date)
    //             setDailyExpenseSummary(res.data);
    //             var t = res.data as DailyActivitySummary[];
    //             const daysInMonth = new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();
    //             const dailyData: number[] = [];
    //             for (let i = 1; i < daysInMonth; i++) {
    //                 // if(t.find(d=>d.date.getDate()==i))
    //                 // {
    //                 // }
    //                 var expense = t.find(d => d.date.getDate() == i)
    //                 dailyData.push(expense?.amount ?? 0)
    //             }
    //             t.forEach(d=>{
    //                 console.log(d.date)
    //             })
    //             const data = {
    //                 labels: Array.from({ length: daysInMonth }, (_, i) => `${i + 1}/${date.getMonth() + 1}`),
    //                 datasets: [
    //                     {
    //                         label: 'Daily Expenses',
    //                         data: dailyData,
    //                         fill: true,
    //                         borderColor: documentStyle.getPropertyValue('--orange-500'),
    //                         tension: .5,
    //                         backgroundColor: 'rgba(255,167,38,0.2)'
    //                     }
    //                 ]
    //             };
    //             setChartData(data);
    //         }


    //     })
    // }

    const fetchGetDailyExpenseSummaryForMonth = (appUserId: number, month: number) => {
        getDailyExpenseSummaryForMonth(appUserId, month).then((res) => {
            if (res.status === 200) {
                setDailyExpenseSummary(res.data);

                const t = res.data as DailyActivitySummary[];
                const daysInMonth = new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();
                const dailyData: number[] = [];

                for (let i = 1; i <= daysInMonth; i++) {
                    const expense = t.find(d => {
                        // Convert d.date to a Date object if it’s not already
                        const expenseDate = new Date(d.date);
                        return expenseDate.getDate() === i;
                    });
                    if (expense !== undefined)
                        dailyData.push(expense.total);
                    else
                        dailyData.push(0);

                }

                const data = {
                    labels: Array.from({ length: daysInMonth }, (_, i) => `${i + 1} ${date.toLocaleString('default', { month: 'short' })}`),
                    datasets: [
                        {
                            label: 'Daily Expenses',
                            data: dailyData,
                            fill: true,
                            borderColor: '#14b8a6b0 ',
                            tension: .5,
                            backgroundColor: '#14b8a630',
                        }
                    ]
                };
                setChartData(data);
            }
        });
    };

    const onMonthChangeForChart = (date: Date) => {

    }


    const onMonthChangeForGoals = (date: Date) => {
        setSelectedDateFromGoals(date);
        getGoalsSummary(appUser.id, date.getMonth() + 1, date.getFullYear()).then((response) => {
            if (response.status === 200) {
                setGoalSummary(response.data);
            }
        })
    }


    const getIcon = (categoryName: string) => {
        return getIconForCategory(categoryName);
    }

    const showGoalForm = (categoryName?: string, isCurrentMonth: boolean = false) => {
        if (categories.length === 0) {
            getCategories().then((response) => {
                if (response.status === 200) {
                    setCategories(response.data);
                    setVisible(true);
                }

            })
        }
        setSelectedCategory(categoryName);
        setVisible(true);
        if (isCurrentMonth)
            setSelectedDate(date)
        else
            setSelectedDate(selectedDateFromGoals);

    }

    //completed
    const onGoalUpdate = (goal: UpdateGoal) => {
        // goal.month = selectedDateFromGoals ? selectedDateFromGoals.getMonth() + 1 : date.getMonth() + 1;
        // goal.year = selectedDateFromGoals ? selectedDateFromGoals.getFullYear() : date.getFullYear();
        if (appUser?.id !== undefined) {
            updateGoal(appUser.id, goal).then(() => {
                getAllCategoryGoals(appUser.id, date.getMonth() + 1, date.getFullYear()).then((response) => {
                    if (response.status === 200) {
                        setGoals(response.data);
                    }
                })

                if (selectedDateFromGoals) {
                    getGoalsSummary(appUser.id, selectedDateFromGoals.getMonth() + 1, selectedDateFromGoals.getFullYear()).then((response) => {
                        if (response.status === 200) {
                            setGoalSummary(response.data);
                        }
                    })
                }
                getGoalExpenseSummary(appUser.id).then((response) => {
                    if (response.status === 200) {
                        setGoalExpenseSummary(response.data);
                    }
                })
                setVisible(false);
            })
        }
    }

    return (
        <div>
            <div className='d-flex flex-column gap-2'>
                <p className="mb-1 card-header-text">Expense Comparison</p>
                <div className="d-flex gap-3 ">
                    <GoalsCard target={goalSummary?.budget ?? 0} achieved={goalSummary?.targetSpent ?? 0} isOverViewPage={false} onUpdate={() => showGoalForm()} onMonthChange={(date) => { onMonthChangeForGoals(date) }} ></GoalsCard>
                    <div className="card p-3 flex-grow-1">
                        <Chart type="line" data={chartData} options={chartOptions} />
                    </div>
                </div>
                <p className="mb-1 card-header-text">Expenses Goals by Category(Current Month)</p>
                <div className='card flex-row row flex-wrap gap-3 mx-1'>
                    {
                        goals?.map((ge) => (
                            <div key={ge.id} className='w-25 d-flex gap-3 align-items-center justify-content-between category-name flex-grow-1 pb-2 border-bottom'>
                                <div className='d-flex gap-2'>
                                    <ComponentWrapper height='60px' width='40px' icon={getIcon(ge.categoryName)}></ComponentWrapper>
                                    <div>
                                        {/* Render goal expense summary details here */}
                                        <p className='mb-0 text-secondary fw-medium'>{ge.categoryName}</p>
                                        <p className='mb-0 fw-bold text-black'>₹ {ge.budget}</p>
                                    </div>
                                </div>

                                <Button label="Adjust" icon="pi pi-pencil" className='rounded-3 py-2 h-max-content' iconPos="right" onClick={() => showGoalForm(ge.categoryName, true)} outlined />
                            </div>
                        ))
                    }
                </div>

            </div>
            <Dialog visible={visible} style={{ width: 'clamp(10rem, 50vw, 30rem)' }} header="Update Goal" draggable={false} onHide={() => { if (!visible) return; setVisible(false); }}>
                <div className="px-3 pb-3">
                    <TargetForm goals={goals || []} categories={categories} category={selectedCategory} date={selectedDate} onSubmit={(data) => onGoalUpdate(data.updateGoal)}></TargetForm>
                </div>
            </Dialog>
        </div>
    )
}

export default Goals
