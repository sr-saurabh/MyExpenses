import React, { useEffect, useState } from 'react';
import UserContext from './UserContext.ts';
import { AppUser } from '../Models/AppUser';
import { useLocation } from 'react-router-dom';

const UserContextProvider = ({ children }) => {
    const [user, setUser] = useState<AppUser | null>(null);
    const [selectedItem, setSelectedItem] = useState('Overview');

    const location = useLocation();

    useEffect(() => {
        const items = [
            { label: 'Overview', icon: 'pi pi-th-large', route: 'overview' },
            { label: 'Balances', icon: 'pi pi-wallet', route: 'balances' },
            { label: 'Transactions', icon: 'pi pi-arrow-right-arrow-left', route: 'transactions' },
            { label: 'Groups', icon: 'pi pi-users', route: 'groups' },
            { label: 'Expenses', icon: 'pi pi-indian-rupee' },
            { label: 'Goals', icon: 'pi pi-bullseye' },
            { label: 'Settings', icon: 'pi pi-cog' }
        ];
        const currentItem = items.find((item) => `/${item.route}` === location.pathname);
        if (currentItem) {
            setSelectedItem(currentItem.label);
        }
    }, [location]);
    return (
        <UserContext.Provider value={{ user, setUser }}>
            {children}
        </UserContext.Provider>
    )

}
export default UserContextProvider;