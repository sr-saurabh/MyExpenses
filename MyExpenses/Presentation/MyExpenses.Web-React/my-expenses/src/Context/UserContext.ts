import { createContext } from 'react';
import { AppUser } from '../Models/AppUser';

interface UserContextType {
    user: AppUser | null;
    setUser: React.Dispatch<React.SetStateAction<AppUser | null>>;
}

const UserContext = createContext<UserContextType | undefined>(undefined);

export default UserContext;
