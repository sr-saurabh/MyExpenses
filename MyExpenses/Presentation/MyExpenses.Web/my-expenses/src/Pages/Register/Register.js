import React from 'react'
import { InputText } from "primereact/inputtext";
import { FloatLabel } from "primereact/floatlabel";
import { Button } from 'primereact/button';
import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';

// import { Register } from '../../Services/authService';

export default function Register() {


    const initialFormData = {
        firstName: '',
        lastName: '',
        avatar: '',
        phoneNumber: '',
        monthlyBudget: '',
    };
    const [formData, setFormData] = useState(initialFormData);
    const [formError, setFormError] = useState({});

    const handleSubmit = (e) => {
        e.preventDefault();
        const formError = vlaidateForm(formData);
        if (formError) {
            setFormError(formError);
            if (Object.keys(formError).length > 0)
                return;
        }
    };

    const handleChange = (e) => {
        const { id, value } = e.target;
        setFormData({ ...formData, [id]: value });
    }

    const vlaidateForm = (formData) => {
        const formError = {};


        return formError;
    }

    return (
        <>
            <div className='login-container'>
                <div className='form-container d-flex flex-column bg-white rounded-3 opacity-75 overflow-hidden'>
                    <div className='form-description d-flex flex-column justify-content-end'>
                        <div className='p-3 pb-0 fw-medium'>
                            ExpenseManager is your ultimate tool for effortlessly managing and sharing expenses. Whether youre tracking personal finances, splitting bills with friends, or managing group expenses, ExpenseMate makes it easy. With a user-friendly interface, you can categorize your spending, set budgets, and view insightful reports to stay on top of your finances.
                        </div>
                    </div>
                    <div className='p-3'>
                        <form className='d-flex flex-column  justify-content-center' onSubmit={handleSubmit}>
                            <div className='mb-3 mt-2'>
                                <FloatLabel>
                                    <InputText className='w-100' id="firstName" value={formData.firstName} invalid={!formError.firstName === ""} onChange={(e) => handleChange(e)} />
                                    <label htmlFor="firstName">First Name</label>
                                </FloatLabel>
                                <div className='text-danger'>{formError?.email}</div>
                            </div>
                            <div className='my-3 password-container'>
                                <FloatLabel className='w-100'>
                                    <InputText className='w-100' id="lastName" value={formData.lastName} invalid={!formError.lastName === ""} onChange={(e) => handleChange(e)} />
                                    <label htmlFor="lastName">Last Name</label>
                                </FloatLabel>
                                <div className='text-danger'>{formError?.password}</div>
                            </div>
                            <Button label="Submit" severity='info' className='rounded-3' iconPos='right' />
                        </form>
                    </div>
                </div>
            </div>
        </>
    )
}
