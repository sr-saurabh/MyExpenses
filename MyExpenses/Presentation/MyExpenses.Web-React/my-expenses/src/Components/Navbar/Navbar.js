import React, { useEffect } from 'react'
import { Menubar } from 'primereact/menubar';
import { InputText } from 'primereact/inputtext';
import { Badge } from 'primereact/badge';
import { Avatar } from 'primereact/avatar';
import { useRef, useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { Button } from 'primereact/button';
import { googleLogout } from '@react-oauth/google';

import ImageName from '../ImageName/ImageName';
import { ContextMenu } from 'primereact/contextmenu';


import './Navbar.css';

const Navbar= React.memo((props)=>{
    console.log("object")
    const navigate = useNavigate();
    const cm = useRef(null);
    const [menuItems, setMenuItems] = useState([]); // Initial state as empty array
    const [contextItems, setContextItems] = useState([]); // Initial state as empty array
    const [profileData, setProfileData] = useState({});
    const [hasImage, setHasImage] = useState(true);
    const location = useLocation();


    useEffect(() => {
        if(props.profileData){
            setProfileData(props.profileData);
            setHasImage(profileData.avatar ? true : false);
        }
        setMenuItems([
            {
                id: 'dashboard',
                label: 'Dashboard',
                icon: 'pi pi-home',
                command: () => navigate('/dashboard')
            },
            {
                id: 'my-expense',
                label: 'My Expense',
                icon: 'pi pi-indian-rupee',
                command: () => navigate('/my-expenses')
            },
            {
                id: 'friends',
                label: 'Friends',
                icon: 'pi pi-user',
                command: () => navigate('/friends')
            },
            {
                id: 'groups',
                label: 'Groups',
                icon: 'pi pi-users',
                command: () => navigate('/groups')
            }
        ]);
        setContextItems([
            {
                label: 'Profile',
                icon: 'pi pi-id-card',
                command: () => navigate('/my-profile')
            },
            {
                label: 'Logout',
                template: logoutBtn
            }
        ]);

    }, [profileData]);

    const logoutBtn = (
        <Button label="Logout" icon="pi pi-sign-out" className='rounded w-100' onClick={() => handleLogout()} />
    );
    const handleLogout = () => {
        props.handleLogout();
    };

    const onRightClick = (event) => {
        cm.current.show(event);
    };

    const start = <img alt="logo" src="https://primefaces.org/cdn/primereact/images/logo.png" height="40" className="mr-2"></img>;
    const end = (
        <div className="d-flex align-items-center gap-2">
            <span className="p-mr-2">Welcome, <span className='fw-bold'>{profileData?.fullName}</span></span>
            {hasImage ? (<Avatar image={profileData?.avatar} size='normal' shape='circle' className='' onClick={(e) => cm.current.show(e)} />) : (<div onContextMenu={(e) => cm.current.show(e)}>
                    <ImageName profileName={profileData?.fullName} divClassName="p-2" textClassName='fs-6' />
                </div>)}
            {/* <Avatar image={profileData?.avatar} shape="circle" onContextMenu={(event) => onRightClick(event)} /> */}
            <ContextMenu model={contextItems} ref={cm} breakpoint="767px" className='profile-context' />
        </div>
    );
    const makeActiveTab = (event) => {
        menuItems.map((item) => {
            if (item.id === event) {
                console.log(item.className)
                item.className = 'text-info active-tab';
            }
            else{
                item.className = '';
            }
        });
    }
    switch (location.pathname) {
        case '/dashboard':
            makeActiveTab('dashboard');
            break;
        case '/friends':
            makeActiveTab('friends');
            break;
        case '/groups':
            makeActiveTab('groups');
            break;
        case '/my-expenses':
            makeActiveTab('my-expense');
            break;
        default:
            break;
    }
    return (
        <div className="card">
            <Menubar model={menuItems} start={start} end={end} className='d-flex nav-menu' />
        </div>
    )
});
export default  Navbar;
