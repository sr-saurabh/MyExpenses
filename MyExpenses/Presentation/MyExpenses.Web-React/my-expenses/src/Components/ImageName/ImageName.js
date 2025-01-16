import React, { useState, useEffect } from 'react';
import './ImageName.css';

const ImageName = React.memo((props) => {
    const [profileName, setProfileName] = useState('');
    const [imageName, setImageName] = useState('');
    
    const bgList = ['bg-primary', 'bg-secondary', 'bg-success', 'bg-danger', 'bg-warning', 'bg-info', 'bg-dark', 'bg-gray', 'bg-blue', 'bg-megenta', 'bg-pink'];
    const bgcolor = bgList[Math.floor(Math.random() * bgList.length)];

    // Update the profileName and imageName when props.profileName changes
    useEffect(() => {
        if (props.profileName) {
            setProfileName(props.profileName);
            // Split the profileName and create initials
            const nameArray = props.profileName.trim().split(" ");
            if (nameArray.length === 1) {
                setImageName(nameArray[0].charAt(0).toUpperCase());
            } else if (nameArray.length >= 2) {
                const initials = nameArray[0].charAt(0).toUpperCase() + nameArray[1].charAt(0).toUpperCase();
                setImageName(initials);
            }
        }
    }, [props.profileName]); // Dependency array to trigger when profileName changes

    return (
        <div className={`rounded-circle bg-dark ${props.divClassName} name-container`}>
            <span className={`fw-medium text-white ${props.textClassName}`}>
                {/* Display imageName when available */}
                {imageName} 
            </span>
        </div>
    );
});
export default ImageName;
