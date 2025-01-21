import { Routes, Route } from 'react-router-dom';
import Login from '../Pages/Login/Login';
import Overview from '../Pages/Overview/Overview.tsx';
import Register from '../Pages/Register/Register';
import Friends from '../Pages/Friends/Friends';
import Groups from '../Pages/Groups/Groups';
import Profile from '../Pages/Profile/Profile';
import Transactions from '../Pages/Transactions/Transactions.tsx';
import Balances from '../Pages/balances/balances.tsx';
import AccountDetails from '../Pages/AccountDetails/AccountDetails.tsx';

const AppRouters = () => {
    return (
        <div>
            <Routes>
                <Route path="/"  element={<Overview />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register-user" element={<Register />} />
                <Route path="/overview" element={<Overview />} />
                <Route path="/balances" element={<Balances />} />
                <Route path="/balances/account/:accountId" element={<AccountDetails />} />
                <Route path="/friends" element={<Friends />} />
                <Route path="/groups" element={<Groups />} />
                <Route path='my-profile' element={<Profile />} />
                <Route path='activity' element={<Transactions />} />
            </Routes>
        </div>
    );
};

export default AppRouters;