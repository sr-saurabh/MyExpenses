import React, { Children, FC, useEffect, useState } from 'react';
import './AccountForm.css';
import { Button } from 'primereact/button';
import { InputText } from 'primereact/inputtext';
import { AccountModel, AccountType } from '../../Models/AccountModel.ts';
import { InputNumber } from 'primereact/inputnumber';
import { Dropdown, DropdownChangeEvent } from 'primereact/dropdown';

interface FormError {
  accountName?: string;
  accountNumber?: string;
  branchName?: string;
  ifsc?: string;
  accountType?: string;
  balance?: string;
}
interface AccountFormProps {
  account?: AccountModel,
  isUpdate: boolean,
  onSubmit: (data: { accountData: AccountModel }) => void;
}

const AccountForm: FC<AccountFormProps> = ({ account, isUpdate = false, onSubmit }) => {
  const initialFormData: AccountModel = {
    id: 0,
    userId: 0,
    accountName: '',
    accountNumber: '',
    branchName: '',
    ifsc: '',
    accountType:AccountType.Saving,
    balance: 0
  }

  const [formData, setFormData] = useState(initialFormData);

  const [formError, setFormError] = useState<FormError>({});
  const [selectedType, setSelectedType] = useState({name:'Saving', id:1});

  const accountType = Object.keys(AccountType)
    .filter((key) => isNaN(Number(key))) // Filter out numeric keys (reverse mapping in enums)
    .map((key) => ({
      name: key,
      id: AccountType[key as keyof typeof AccountType]
    }));


  useEffect(() => {
    if (account != undefined) {
      var formData: AccountModel = {
        id: account.id,
        userId: account.userId,
        accountName: account.accountName,
        accountNumber: account.accountNumber,
        branchName: account.branchName,
        ifsc: account.ifsc,
        accountType: account.accountType??AccountType.Saving,
        balance: account.balance
      }
      setFormData(formData);
      var x = accountType.find(acc => acc.id == account.accountType);

      if (x) {
        setSelectedType(x);
      }
    }

  }, [account]);

  const handleChange = (e) => {
    if (e.target === undefined) {
      const { id, value } = e.originalEvent.target;
      setFormData({ ...formData, [id]: value });
      return;
    }
    const { id, value } = e.target;
    setFormData({ ...formData, [id]: value });
  }

  const onAccountTypeChange = (e: DropdownChangeEvent) => {
    console.log(e)
    const id = e.target.id;
    const value = e.target.value.name;
    setSelectedType(e.value)
    setFormData({ ...formData, [id]: value });
  }
  
  const handleSubmit = (e) => {
    e.preventDefault();
    const formError = validateForm(formData);
    if (formError) {
      setFormError(formError);
      if (Object.keys(formError).length > 0)
        return;
      else {
        console.log(formError)
        console.log(formData)
      }
    }
    console.log("The form has no error");
    onSubmit({ accountData: formData });
  }

  const validateForm = (formData: AccountModel) => {
    const formError: FormError = {};
    console.log(formData)
    if (!formData.accountName) {
      formError.accountName = 'Account Name is required';
    }
    if (!formData.accountNumber || formData.accountNumber === "0") {
      formError.accountNumber = 'Account Number is required';
    }
    if (!formData.branchName) {
      formError.branchName = 'Branch Name is required';
    }
    if (!formData.ifsc) {
      formError.ifsc = 'IFSC Code is required';
    }
    if (!formData.accountType) {
      formError.accountType = 'Account Type is required';
    }
    if (formData.balance === undefined || formData.balance < 0) {
      formError.balance = 'Balance must be a positive number';
    }

    return formError;
  }

  return (
    <div className=''>
      <form className='d-flex flex-column  justify-content-center' onSubmit={handleSubmit}>
        <div className='mb-3 mt-2'>
          <div className="flex flex-column gap-2">
            <label htmlFor="accountName" className='text-black fw-medium'>Account Name</label>
            <InputText className='w-100' id="accountName" value={formData.accountName} invalid={!!formError?.accountName} placeholder='Account Name' onChange={(e) => handleChange(e)} />
          </div>
          <div className='text-danger'>{formError?.accountName}</div>
        </div>
        <div className='mb-3 mt-2'>
          <div className="flex flex-column gap-2">
            <label htmlFor="accountNumber" className='text-black fw-medium'>Account Number</label>
            <InputNumber
              className='w-100'
              inputId="accountNumber"
              id="accountNumber"
              value={Number(formData.accountNumber)}
              onChange={(e) => handleChange(e)}
              useGrouping={false} invalid={!!formError?.accountNumber}
              placeholder='Account Number' />
          </div>
          <div className='text-danger'>{formError?.accountNumber}</div>
        </div>

        <div className='mb-3 mt-2'>
          <div className="flex flex-column gap-2">
            <label htmlFor="branchName" className='text-black fw-medium'>Branch Name</label>
            <InputText className='w-100' id="branchName" value={formData.branchName} invalid={!!formError?.branchName} placeholder='Branch Name' onChange={(e) => handleChange(e)} />
          </div>
          <div className='text-danger'>{formError?.branchName}</div>
        </div>
        <div className='mb-3 mt-2'>
          <div className="flex flex-column gap-2">
            <label htmlFor="ifsc" className='text-black fw-medium'>IFSC Code</label>
            <InputText className='w-100' id="ifsc" value={formData.ifsc} invalid={!!formError?.ifsc} placeholder='IFSC Code' onChange={(e) => handleChange(e)} />
          </div>
          <div className='text-danger'>{formError?.ifsc}</div>
        </div>
        <div className='mb-3 mt-2 '>
          <label htmlFor="accountType" className='text-black fw-medium'>Account Type</label>
          <Dropdown id='accountType' value={selectedType} focusOnHover={false} panelClassName='category-dropdown' onChange={(e: DropdownChangeEvent) => onAccountTypeChange(e)} options={accountType} optionLabel="name"
            placeholder="Select a Category" className="w-100 md:w-14rem" />
          <div className='text-danger'>{formError?.accountType}</div>

        </div>
        <div className='mb-3 mt-2'>
          <div className="flex flex-column gap-2">
            <label htmlFor="balance" className='text-black fw-medium'>Balance</label>
            <InputNumber
              className='w-100'
              inputId="balance"
              id="balance"
              value={Number(formData.balance)}
              onChange={(e) => handleChange(e)}
              useGrouping={false} invalid={!!formError?.balance}
              disabled={isUpdate}
              minFractionDigits={0}
              maxFractionDigits={3}
              placeholder='Balance' />
          </div>
          <div className='text-danger'>{formError?.balance}</div>
        </div>
        <Button label={'Submit'} className='rounded-3' iconPos='right' />
      </form>
    </div>
  );
}

export default AccountForm;
