import React, { useEffect, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import './Sidebar.css';

import { Divider } from 'primereact/divider';

import { Avatar } from 'primereact/avatar';
import { AvatarGroup } from 'primereact/avatargroup';   //Optional for grouping

import { ContextMenu } from 'primereact/contextmenu';
import ImageName from '../ImageName/ImageName'
const Sidebar = React.memo((props) => {
    const navigate = useNavigate();
    const location = useLocation();
    const [selectedItem, setSelectedItem] = useState('Overview');
    const [menuItems, setMenuItems] = useState([]);
    const [profileData, setProfileData] = useState({});
    const [hasImage, setHasImage] = useState(true);

    useEffect(() => {
        if (props.profileData) {
            setProfileData(props.profileData);
            setHasImage(props.profileData.avatar ? true : false);
        }
    }, [props.profileData]);

    useEffect(() => {
        const items = [
            { label: 'Overview', icon: 'pi pi-th-large', route: 'overview' },
            { label: 'Balances', icon: 'pi pi-wallet', route: 'balances' },
            { label: 'Activity', icon: 'pi pi-arrow-right-arrow-left', route: 'activity' },
            { label: 'Groups', icon: 'pi pi-users', route: 'groups' },
            { label: 'Expenses', icon: 'pi pi-indian-rupee', route:'expenses' },
            { label: 'Goals', icon: 'pi pi-bullseye' },
            { label: 'Settings', icon: 'pi pi-cog' }
        ];
        setMenuItems(items);
        console.log(location.pathname)
        const currentItem = items.find((item) => location.pathname.includes(item.route));
        // const currentItem = items.find((item) => `/${item.route}` === location.pathname);
        if (currentItem) {
            setSelectedItem(currentItem.label);
        }
    }, [location.pathname]); // Ensure it runs when location.pathname changes

    const menuItemSelected = (menuItem) => {
        setSelectedItem(menuItem.label);
        console.log(menuItem)
        if (menuItem.route) {
            navigate(`/${menuItem.route}`);
        }
    };

    const handleLogout=()=>{
        props.handleLogout();
    }
    return (
        <div className='d-flex flex-column justify-content-between h-100 p-4 py-3 bg-black text-white'>
            <div className='d-flex flex-column gap-4'>
                <div className='d-flex justify-content-center text-teal align-items-end user-select-none'>
                    <span className='fs-1 fw-bold'>MY E </span>
                    <span className='fs-1 fw-medium'>xpenses</span>
                </div>
                <div>
                    <ul className='list-unstyled d-flex flex-column gap-1'>
                        {menuItems.map((item) => (
                            <li
                                key={item.label}
                                className={`p-3 list-item d-flex align-items-center gap-3 rounded-2 cursor-pointer ${selectedItem === item.label ? 'active-tab' : ''}`}
                                onClick={() => menuItemSelected(item)}
                            >
                                <i className={`pi ${item.icon}`} style={{ fontSize: '1rem' }}></i>
                                <span>{item.label}</span>
                            </li>
                        ))}
                    </ul>
                </div>
            </div>
            <div className='d-flex flex-column bottom-container bg-black'>
                <div className='p-3 d-flex gap-3 align-items-center bg-dark rounded-3 cursor-pointer' onClick={()=>{handleLogout()}}>
                    <i className="pi pi-sign-out"></i>
                    <span className='ms-3'>Logout</span>
                </div>
                <Divider></Divider>
                <div className="d-flex align-items-center gap-2">
                    {/* {hasImage ? (<Avatar image={profileData?.avatar} size='normal' shape='circle' className='' />) : (<div>
                    </div>)} */}
                        <ImageName profileName={profileData?.fullName} divClassName="p-2" textClassName='fs-6' />
                    <span className="p-mr-2"><span className='fw-bold'>{profileData?.fullName}</span></span>
                </div>
            </div>
        </div>
    );
});

export default Sidebar;
