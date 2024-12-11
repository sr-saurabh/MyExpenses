import React, { useState, useEffect } from 'react';
import { getCurrentUserProfile } from '../../Services/userServices';
import { useNavigate } from 'react-router-dom';
import { Avatar } from 'primereact/avatar';
import ImageName from '../../Components/ImageName/ImageName';
import './Dashboard.css';

function Dashboard() {
    const navigate = useNavigate();
    const [profileData, setProfileData] = useState(null); // Initial state as null
    const [hasImage, setHasImage] = useState(true);

    useEffect(() => {
        const userProfile = localStorage.getItem('profileData');
        if (userProfile) {
            const parsedProfile = JSON.parse(userProfile);
            setProfileData(parsedProfile);

            if (!parsedProfile.avatar) {
                setHasImage(false);
            } else {
                setHasImage(true);
            }
        } else {
            navigate('/login');
        }
    }, [navigate]);

    // Only render when profileData is not null
    if (!profileData) {
        return <div>Loading...</div>; // Or some kind of loading indicator
    }

    const reditectToProfile = () => {
        navigate('/my-profile');
    }
    return (
        <div className='p-4'>
            <div>
                <section className='position-relative w-fit-content profile-section'>
                    {/* Profile Section */}
                    <div className='card rounded-3 border-0 px-5 py-4 d-flex gap-3 flex-column align-items-center w-fit-content profile-card'>
                        {hasImage && (
                            <Avatar image={profileData.avatar} shape='circle' className='mt-3 profile-img' />
                        )}
                        {!hasImage && (
                            <ImageName profileName={profileData.fullName} divClassName="p-4" textClassName='fs-4' />
                        )}
                        <h2 className='mt-2'>{profileData.fullName}</h2>
                    </div>
                    <div className='position-absolute profile-redirect-btn'>
                        <i className="pi pi-window-maximize cursor-pointer" style={{ fontSize: '1.5rem' }} onClick={reditectToProfile}></i>
                    </div>
                </section>
            </div>
            <div></div>
        </div>
    );
}

export default Dashboard;
