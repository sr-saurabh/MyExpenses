import { Button } from 'primereact/button';
import { Dialog } from 'primereact/dialog';
import React, { useContext, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import AccountCard from '../../Components/accountCard/accountCard.tsx';
import AccountForm from '../../Components/AccountForm/AccountForm.tsx';
import UserContext from '../../Context/UserContext.ts';
import { AccountModel } from '../../Models/AccountModel.ts';
import { AppUser } from '../../Models/AppUser';
import { addNewAccount, getAccounts } from '../../Services/accountService.tsx';
import './balances.css';


const Balances = () => {
  const navigate= useNavigate();
  const { user, setUser } = useContext(UserContext);
  const [accounts, setAccounts] = useState<AccountModel[] | null>(null);
  const [appUser, setAppUser] = useState<AppUser | null>(null)
  const [visible, setVisible] = useState<boolean>(false);
  const [accountFormHeader, setAccountFormHeader] = useState("Add Account");



  useEffect(() => {
    const userProfile = localStorage.getItem('profileData');
    if (userProfile !== undefined && userProfile !== null) {
      let appUser: AppUser = JSON.parse(userProfile);
      setAppUser(appUser);
      getAccounts(appUser.id).then((response: any) => {
        setAccounts(response.data);
      });
    }
  }, []);

  const showAccountForm = (isAdd: boolean = true) => {
    setAccountFormHeader(isAdd ? 'Add Account' : 'Update Account');
    setVisible(true);
  }

  const hideAccountForm = () => {
    setVisible(false);
  }

  const onAccountFormSubmit = (newAccountData: AccountModel) => {
    console.log(newAccountData)
    if (appUser) {
      newAccountData.appUserId = appUser.id;
      addNewAccount(newAccountData).then((response) => {
        console.log(response);
        getAccounts(appUser?.id).then((response: any) => {
          setAccounts(response.data);
        });
      })
    }
    // hideAccountForm();
  }
  const redirectToAccountDetails=(accountId:number)=>{
    navigate(`account/${accountId}`);
  }
  return (
    <div className={''}>
      <p className='card-header-text' key={'1'}>Balances</p>
      <div className="account-card-container d-flex flex-wrap gap-3">
        {accounts?.map((account) => (
          <div className='flex-grow-1' key={account.id}>
            <AccountCard account={account} onRemove={undefined} onClick={(data)=>{redirectToAccountDetails(data.accountId)}} />
          </div>
        ))}
        <div className='card flex-grow-1 d-flex flex-column justify-content-center align-items-center'>
          <Button label="Add Account" className='rounded-3' icon="pi pi-chevron-right" iconPos="right" onClick={() => showAccountForm()} />
          {/* <span className='sub-text mt-2 fw-medium cursor-pointer'>Edit Accounts</span> */}
        </div>
      </div>


      <Dialog header={accountFormHeader} visible={visible} style={{ width: 'clamp(20rem, 50vw, 50rem)' }} position='top' draggable={false} onHide={() => { if (!visible) return; hideAccountForm(); }}>
        <div className="px-3 pb-3">
          <AccountForm isUpdate={false} onSubmit={(data) => { onAccountFormSubmit(data.accountData) }}></AccountForm>
        </div>
      </Dialog>

    </div>
  );
}
export default Balances;
