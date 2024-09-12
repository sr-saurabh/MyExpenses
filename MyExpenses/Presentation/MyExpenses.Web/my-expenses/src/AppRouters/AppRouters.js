import { Routes, Route } from 'react-router-dom';
import Login from '../Pages/Login/Login';
import Dashboard from '../Pages/Dashboard/Dashboard';
import Register from '../Pages/Register/Register';
import Friends from '../Pages/Friends/Friends';
import Groups from '../Pages/Groups/Groups';
import Profile from '../Pages/Profile/Profile';
import MyExpenses from '../Pages/MyExpenses/MyExpenses';

const AppRouters = () => {
    return (
        <div>
            <Routes>
                <Route path="/" element={<Login />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register-user" element={<Register />} />
                <Route path="/dashboard" element={<Dashboard />} />
                <Route path="/friends" element={<Friends />} />
                <Route path="/groups" element={<Groups />} />
                <Route path='my-profile' element={<Profile />} />
                <Route path='my-expenses' element={<MyExpenses />} />
            </Routes>
        </div>
    );
};

export default AppRouters;