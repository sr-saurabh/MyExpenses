import React, { useEffect, useState } from 'react';
import './MyExpenses.css';
import { Button } from 'primereact/button';
import { Dialog } from 'primereact/dialog';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import PersonalExpenseForm from '../../Components/PersonalExpenseForm/PersonalExpenseForm';
import { addPersonalExpense, getPersonalExpense, getPersonalExpenses } from '../../Services/PersonalExpenseService';

export default function MyExpenses() {
  var [expenses, setExpenses] = useState([]);
  var [profileData, setProfileData] = useState({});

  const [formData, setFormData] = useState({
    description: '',
    category: '',
    date: '',
    amount: null,
    transactionType: 0,
    appUserId: 0
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

  const paginatorLeft = <Button type="button" icon="pi pi-refresh" text />;
  const paginatorRight = <Button type="button" icon="pi pi-download" text />;

  return (
    <div className=''>
      <div className='d-flex justify-content-between'>
        <div className='ms-3 mt-3'>filter section</div>
        <div className='mt-3 me-3'>
          <Button label="Add Expense" icon="pi pi-plus" iconPos="right" className='rounded' onClick={() => setVisible(true)} />
          <Dialog header={headerElement} visible={visible} className='expense-form-container' headerClassName='pb-0' style={{ width: '50vw' }} onHide={() => { if (!visible) return; setVisible(false); }}>
            <PersonalExpenseForm formData={formData} onSubmit={handleFormSubmit} />
          </Dialog>
        </div>
      </div>
      <div className=' m-3 p-3 rounded-3 data-container'>
        <DataTable value={expenses} paginator rows={5} rowsPerPageOptions={[5, 10, 25, 50]} tableStyle={{ minWidth: '50rem' }}
          paginatorTemplate="RowsPerPageDropdown FirstPageLink PrevPageLink CurrentPageReport NextPageLink LastPageLink"
          currentPageReportTemplate="{first} to {last} of {totalRecords}" paginatorLeft={paginatorLeft} paginatorRight={paginatorRight}>
          <Column field="category" header="Category" style={{ width: '25%' }}></Column>
          <Column field="amount" header="Amount" style={{ width: '25%' }}></Column>
          <Column field="date" header="Date" style={{ width: '25%' }}></Column>
          <Column field="type" header="Transaction Type" style={{ width: '25%' }}></Column>
        </DataTable>
      </div>
    </div>
  )
}
