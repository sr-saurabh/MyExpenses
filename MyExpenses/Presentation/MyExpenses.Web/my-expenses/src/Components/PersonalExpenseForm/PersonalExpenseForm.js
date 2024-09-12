import React, { useEffect } from 'react'
import { useState } from 'react';
import { InputText } from 'primereact/inputtext';
import { FloatLabel } from 'primereact/floatlabel';
import { Button } from 'primereact/button';
import { Dropdown } from 'primereact/dropdown';
import { Calendar } from 'primereact/calendar';
import { InputNumber } from 'primereact/inputnumber';
import './PersonalExpenseForm.css';
import { getCategories } from '../../Services/PersonalExpenseService';
import { Carousel } from 'bootstrap';

export default function PersonalExpenseForm(props) {

    const [categories, setCategories] = useState([]);
    const [formData, setFormData] = useState({
        description: '',
        category: '',
        date: '',
        amount: null,
        transactionType: 0,
        appUserId: null
    });


    useEffect(() => {
        setFormData(props?.formData);
        getCategories(props.formData?.appUserId)
            .then((response) => {
                setCategories(response.data);
            })
            .catch((error) => {
                console.log(error);
            });
    }, []);

    const [formError, setFormError] = useState({});
    const handleChange = (e) => {
        if (e.target?.id === 'transactionType') {
            console.log(e.target);
            if (typeof e.target.value === 'object') {
                setFormData({ ...formData, [e.target.id]: e.target.value });
            }
            else
                setFormData({ ...formData, [e.target.id]: e.target.value });
        }
        else
            setFormData({ ...formData, [e.target?.id]: e.target.value });

            console.log(formData);
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const formError = vlaidateForm(formData);
        if (formError) {
            setFormError(formError);
            if (Object.keys(formError).length > 0)
                return;
        }
        props.onSubmit(formData);
    };
    const vlaidateForm = (formData) => {
        const formError = {};
        if (!formData.description) {
            formError.description = 'Description is required';
        }
        if (!formData.category) {
            formError.category = 'Category is required';
        }
        if (!formData.date) {
            formError.date = 'Date is required';
        }
        if (!formData.amount) {
            formError.amount = 'Amount is required';
        }
        if (!formData.transactionType) {
            formError.transactionType = 'Transaction Type is required';
        }

        return formError;
    };
    // const categories = [ 'New York', 'Rome', 'London', 'Istanbul', 'Paris'];
    // const transactionType = ['Debit', 'Credit'];
    const transactionType=[
        {name:'Credit', value:0},
        {name:'Debit', value:1},
    ]
    return (
        <div>
            <form className='d-flex flex-column  justify-content-center' onSubmit={handleSubmit}>
                <div className='mb-3 mt-4'>
                    <FloatLabel>
                        <InputText className='w-100' id="description" value={formData?.description} invalid={!formError.description === ""} onChange={(e) => handleChange(e)} />
                        <label htmlFor="description">Description</label>
                    </FloatLabel>
                    <div className='text-danger'>{formError?.description}</div>
                </div>
                <div className="flex justify-content-center mb-3 mt-2">
                    <FloatLabel>
                        <Dropdown value={formData.category} onChange={(e) => handleChange(e)} id='category' inputId='category' options={categories}
                            editable className="w-100 md:w-14rem expense-form-dropdown" />
                        <label htmlFor="category">Category</label>
                    </FloatLabel>
                    <div className='text-danger'>{formError?.category}</div>
                </div>
                <div className="flex justify-content-center mb-3 mt-2">
                    <FloatLabel>
                        <Calendar value={formData.date} id="date" inputId='date' className='w-100' onChange={(e) => handleChange(e)} />
                        <label htmlFor="date">Date</label>
                    </FloatLabel>
                    <div className='text-danger'>{formError?.date}</div>
                </div>
                <div className="flex justify-content-center mb-3 mt-2">
                    <FloatLabel>
                        <InputNumber id="amount" value={formData.amount} className='w-100' onValueChange={(e) => handleChange(e)} minFractionDigits={2} maxFractionDigits={2} />
                        <label htmlFor="amount">Amount</label>
                    </FloatLabel>
                    <div className='text-danger'>{formError?.amount}</div>
                </div>
                <div className="flex justify-content-center mt-2">
                    <FloatLabel>
                        <Dropdown value={formData.transactionType} onChange={(e) => handleChange(e)} id='transactionType' hea inputId='transactionType' options={transactionType} optionLabel='name' optionValue='value' className="w-100 md:w-14rem expense-form-dropdown" />
                        <label htmlFor="transactionType">Transaction Type</label>
                    </FloatLabel>
                    <div className='text-danger'>{formError?.transactionType}</div>
                </div>
                <Button label="Submit" severity='info' className='rounded-3 mt-2' iconPos='right' />
            </form>
        </div>
    )
}