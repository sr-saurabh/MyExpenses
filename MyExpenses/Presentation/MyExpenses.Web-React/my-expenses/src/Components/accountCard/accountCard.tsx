import React, { FC } from 'react';
import './accountCard.css';
import { AccountModel, AccountType } from '../../Models/AccountModel.ts';
import { Button } from 'primereact/button';

interface AccountCardProps {
  account: AccountModel
  onRemove?: () => void;
  onClick: (data: { accountId: number }) => void;
}

const AccountCard: FC<AccountCardProps> = ({ account, onRemove, onClick }) => {
  const onDetailsClick=()=>{
    onClick({accountId:account.id});
  }
  return (
    <div className='card gap-3'>
      <div className='d-flex justify-content-between align-items-center'>
        <p>{AccountType[account.accountType]}</p>
        <p>{account.accountName}</p>
      </div>
      <div className='d-flex flex-column gap-1'>
        <div>
          <p className="mb-0 text-highlight">{account.accountNumber}</p>
          <p className="mb-0 sub-text">Account Number</p>
        </div>
        <div>
          <p className="mb-0 text-highlight">Rs. {account.balance}</p>
          <p className="mb-0 sub-text">Total amount</p>
        </div>
      </div>
      <div className='d-flex justify-content-between align-items-center'>
        <span className='text-teal fs-14px fw-medium'>Remove</span>
        <Button label="Details" className='rounded-3' icon="pi pi-chevron-right" iconPos="right"  onClick={onDetailsClick}/>
      </div>
    </div>
  );

}
export default AccountCard;
