import 'primeicons/primeicons.css';
import React, { useContext, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import './App.css';
import AppRouters from './AppRouters/AppRouters';
import Sidebar from './Components/Sidebar/Sidebar';
import TopNav from './Components/TopNav/TopNav.tsx';
import UserContext from './Context/UserContext.ts';
import UserContextProvider from './Context/UserContextProvider.tsx';
import { getCurrentUserProfile } from './Services/userServices.tsx';

function App() {
  var [profileData, setProfileData] = useState({});
  var [isLogged, setIsLogged] = useState(false);
  const navigate = useNavigate();
  const { user, setUser } = useContext(UserContext);

  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token == null || token == undefined) {
      navigate('/login');
      return;
    }

    if (user !== null) {
      setIsLogged(true);
    }
    else {
      const userProfile = localStorage.getItem('profileData');
      if (userProfile !== undefined && userProfile !== null) {
        const pd= JSON.parse(userProfile);
        setProfileData(pd);
        setUser(JSON.parse(userProfile));
        setIsLogged(true);
      } else {
        getCurrentUserProfile()
          .then((response) => {
            if (response.status !== 200)
              return;
            // throw new Error('Failed to fetch user profile');
            const profile = response.data;
            setProfileData(profile);
            localStorage.setItem('profileData', JSON.stringify(profile));
            setUser(profile);
            setIsLogged(true);
          })
          .catch((error) => {
            navigate('/login');
          });
      }
    }
  }, [navigate]);

  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('profileData');
    setIsLogged(false);
    navigate('/login');
  }

  return (
    <>
      <div className='d-flex content'>
        {isLogged &&
          <div>
            <Sidebar profileData={profileData} handleLogout={handleLogout} />
          </div>
        }
        <div className='flex-grow-1'>
          {isLogged &&
            <div>
              <TopNav name={profileData.fullName}></TopNav>
            </div>
          }
          <div className={`${isLogged ? 'main-container' : ''}`}>

            <AppRouters />
          </div>
        </div>
      </div>


    </>
  );
}

export default App;
