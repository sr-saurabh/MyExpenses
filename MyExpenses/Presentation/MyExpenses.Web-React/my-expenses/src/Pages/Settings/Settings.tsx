import React, { useEffect, useState, useContext } from 'react'
import { TabMenu } from 'primereact/tabmenu';
import { MenuItem } from 'primereact/menuitem';
import './Settings.css';
import { AppUser } from '../../Models/AppUser';
import { Button } from 'primereact/button';
import { Dialog } from 'primereact/dialog';
import { InputText } from 'primereact/inputtext';
import UserContext from '../../Context/UserContext.ts';

import { getCurrentUserProfile, registerUserData, updateUser } from '../../Services/userServices.tsx';
const Settings = () => {
    const [appUser, setAppUser] = useState<AppUser | null>(null)
    const [updateUsrFormVisible, setUserUpdateFormVisible] = useState<boolean>(false);

    const [updateUserSaveBtn, setUpdateUserSaveBtn] = useState<boolean>(false);
    const [updateUserData, setUpdateUserData] = useState<registerUserData>({
        firstName: appUser?.firstName || '',
        lastName: appUser?.lastName || '',
        avatar: appUser?.avatar || '',
        phoneNumber: appUser?.phoneNumber || ''
    });
    const { user, setUser } = useContext(UserContext);


    const items: MenuItem[] = [
        { label: 'Account' },
        { label: 'Security' },
    ];

    useEffect(() => {
        const userProfile = localStorage.getItem('profileData');
        if (userProfile !== undefined && userProfile !== null) {
            let appUser: AppUser = JSON.parse(userProfile);
            setAppUser(appUser);

            setUpdateUserData({
                firstName: appUser.firstName,
                lastName: appUser.lastName,
                avatar: appUser.avatar || null,
                phoneNumber: appUser.phoneNumber
            })
        }
    }, [])

    const onUpdateUser = () => {
        updateUser(appUser?.id, updateUserData).then((res) => {
            console.log(res);
            getCurrentUserProfile(true).then((response) => {
                setUserUpdateFormVisible(false);
                const profileData = response.data;
                setAppUser(response.data);
                setUser(response.data);
                localStorage.setItem('profileData', JSON.stringify(profileData));
            })
        }).catch((error) => {
            console.log(error);
        });
    }

    const handleChange = (e: any) => {
        setUpdateUserData({ ...updateUserData, [e.target?.id]: e.target.value });
        setUpdateUserSaveBtn(true);
    };
    return (
        <div className='card border-0 item-raised'>
            <div className='settings-tab mb-3'>
                <TabMenu model={items} />
            </div>
            <div className='d-flex'>
                <div className='d-flex flex-column gap-3 w-50'>
                    <div className="">
                        <p className='mb-2 fw-medium'>Full Name</p>
                        <span className="ms-3 text-secondary">{appUser?.fullName}</span>
                    </div>
                    <div className="">
                        <p className='mb-2 fw-medium'>Email</p>
                        <span className="ms-3 text-lowercase text-secondary">{appUser?.email}</span>
                    </div>
                    <div className="">
                        <p className='mb-2 fw-medium'>Phone Number</p>
                        <span className="ms-3 text-secondary">{appUser?.phoneNumber}</span>
                    </div>
                </div>
                <div className='w-50 d-flex justify-content-center align-items-center'>
                    <img src={appUser?.avatar} alt="aaa" />
                </div>
            </div>
            <Button label="Update Profile" className='rounded-3 px-5 mt-3  w-max-content' onClick={() => { setUserUpdateFormVisible(true) }} />

            <Dialog header="Update Profile" draggable={false} visible={updateUsrFormVisible} position='top' style={{ width: '35vw' }} onHide={() => { setUserUpdateFormVisible(false) }}>
                <div className="p-fluid">
                    <div className="flex flex-column gap-2">
                        <label htmlFor="firstName" className='text-black fw-medium'>First Name</label>
                        <InputText className='w-100' id="firstName" value={updateUserData?.firstName} placeholder='First Name' onChange={(e) => handleChange(e)} />
                    </div>
                    <div className="flex flex-column gap-2 mt-2">
                        <label htmlFor="lastName" className='text-black fw-medium'>Last Name</label>
                        <InputText className='w-100' id="lastName" value={updateUserData?.lastName} placeholder='Last Name' onChange={(e) => handleChange(e)} />
                    </div>
                    <div className="flex flex-column gap-2 mt-2">
                        <label htmlFor="phoneNumber" className='text-black fw-medium'>Phone Number</label>
                        <InputText className='w-100' id="phoneNumber" value={updateUserData?.phoneNumber} placeholder='Phone Number' onChange={(e) => handleChange(e)} />
                    </div>
                    <div className="flex flex-column gap-2 mt-2">
                        <label htmlFor="email" className='text-black fw-medium'>Email</label>
                        <InputText className='w-100' id="email" disabled={true} value={appUser?.email.toLowerCase()} placeholder='Email' />
                    </div>
                    <Button label="Save" disabled={!updateUserSaveBtn} className='mt-3 rounded-3' onClick={() => { onUpdateUser() }} />
                </div>
            </Dialog>
        </div>
    )
}

export default Settings
