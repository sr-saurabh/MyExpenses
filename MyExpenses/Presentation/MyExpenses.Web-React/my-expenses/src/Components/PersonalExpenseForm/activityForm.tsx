import React, { Children, FC, useEffect } from 'react'
import { useState } from 'react';
import { InputText } from 'primereact/inputtext';
import { FloatLabel } from 'primereact/floatlabel';
import { Button } from 'primereact/button';
import { Dropdown } from 'primereact/dropdown';
import { Calendar } from 'primereact/calendar';
import { InputNumber } from 'primereact/inputnumber';
import './activityForm.css';
import { getAllCategories } from '../../Services/sharedService.tsx';
import { AccountModel  } from '../../Models/AccountModel.ts';
import { CreateActivity } from '../../Models/ActivityModel.ts';
import { TransactionType } from '../../Models/Transactions.ts';
import { getAccounts } from '../../Services/accountService.tsx';

interface Category {
    categoryName: string;
    id: number;
}

interface ActivityProps {
    formData: CreateActivity;
    onSubmit: any;

}
interface ActivityFormError {
    description?: string,
    category?: string,
    date?: string,
    amount?: string,
    transactionType?: string,
    account?: string,
}

const ActivityForm: FC<ActivityProps> = ({ formData, onSubmit }: ActivityProps) => {

    const [categories, setCategories] = useState<Category[]>([]);
    const [selectedCategory, setSelectedCategory] = useState<Category | null>(null);
    const [formError, setFormError] = useState<ActivityFormError>({});
    const [accounts, setAccounts] = useState<AccountModel[]>([]);
    const [selectedAccount, setSelectedAccounts] = useState<AccountModel>({} as AccountModel);
    const [newFormData, setFormData] = useState<CreateActivity>({
        description: '',
        category: '',
        date: new Date(),
        amount: 0,
        type: 0,
        appUserId: 0,
        categoryId: 0,
        accountId: 0
    });

    const transactionType = [
        { name: 'Credit', value: 0 },
        { name: 'Debit', value: 1 },
    ]

    useEffect(() => {
        getAllCategories()
            .then((response) => {
                setCategories(response.data);
            })
            .catch((error) => {
                console.log(error);
            });

        getAccounts(formData.appUserId).then((response) => {
            setAccounts(response.data)
        })

    }, []);

    useEffect(() => {
        setFormData(formData);
    }, [formData]);

    const handleChange = (e: any) => {
        if (e.target?.id === 'transactionType') {
            if (typeof e.target.value === 'object') {
                setFormData({ ...newFormData, [e.target.id]: e.target.value });
            }
            else
                setFormData({ ...newFormData, [e.target.id]: e.target.value });
        }
        else
            setFormData({ ...newFormData, [e.target?.id]: e.target.value });
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const formError = validateForm(newFormData);
        if (formError) {
            setFormError(formError);
            if (Object.keys(formError).length > 0)
                return;
        }
        onSubmit(newFormData);
    };
    const validateForm = (formData) => {
        console.log(newFormData);
        const formError: ActivityFormError = {};
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
        if (!formData.type) {
            formError.transactionType = 'Activity Type is required';
        }
        if (!formData.accountId) {
            formError.account = 'Account is required';
        }

        return formError;
    };

    const onCategorySelected = (e: any) => {
        setSelectedCategory(e.value);
        setFormData({ ...newFormData, 'category': e.value.categoryName, 'categoryId': e.value.id })
    }

    const onAccountSelected = (e: any) => {
        console.log(e.value)
        setSelectedAccounts(e.value);
        setFormData({ ...newFormData, accountId: e.value.id})
    }

    return (
        <div>
            <form className='d-flex flex-column  justify-content-center' onSubmit={handleSubmit}>
                <div className='mt-4'>
                    <div className='mt-1'>
                        <label htmlFor="currentTargetAmount" className='text-black fw-medium'>Category</label>
                        <Dropdown value={selectedCategory || null} focusOnHover={false} panelClassName='category-dropdown' onChange={(e) => onCategorySelected(e)} options={categories} optionLabel="categoryName"
                            placeholder="Select a Category" className="w-100 md:w-14rem" />
                    </div>
                    <div className='text-danger fs-12px'>{formError?.category}</div>
                </div>
                <div className='mt-1'>
                    <div className="flex flex-column gap-2">
                        <label htmlFor="description" className='text-black fw-medium'>Description</label>
                        <InputText className='w-100' id="description" value={newFormData.description} placeholder='Description' onChange={(e) => handleChange(e)} />
                    </div>
                    <div className='text-danger fs-12px'>{formError?.description}</div>
                </div>
                <div className='mt-1'>
                    <div className="flex flex-column gap-2">
                        <label htmlFor="date" className='text-black fw-medium'>Date</label>
                        <Calendar value={newFormData.date} id="date" inputId='date' className='w-100' placeholder='Date' onChange={(e) => handleChange(e)} />
                    </div>
                    <div className='text-danger fs-12px'>{formError?.date}</div>
                </div>
                <div className='mt-1'>
                    <div className="flex flex-column gap-2">
                        <label htmlFor="amount" className='text-black fw-medium'>Amount</label>
                        <InputNumber id="amount" value={newFormData.amount} className='w-100' onValueChange={(e) => handleChange(e)} minFractionDigits={2} maxFractionDigits={2} mode="currency" currency="INR" currencyDisplay="code" locale="en-IN" />
                    </div>
                    <div className='text-danger fs-12px'>{formError?.amount}</div>
                </div>
                <div className='mt-1'>
                    <label htmlFor="transactionType">Activity Type</label>
                    <Dropdown value={newFormData.type} focusOnHover={false} placeholder='Select the activity type' onChange={(e) => handleChange(e)} id='type' inputId='type' options={transactionType} optionLabel='name' optionValue='value' className="w-100 md:w-14rem expense-form-dropdown" />
                    <div className='text-danger fs-12px'>{formError?.transactionType}</div>
                </div>
                <div className='mt-1'>
                    <label htmlFor="account">Account</label>
                    <Dropdown value={selectedAccount} focusOnHover={false} onChange={(e) => onAccountSelected(e)} id='account' inputId='account' options={accounts} optionLabel='accountName' className="w-100 md:w-14rem expense-form-dropdown" placeholder='Select an account'/>
                    <div className='text-danger fs-12px'>{formError?.account}</div>
                </div>
                <Button label="Submit" className='rounded-3 mt-2' iconPos='right' />
            </form>
        </div>
    )
}
export default ActivityForm;