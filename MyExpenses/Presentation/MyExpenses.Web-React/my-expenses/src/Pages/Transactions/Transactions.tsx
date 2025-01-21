import React, { useEffect, useState } from 'react';
import './Transactions.css';
import { Button } from 'primereact/button';
import { Dialog } from 'primereact/dialog';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import PersonalExpenseForm from '../../Components/PersonalExpenseForm/activityForm.tsx';
import { addPersonalExpense, getPersonalExpense, getPersonalExpenses } from '../../Services/PersonalExpenseService';
import { TransactionType } from '../../Models/Transactions.ts';
import moment from 'moment';
import ActivityForm from '../../Components/PersonalExpenseForm/activityForm.tsx';
import { Activity, CreateActivity } from '../../Models/ActivityModel.ts';

export default function Transactions() {
  var [expenses, setExpenses] = useState();
  var [profileData, setProfileData] = useState({});

  const [formData, setFormData] = useState<CreateActivity>({
    description: '',
    category: '',
    date: new Date(),
    amount: 0,
    type: 0,
    appUserId: 0,
    categoryId: 0,
    accountId: 0
  });
  const [visible, setVisible] = useState(false);

  useEffect(() => {
    const userProfile = localStorage.getItem('profileData');
    if (userProfile) {
      const parsedProfile = JSON.parse(userProfile);
      setProfileData(parsedProfile);
      setFormData({ ...formData, appUserId: parsedProfile.id });
      getExpenses(parsedProfile.id);
    }
  }, []);

  const getExpenses = (appUserId) => {
    getPersonalExpenses(appUserId)
      .then((response) => {
        setExpenses(response.data);
      })
      .catch((error) => {
        console.log(error);
      });
  }

  const handleFormSubmit = (formData) => {
    console.log(formData);
    setVisible(false);
    addPersonalExpense(formData)
      .then((response) => {
        setVisible(false);
        getExpenses(profileData.id);
      })
      .catch((error) => {
        console.log(error);
      });

  }

  const headerElement = (
    <div className="inline-flex align-items-center justify-content-center gap-2">
      <span className="font-bold white-space-nowrap">Add Expense</span>
    </div>
  );

  const paginatorLeft = <Button type="button" className='rounded-3' icon="pi pi-refresh" text />;
  const paginatorRight = <Button type="button" icon="pi pi-download" text />;

  const formatCurrency = (value: number) => {
    return value.toLocaleString('en-US', { style: 'currency', currency: 'INR' });
  };
  const amountBodyTemplate = (activity: Activity) => {
    return formatCurrency(activity.amount);
  };
  const dateBodyTemplate = (activity: Activity) => {
    return <> {moment(activity.date).format("DD MMM, yyyy")}</>;
  };
  const actionBodyTemplate = (activity: Activity) => {
    return <>
    <div className='d-flex gap-2'>

    <Button type="button" raised className='rounded-3 outline-none' icon="pi pi-pencil"/>
    <Button type="button" raised className='rounded-3 outline-none' icon="pi pi-trash"/>
    </div>
     </>;
  };

  return (
    <div className=''>
      <div className='d-flex justify-content-between'>
        <div className='ms-3 mt-3'>filter section</div>
        <div className='mt-3 me-3'>
          <Button label="Add Expense" icon="pi pi-plus" iconPos="right" className='rounded' onClick={() => setVisible(true)} />
          <Dialog header={headerElement} visible={visible} className='expense-form-container' headerClassName='pb-0' style={{ width: '50vw' }} onHide={() => { if (!visible) return; setVisible(false); }}>
            <ActivityForm formData={formData} onSubmit={handleFormSubmit} />
          </Dialog>
        </div>
      </div>
      <div className=' m-3 p-3 rounded-3 card data-container'>
        <DataTable value={expenses} paginator rows={5} rowsPerPageOptions={[5, 10, 25, 50]} tableStyle={{ minWidth: '50rem' }}
          paginatorTemplate="RowsPerPageDropdown FirstPageLink PrevPageLink CurrentPageReport NextPageLink LastPageLink"
          currentPageReportTemplate="{first} to {last} of {totalRecords}">
          <Column field="description" header="Description" style={{ width: '25%' }}></Column>
          <Column field="category" header="Category" style={{ width: '15%' }}></Column>
          <Column body={amountBodyTemplate} header="Amount" style={{ width: '20%' }}></Column>
          <Column body={dateBodyTemplate} header="Date" style={{ width: '20%' }}></Column>
          <Column field="transactionType" header="Transaction Type" style={{ width: '20%' }}></Column>
          <Column body={actionBodyTemplate} header="Action " style={{ width: '20%' }}></Column>
        </DataTable>
      </div>
    </div>
  )
}
