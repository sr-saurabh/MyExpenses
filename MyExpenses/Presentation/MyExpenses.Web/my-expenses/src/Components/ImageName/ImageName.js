import React, { useState, useEffect } from 'react';
import './ImageName.css';

export default function ImageName(props) {
    const [profileName, setProfileName] = useState('');
    const [imageName, setImageName] = useState('K B');
    
    const bgList = ['bg-primary', 'bg-secondary', 'bg-success', 'bg-danger', 'bg-warning', 'bg-info', 'bg-dark', 'bg-gray', 'bg-blue', 'bg-megenta', 'bg-pink'];
    const bgcolor = bgList[Math.floor(Math.random() * bgList.length)];

    // Update the profileName and imageName when props.profileName changes
    useEffect(() => {
        if (props.profileName) {
            setProfileName(props.profileName);
            // Split the profileName and create initials
            const nameArray = profileName.trim().split(" ");
            if (nameArray.length === 1) {
                setImageName(nameArray[0].charAt(0).toUpperCase());
            } else if (nameArray.length >= 2) {
                const initials = nameArray[0].charAt(0).toUpperCase() + nameArray[1].charAt(0).toUpperCase();
                setImageName(initials);
            }
        }
    }, [profileName]); // Dependency array to trigger when profileName changes

    return (
        <div className={`rounded-circle ${props.divClassName} ${bgcolor}`}>
            <span className={`fw-medium text-white ${props.textClassName}`}>
                {/* Display imageName when available */}
                {imageName} 
            </span>
        </div>
    );
}
