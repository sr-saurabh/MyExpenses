import './App.css';
import React from 'react';
import { useState, useEffect } from 'react';
import AppRouters from './AppRouters/AppRouters';
import Navbar from './Components/Navbar/Navbar';
import { getCurrentUserProfile } from './Services/userServices';
import { useNavigate } from 'react-router-dom';
import 'primeicons/primeicons.css';

function App() {
  var [profileData, setProfileData] = useState({});
  var [isLogged, setIsLogged] = useState(false);
  const navigate = useNavigate();
  useEffect(() => {
    const token = localStorage.getItem('token');
    if (token == null || token == undefined) {
      navigate('/login');
      return;
    }


    const userProfile = localStorage.getItem('profileData');
    // console.log("test", userProfile);
    // console.log("test1", typeof userProfile);
    if (userProfile != null && userProfile != undefined && userProfile!="" ) {
      setProfileData(JSON.parse(userProfile));
      // console.log(1);
      // console.log(profileData);
      setIsLogged(true);
    } else {
      getCurrentUserProfile()
      .then((response) => {
        const profile = response.data;
        setProfileData(profile);
        localStorage.setItem('profileData', JSON.stringify(profile));
        setIsLogged(true);
        console.log(profileData);
        })
        .catch((error) => {
          console.log(error);
          navigate('/login');
        });

    }
  }, [navigate]);

  const handleLogout = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('profileData');
    setIsLogged(false);
    navigate('/login');
    console.log('User logged out successfully');
  }

  return (
    <>
      {isLogged &&
        <div>
          <Navbar profileData={profileData} handleLogout={handleLogout} />
        </div>
      }
      <AppRouters />

    </>
  );
}

export default App;
