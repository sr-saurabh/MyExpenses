import moment from 'moment';
import { Badge } from 'primereact/badge';
import { InputText } from "primereact/inputtext";
import React, { useEffect, useState } from 'react';
import './TopNav.css';
        

interface TopNavProps {
    name: string;
    event: any;
}

const TopNav: React.FC<TopNavProps> = (props) => {
    const today = new Date().toLocaleDateString();
    const [searchValue, setSearchValue] = useState('');
    const [name, setName] = useState('');

    useEffect(() => {
        const firstName = props?.name.split(' ')[0];
        setName(firstName);
    }, [props.name]);
    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        console.log(e);
        setSearchValue(e.target.value)
    }
    return (
        <nav className="top-nav d-flex justify-content-between border-bottom ">
            <div className="nav-left d-flex align-items-center gap-4">
                <span className="nav-name fs-4 fw-bold">Hello {name}</span>
                <div className="d-flex align-items-center gap-2">
                    <i className="pi pi-angle-double-right" style={{ color: '#708090' }}></i>
                    <span className="nav-date">{moment(today).format("DD MMM, yyyy")}</span>
                </div>
            </div>
            <div className="nav-right d-flex align-items-center gap-3">
                <div>
                    <i className="pi pi-bell p-overlay-badge" style={{ fontSize: '1rem' }}>
                        <Badge></Badge>
                    </i>
                </div>
                <div className="position-relative">
                    <InputText className='w-100 search-input' id="email" value={searchValue} placeholder='Search here' onChange={(e) => handleChange(e)} />
                    <i className="pi pi-search position-absolute search-icon" style={{ color: '#708090' }}></i>
                </div>

            </div>
        </nav>
    );
};

export default TopNav;