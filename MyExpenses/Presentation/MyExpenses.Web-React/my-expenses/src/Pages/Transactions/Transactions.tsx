import React, { useEffect, useRef, useState } from 'react';
import './Transactions.css';
import { Button } from 'primereact/button';
import { Dialog } from 'primereact/dialog';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import PersonalExpenseForm from '../../Components/PersonalExpenseForm/activityForm.tsx';
import { addPersonalExpense, deletePersonalExpense, getPersonalExpense, getPersonalExpenses, updatePersonalExpense } from '../../Services/PersonalExpenseService';
import { TransactionType } from '../../Models/Transactions.ts';
import moment from 'moment';
import ActivityForm from '../../Components/PersonalExpenseForm/activityForm.tsx';
import { Activity, CreateActivity, UpdateActivityModel } from '../../Models/ActivityModel.ts';

import { ConfirmDialog } from 'primereact/confirmdialog'; // For <ConfirmDialog /> component
import { confirmDialog } from 'primereact/confirmdialog'; // For confirmDialog method
import { Toast } from 'primereact/toast';


export default function Transactions() {
  var [expenses, setExpenses] = useState();
  var [profileData, setProfileData] = useState({});
  var [isUpdate, setIsUpdate] = useState(false);

  const [formData, setFormData] = useState<CreateActivity>({} as CreateActivity);

  const [selectedActivity, setSelectedActivity] = useState<Activity>();
  const [visible, setVisible] = useState(false);
  const toast = useRef<Toast>(null);

  useEffect(() => {
    const userProfile = localStorage.getItem('profileData');
    if (userProfile) {
      const parsedProfile = JSON.parse(userProfile);
      setProfileData(parsedProfile);
      setupFormData(parsedProfile.id);
      getExpenses(parsedProfile.id);
    }
  }, []);

  
  const setupFormData=(appUserId: number, formdata?: any)=>{
    setFormData({
      description: '',
      category: '',
      date: new Date(),
      amount: 0,
      type: 0,
      appUserId: appUserId,
      categoryId: 0,
      accountId: 0
    })
  }
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
    if (!isUpdate) {
      addPersonalExpense(formData)
        .then((response) => {
          setVisible(false);
          getExpenses(profileData.id);
        })
        .catch((error) => {
          console.log(error);
        });
    }
    else {
      console.log(formData);
      // return;
      const newActivity: UpdateActivityModel = { ...formData, id: selectedActivity?.id ?? 0 };
      updatePersonalExpense(selectedActivity?.id, newActivity)
        .then((response) => {
          setVisible(false);
          getExpenses(profileData.id);
        })
        .catch((error) => {
          console.log(error);
        });
    }
  }

  const headerElement = (
    <div className="inline-flex align-items-center justify-content-center gap-2">
      <span className="font-bold white-space-nowrap">Add Expense</span>
    </div>
  );


  const formatCurrency = (value: number) => {
    return value.toLocaleString('en-US', { style: 'currency', currency: 'INR' });
  };

  const amountBodyTemplate = (activity: Activity) => {
    return formatCurrency(activity.amount);
  };

  const dateBodyTemplate = (activity: Activity) => {
    return <> {moment(activity.date).format("DD MMM, yyyy")}</>;
  };

  const transactionTypeBodyTemplate = (activity: Activity) => {
    return <> {TransactionType[activity.transactionType]}</>;
  };

  const actionBodyTemplate = (activity: Activity) => {
    return <>
      <div className='d-flex gap-2'>

        <Button type="button" onClick={() => updateActivity(activity)} raised className='rounded-3 outline-none' icon="pi pi-pencil" />
        <Button type="button" raised onClick={()=>confirm2(activity)} className='rounded-3 outline-none' icon="pi pi-trash" />
      </div>
    </>;
  };

  const updateActivity = (activity: Activity) => {

    setFormData({
      ...formData,
      appUserId: profileData.id,
      description: activity.description,
      category: activity.category,
      date: new Date(activity.date),
      amount: activity.amount,
      type: activity.transactionType,
      categoryId: activity.categoryId,
      accountId: activity.accountId

    });
    setSelectedActivity(activity);
    setVisible(true);
    setIsUpdate(true);
  }
  const deleteActivity = (activityId:number) => {
    deletePersonalExpense(activityId).then((res)=>{
      if(res.status===200)
      {
        toast.current?.show({ severity: 'success', summary: 'Confirmed', detail: 'Activity deleted successfully', life: 3000 });
        getExpenses(profileData.id);
      }
    })
  }

  const reject = () => {
    toast.current?.show({ severity: 'warn', summary: 'Rejected', detail: 'You have rejected', life: 3000 });
  }
  const confirm2 = (activity:Activity) => {
    setSelectedActivity(activity);
    confirmDialog({
      message: 'Do you want to delete this activity?',
      header: 'Delete Activity',
      icon: 'pi pi-trash fs-14px',
      defaultFocus: 'reject',
      acceptLabel: 'Delete',
      acceptClassName: 'p-button-danger rounded-3',
      rejectLabel:'Cancel',
      rejectClassName:'rounded-3 me-3',
      draggable:false,
      focusOnShow:false,
      accept: () => deleteActivity(activity.id),
      reject
    });
  };
  return (
    <div className=''>
      <div className='d-flex justify-content-between'>
        <div className='ms-3 mt-3'>filter section</div>
        <div className=''>
          <Button label="Add Expense" icon="pi pi-plus" iconPos="right" className='rounded' onClick={() => {setVisible(true); setupFormData(profileData.id)}} />
          <Dialog header={headerElement} visible={visible} className='expense-form-container' headerClassName='pb-0' style={{ width: '50vw' }} onHide={() => { if (!visible) return; setVisible(false); }}>
            <ActivityForm formData={formData} onSubmit={handleFormSubmit} />
          </Dialog>
        </div>
      </div>
      <div className=' m-3 me-0  p-3 rounded-3 card data-container'>
        <DataTable value={expenses} paginator rows={5} rowsPerPageOptions={[5, 10, 25, 50]} tableStyle={{ minWidth: '50rem' }}
          paginatorTemplate="RowsPerPageDropdown FirstPageLink PrevPageLink CurrentPageReport NextPageLink LastPageLink"
          currentPageReportTemplate="{first} to {last} of {totalRecords}">
          <Column field="description" header="Description" style={{ width: '25%' }}></Column>
          <Column field="category" header="Category" style={{ width: '15%' }}></Column>
          <Column body={amountBodyTemplate} header="Amount" style={{ width: '20%' }}></Column>
          <Column body={dateBodyTemplate} header="Date" style={{ width: '20%' }}></Column>
          <Column field={transactionTypeBodyTemplate} header="Transaction Type" style={{ width: '20%' }}></Column>
          <Column body={actionBodyTemplate} header="Action " style={{ width: '20%' }}></Column>
        </DataTable>
      </div>

      <ConfirmDialog />
      <Toast ref={toast} />


    </div>
  )
}
