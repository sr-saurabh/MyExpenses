import { Routes, Route } from 'react-router-dom';
import Login from '../Pages/Login/Login';
import Overview from '../Pages/Overview/Overview.tsx';
import Register from '../Pages/Register/Register';
import Friends from '../Pages/Friends/Friends';
import Groups from '../Pages/Groups/Groups';
import Transactions from '../Pages/Transactions/Transactions.tsx';
import Balances from '../Pages/balances/balances.tsx';
import AccountDetails from '../Pages/AccountDetails/AccountDetails.tsx';
import Expenses from '../Pages/Expenses/Expenses.tsx';
import Goals from '../Pages/Goals/Goals.tsx';
import Settings from '../Pages/Settings/Settings.tsx';

const AppRouters = () => {
    return (
            <Routes>
                <Route path="/"  element={<Overview />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register-user" element={<Register />} />
                <Route path="/overview" element={<Overview />} />
                <Route path="/balances" element={<Balances />} />
                <Route path="/balances/account/:accountId" element={<AccountDetails />} />
                <Route path="/expenses" element={<Expenses />} />
                <Route path="/goals" element={<Goals />} />
                <Route path="/friends" element={<Friends />} />
                <Route path="/groups" element={<Groups />} />
                <Route path='/settings' element={<Settings />} />
                <Route path='activity' element={<Transactions />} />
            </Routes>
    );
};

export default AppRouters;