import React from 'react'
import { InputText } from "primereact/inputtext";
import { Button } from 'primereact/button';
import { useState, useRef } from 'react';
import { Toast } from 'primereact/toast';
import { registerUser } from '../../Services/userServices.tsx';
import { useNavigate } from 'react-router-dom';

export default function Register() {

    const toast = useRef(null);
    const navigate = useNavigate();

    const initialFormData = {
        firstName: '',
        lastName: '',
        avatar: '',
        phoneNumber: '',
    };
    const [formData, setFormData] = useState(initialFormData);
    const [formError, setFormError] = useState({});

    const handleSubmit = (e) => {
        e.preventDefault();
        const formError = validateForm(formData);
        if (formError.length > 0) {
            console.log(formError);
            setFormError(formError);
            if (Object.keys(formError).length > 0)
                return;
        }
        else {
            console.log(formData);
            registerUser(formData).then((response)=>{
                console.log(response);
                if(response.status===200)
                {
                    navigate('/overview')
                }
            })
        }
    };

    const handleChange = (e) => {
        const { id, value } = e.target;
        setFormData({ ...formData, [id]: value });
    }

    const validateForm = (formData) => {
        const formError = {};

        if (!formData.firstName) {
            formError.firstName = 'First name is required';
        }
        if (!formData.lastName) {
            formError.lastName = 'Last name is required';
        }

        return formError;
    }

    return (
        <>
            <div className='login-container'>
                <Toast ref={toast} />

                <div className='form-container d-flex flex-column bg-white rounded-3 opacity-75 overflow-hidden'>
                    <div className='form-description d-flex flex-column justify-content-end'>
                        <div className='d-flex justify-content-center text-teal align-items-end user-select-none'>
                            <span className='fs-1 fw-bold'>MY E </span>
                            <span className='fs-1 fw-medium'>xpenses</span>
                        </div>
                    </div>
                    <div className='p-3'>
                        <form className='d-flex flex-column  justify-content-center' onSubmit={handleSubmit}>
                            {/* <div className='mb-3 mt-2'>
                                <FloatLabel>
                                    <InputText className='w-100' id="firstName" value={formData.firstName} invalid={!formError.firstName === ""} onChange={(e) => handleChange(e)} />
                                    <label htmlFor="firstName">First Name</label>
                                </FloatLabel>
                                <div className='text-danger'>{formError?.email}</div>
                            </div> */}
                            <div className='mb-3 mt-2'>
                                <div className="flex flex-column gap-2">
                                    <label htmlFor="firstName" className='text-black fw-medium'>First Name</label>
                                    <InputText className='w-100' id="firstName" value={formData.firstName} invalid={!formError.firstName === ""} onChange={(e) => handleChange(e)} />
                                </div>
                                <div className='text-danger'>{formError?.firstName}</div>
                            </div>
                            <div className='mb-3 mt-2'>
                                <div className="flex flex-column gap-2">
                                    <label htmlFor="lastName" className='text-black fw-medium'>Last Name</label>
                                    <InputText className='w-100' id="lastName" value={formData.lastName} invalid={!formError.lastName === ""} onChange={(e) => handleChange(e)} />
                                </div>
                                <div className='text-danger'>{formError?.lastName}</div>
                            </div>
                            {/* <div className='my-3 password-container'>
                                <FloatLabel className='w-100'>
                                    <InputText className='w-100' id="lastName" value={formData.lastName} invalid={!formError.lastName === ""} onChange={(e) => handleChange(e)} />
                                    <label htmlFor="lastName">Last Name</label>
                                </FloatLabel>
                                <div className='text-danger'>{formError?.password}</div>
                            </div> */}
                            <Button label="Submit" className='rounded-3' iconPos='right' />
                        </form>
                    </div>
                </div>
            </div>
        </>
    )
}
