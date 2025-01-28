import moment from 'moment';
import { Dialog } from 'primereact/dialog';
import { MenuItem } from 'primereact/menuitem';
import { TabMenu } from 'primereact/tabmenu';
import React, { useContext, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import ComponentWrapper from '../../Components/Content-Wrapper/ContentWrapper.tsx'; // Adjust the import path as necessary
import GoalsCard from '../../Components/GoalsCard/GoalsCard.tsx';
import Statistics from '../../Components/statistics/statistics.tsx';
import TargetForm from '../../Components/TargetForm/TargetForm.tsx';
import UserContext from '../../Context/UserContext.ts';
import { AccountModel, AccountType } from '../../Models/AccountModel.ts';
import { AppUser } from '../../Models/AppUser.ts';
import { Goal, GoalSummary, UpdateGoal } from '../../Models/Goals.ts';
import { Activity, GoalExpenseSummary, Transaction } from '../../Models/Transactions.tsx';
import { getAccounts } from '../../Services/accountService.tsx';
import { getAllCategoryGoals, getGoalsSummary, updateGoal } from '../../Services/categoryService.tsx';
import { getIconForCategory } from '../../Services/sharedService.tsx';
import { getAllActivities, getGoalExpenseSummary } from '../../Services/transactionService.tsx';
import './Overview.css';


function Overview() {
    const navigate = useNavigate();
    const [appUser, setAppUser] = useState<AppUser | null>(null)
    const [visible, setVisible] = useState<boolean>(false);

    const [selectedAccount, setSelectedAccount] = useState<AccountModel | null>(null);
    const [accounts, setAccounts] = useState<AccountModel[] | null>(null);
    const [goals, setGoals] = useState<Goal[] | null>(null);
    const [goalSummary, setGoalSummary] = useState<GoalSummary | null>(null);
    const [transactions, setTransactions] = useState<Activity[] | null>(null);
    const [goalExpenseSummary, setGoalExpenseSummary] = useState<GoalExpenseSummary[] | null>(null);
    const { user, setUser } = useContext(UserContext);

    const date = new Date();

    const items: MenuItem[] = [
        { label: 'All', command: () => { getTransaction('All') } },
        { label: 'Income', command: () => { getTransaction('Income') } },
        { label: 'Expenses', command: () => { getTransaction('Expenses') } },
    ];


    useEffect(() => {
        const userProfile = localStorage.getItem('profileData');
        if (userProfile !== undefined && userProfile !== null) {
            let appUser: AppUser = JSON.parse(userProfile);
            setAppUser(appUser);

            //fetching the account details of particular user
            getAccounts(appUser.id).then((response: any) => {
                // console.log(response);
                setAccounts(response.data);
                if (response.data && response.data.length !== 0)
                    setSelectedAccount(response.data[0]);
            });
            getAllActivities(appUser.id).then((response) => {
                if (response.status === 200) {
                    setTransactions(response.data);
                }
            });

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

        }
    }, []);
    const selectAccount = (isNext: boolean) => {
        console.log('selectAccount called')
        var id = selectedAccount?.id;
        if (isNext) {
            if (accounts) {
                const nextIndex = accounts.findIndex(s => s.id === id) + 1;
                if (nextIndex < accounts.length)
                    setSelectedAccount(accounts[nextIndex]);
            }
        }
        else {
            if (accounts) {
                const nextIndex = accounts.findIndex(s => s.id === id) - 1;
                if (nextIndex < accounts.length)
                    setSelectedAccount(accounts[nextIndex]);
            }
        }
    }
    const getTransaction = (type: string) => {
        console.log(type);
    }

    const compare = (previous: number, current: number) => {
        if (current === 0)
            return -previous;
        if (previous === 0)
            return current;

        return ((current * 100) / previous) - 100;
    }
    const getComparedIcon = (previous: number, current: number) => {

        if (previous === current)
            return 'equals';
        if (current === 0)
            return 'arrow-down';
        if (previous === 0)
            return 'arrow-up';
        var percentage = ((current * 100) / previous) - 100;
        return percentage < 0 ? 'arrow-down' : percentage > 0 ? 'arrow-up' : 'equals';
    }
    const getIcon = (categoryName: string) => {
        return getIconForCategory(categoryName);
    }

    const onGoalUpdate = (goal: UpdateGoal) => {
        console.log("onGoalUpdate called")
        goal.month = date.getMonth() + 1;
        goal.year = date.getFullYear();
        if (appUser?.id !== undefined) {
            updateGoal(appUser.id, goal).then(() => {
                alert("goal Updated successfully");
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
                getGoalExpenseSummary(appUser.id).then((response) => {
                    if (response.status === 200) {
                        setGoalExpenseSummary(response.data);
                    }
                })
                setVisible(false);
            })
        }
    }

    const redirectToBalances = () => {
        navigate('/balances');
    }
    return (
        <div className='d-flex flex-column gap-4'>
            <div className='d-flex gap-4'>
                <div className='flex-grow-1'>
                    <p className="mb-1 card-header-text">Total Balance</p>
                    <div className='card flex-column gap-3'>
                        <div className='d-flex justify-content-between  card-head border-bottom'>
                            <span>
                                {selectedAccount ? `Rs.${selectedAccount.balance}` : 'Rs NA'}
                            </span>
                            <span className='cursor-pointer fs-6' onClick={redirectToBalances}>All Accounts</span>
                        </div>
                        <div className='d-flex justify-content-between bg-teal gap-1 p-3 rounded-2 text-white'>
                            <div>
                                <p className="mb-0 fs-14px">Account Type</p>
                                <p className="mb-0 fw-bold">{AccountType[selectedAccount?.accountType]}</p>
                                <p className="mb-0">{selectedAccount?.accountNumber}</p>
                            </div>
                            <div className='d-flex flex-column justify-content-between '>
                                <div className='d-flex flex-column align-items-end'>
                                    <span className='fs-14px'>A/C Name</span>
                                    <span className='fw-bold'>{selectedAccount?.accountName}</span>
                                </div>
                                <div className='d-flex align-items-center gap-1'>
                                    <span>Rs. {selectedAccount?.balance}</span>
                                    <i className="pi pi-arrow-circle-right" style={{ color: '#fff' }}></i>
                                </div>
                            </div>
                        </div>
                        <div className='d-flex justify-content-between'>
                            <div className={`d-flex align-items-center gap-1 ${selectedAccount?.id === accounts?.[0]?.id ? 'opacity-75 text-secondary' : 'cursor-pointer'}`} onClick={() => { if (selectedAccount?.id !== accounts?.[0]?.id) selectAccount(false) }}>
                                <i className="pi pi-chevron-left" style={{ color: '#708090', fontSize: '12px' }}></i>
                                <span className='ms-1'>Previous</span>
                            </div>
                            <div className='d-flex align-items-center gap-2'>
                                {accounts?.map((account, index) => (
                                    <p key={index} className={`mb-0 rounded-circle accounts-dot ${selectedAccount?.id === account.id ? 'active-dot' : ''}`}></p>
                                ))}
                            </div>
                            <div className={`d-flex align-items-center gap-1  ${selectedAccount?.id === accounts?.[accounts.length - 1]?.id ? 'opacity-75 text-secondary' : 'cursor-pointer'}`} onClick={() => { if (selectedAccount?.id !== accounts?.[accounts.length - 1]?.id) selectAccount(true) }}>
                                <span className='me-1'>Next</span>
                                <i className="pi pi-chevron-right" style={{ color: '#708090', fontSize: '12px' }}></i>
                            </div>
                        </div>
                    </div>
                </div>
                <div className='flex-grow-1'>
                    <p className="mb-1 card-header-text">Goals</p>
                    <GoalsCard target={goalSummary?.budget ?? 0} achieved={goalSummary?.targetSpent ?? 0} onUpdate={() => setVisible(true)} ></GoalsCard>
                </div>
                <div className='flex-grow-1'>
                    <div className='d-flex justify-content-between'>
                        <p className="mb-1 card-header-text">Upcoming bills</p>
                        <p className="mb-1 cursor-pointer fs-6">
                            <span>View All</span>
                            <i className="pi pi-angle-right" style={{ color: '#708090' }}></i>
                        </p>
                    </div>
                    <div className='card flex-column gap-3 h-75'>
                        <p>{`<add-content>`}</p>
                    </div>
                </div>
            </div>
            <div className='row'>
                <div className='col-3 d-flex flex-column  flex-grow-1 gap-1'>
                    <div className='d-flex justify-content-between align-items-center w-100' >
                        <p className="mb-1 card-header-text">Recent Transaction</p>
                        <p className="mb-0 cursor-pointer fs-6">
                            <span>View All</span>
                            <i className="pi pi-angle-right" style={{ color: '#708090' }}></i>
                        </p>
                    </div>
                    {/* transactions */}
                    <div className='card'>
                        <div className='transaction-tab'>
                            <TabMenu model={items} />
                        </div>
                        <div>
                            {transactions?.slice(0, 6).map((transaction) => {
                                return (
                                    <div key={transaction.id} className='d-flex justify-content-between border-bottom py-4'>
                                        {/* Render transaction details here */}
                                        <div className='d-flex gap-3'>
                                            <ComponentWrapper size='large' icon={getIconForCategory(transaction.category)}></ComponentWrapper>
                                            <div>
                                                <p className='mb-0 fw-medium'>{transaction.category}</p>
                                                <p className='mb-0 sub-text'>{transaction.description}</p>
                                            </div>
                                        </div>
                                        <div>
                                            <p className='mb-0'>Rs.{transaction.amount}</p>
                                            <p className='mb-0 sub-text'>{moment(transaction.date).format("DD MMM, yyyy")}</p>
                                        </div>
                                    </div>
                                )
                            })}
                        </div>
                    </div>
                </div>
                <div className='col-8'>
                    <div className='d-flex flex-column gap-2'>
                        <p className="mb-1 card-header-text">Statistics</p>
                        {appUser && <Statistics userId={appUser.id}></Statistics>}
                    </div>
                    <div className='d-flex flex-column gap-2 mt-4'>
                        <div className='d-flex justify-content-between'>
                            <p className="mb-1 card-header-text">Expenses Breakdown</p>
                            <span>*Compare to last month</span>
                        </div>
                        <div className='card flex-row row flex-wrap gap-3'>
                            {
                                goalExpenseSummary?.map((ge) => (
                                    <div key={ge.id} className='col-3 d-flex gap-3 category-name flex-grow-1 pb-2 border-bottom'>
                                        <ComponentWrapper height='60px' width='40px' icon={getIcon(ge.categoryName)}></ComponentWrapper>
                                        <div>
                                            {/* Render goal expense summary details here */}
                                            <p className='mb-0 text-secondary fw-medium'>{ge.categoryName}</p>
                                            <p className='mb-0'>Rs. {ge.currentMonthExpense}</p>
                                            <p className='mb-0 sub-text'>
                                                {compare(ge.previousMonthExpense, ge.currentMonthExpense)}%*
                                                <span className={`${compare(ge.previousMonthExpense, ge.currentMonthExpense) < 0 ? 'text-success' : compare(ge.previousMonthExpense, ge.currentMonthExpense) > 0 ? 'text-danger' : 'text-secondary'} ms-2`}>
                                                    <i className={`pi pi-${getComparedIcon(ge.previousMonthExpense, ge.currentMonthExpense)}`} style={{ fontSize: '12px' }}></i>
                                                </span>
                                            </p>
                                        </div>
                                    </div>
                                ))
                            }
                        </div>
                    </div>
                </div>
            </div>

            <Dialog visible={visible} style={{ width: 'clamp(10rem, 50vw, 30rem)' }} draggable={false} onHide={() => { if (!visible) return; setVisible(false); }}>
                <div className="px-3 pb-3">
                    <TargetForm goals={goals || []} onSubmit={(data) => onGoalUpdate(data.updateGoal)} ></TargetForm>
                </div>
            </Dialog>
        </div>
    );
}

export default Overview;
