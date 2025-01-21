import moment from 'moment';
import React, { Children, FC, useEffect, useState } from 'react';
import './AccountDetails.css';
import { useLocation, useNavigate, useNavigation, useParams } from 'react-router-dom';
import { getAccount } from '../../Services/accountService.tsx';
import { AccountModel, AccountType } from '../../Models/AccountModel.ts';
import { DataTable } from 'primereact/datatable';
import { Column } from 'primereact/column';
import { Transaction, TransactionType } from '../../Models/Transactions.ts';
import { getAllTransactions } from '../../Services/transactionService.tsx';
const AccountDetails = () => {
  // const [accountId, setAccountId] = useState<number | null>();
  // const location = useLocation();
  // const navigate = useNavigate();
  const { accountId } = useParams(); // Extract the accountId parameter
  const [accountDetails, setAccountDetails] = useState<AccountModel | null>(null);
  const [transactions, setTransactions] = useState<Transaction[]>([]);

  useEffect(() => {
    const fetchAccount = async () => {
      if (accountId) {
        try {
          const response = await getAccount(Number(accountId));
          setAccountDetails(response.data);

          const transactionResponse = await getAllTransactions(Number(accountId));
          setTransactions(transactionResponse.data);

          console.log(transactionResponse);
        } catch (error) {
          console.error('Error fetching account:', error);
        }


      }
    };
    fetchAccount();
  }, [accountId]);
  const formatCurrency = (value: number) => {
    return value.toLocaleString('en-US', { style: 'currency', currency: 'INR' });
  };
  const amountBodyTemplate = (transaction: Transaction) => {
    return formatCurrency(transaction.amount);
  };
  const dateBodyTemplate = (transaction: Transaction) => {
    return <> {moment(transaction.date).format("DD MMM, yyyy")}</>;
  };

  const transactionTypeBodyTemplate = (transaction: Transaction) => {
    return <>{TransactionType[transaction?.transactionType]}</>;
  };

  return (
    <div className={''}>
      <p className='card-header-text'>Account Details</p>
      <div className="card">
        <div className='d-flex gap-3 flex-wrap'>
          <div className='d-flex flex-column justify-content-between account-details-container flex-grow-1 gap-3'>
            <div>
              <p className="subtext mb-0">Bank Name</p>
              <span className="text-highlight">{accountDetails?.accountName}</span>
            </div>
            <div>
              <p className="subtext mb-0">Branch Name</p>
              <span className="text-highlight">{accountDetails?.branchName}</span>
            </div>
          </div>
          <div className='d-flex flex-column justify-content-between account-details-container flex-grow-1 gap-3'>
            <div>
              <p className="subtext mb-0">Account Type</p>
              <span className="text-highlight">{AccountType[accountDetails?.accountType]}</span>
            </div>
            <div>
              <p className="subtext mb-0">Account Number</p>
              <span className="text-highlight">{accountDetails?.accountNumber}</span>
            </div>
          </div>
          <div className='d-flex flex-column justify-content-between account-details-container flex-grow-1 gap-3'>
            <div>
              <p className="subtext mb-0">Balance</p>
              <span className="text-highlight">Rs. {accountDetails?.balance}</span>
            </div>
          </div>
        </div>
        <div className='action-button-container'></div>
      </div>
      <p className='card-header-text'>Account Details</p>
      <div className="card">
        <DataTable value={transactions} tableStyle={{ minWidth: '50rem' }}>
          <Column body={dateBodyTemplate} header="Date"></Column>
          <Column body={transactionTypeBodyTemplate} header="Transaction Type"></Column>
          <Column body={amountBodyTemplate} header="Amount"></Column>
        </DataTable>
      </div>
    </div>
  );
}


export default AccountDetails;
