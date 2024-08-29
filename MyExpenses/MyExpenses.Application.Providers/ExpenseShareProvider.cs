using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Enums;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Entities.Relationships;
using MyExpenses.Domain.core.Models.GroupExpense;
using MyExpenses.Domain.core.Repositories;
using System.Data.Entity;
using System.Transactions;

namespace MyExpenses.Application.Providers
{
    /// <summary>
    /// Provides functionality for managing and updating group expense shares.
    /// </summary>
    public class ExpenseShareProvider : IGroupExpenseShareContract
    {
        private readonly IGroupExpenseShareRepo _groupExpenseShareRepo;
        private readonly IContactRepo _contactRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExpenseShareProvider"/> class.
        /// </summary>
        /// <param name="groupExpenseShareRepo">The repository for handling group expense share operations.</param>
        /// <param name="contactRepo">The repository for handling contact operations.</param>
        public ExpenseShareProvider(IGroupExpenseShareRepo groupExpenseShareRepo, IContactRepo contactRepo)
        {
            _groupExpenseShareRepo = groupExpenseShareRepo;
            _contactRepo = contactRepo;
        }

        /// <summary>
        /// Adds a list of group expense shares and updates the balance of contacts.
        /// </summary>
        /// <param name="expenseShares">A list of <see cref="CreateExpenseShare"/> objects representing the shares to be added.</param>
        /// <param name="groupExpenseId">The ID of the group expense.</param>
        /// <param name="payerId">The ID of the payer.</param>
        /// <returns>True if all expense shares were successfully added and balances updated; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown if an error occurs during the process.</exception>
        public async Task<bool> AddGroupExpenseShare(List<CreateExpenseShare> expenseShares, int groupExpenseId, int payerId)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var isAdded = true;

                try
                {
                    foreach (var item in expenseShares)
                    {
                        GroupExpenseShare expenseShare = new GroupExpenseShare()
                        {
                            ShareAmount = item.ShareAmount,
                            ReceiverId = item.ReceiverId,
                            GroupExpenseId = groupExpenseId
                        };
                        isAdded &= await _groupExpenseShareRepo.CreateAsync(expenseShare);

                        var isBalanceUpdated = await _contactRepo.UpdateAsync(c => c.FromUserId == payerId && c.ToUserId == item.ReceiverId,
                                     c => c.SetProperty(c => c.Balance, c => c.Balance + item.ShareAmount));
                        if (!isBalanceUpdated)
                            isBalanceUpdated = await _contactRepo.UpdateAsync(c => c.ToUserId == payerId && c.FromUserId == item.ReceiverId,
                                        c => c.SetProperty(c => c.Balance, c => c.Balance - item.ShareAmount));
                        if (!isBalanceUpdated && payerId != item.ReceiverId)
                        {
                            await _contactRepo.CreateAsync(new Contact()
                            {
                                FromUserId = payerId,
                                ToUserId = item.ReceiverId,
                                Balance = item.ShareAmount,
                                Status = ContactInvitationStatus.Accepted
                            });
                        }
                    }

                    if (isAdded)
                        transaction.Complete();
                    else
                        transaction.Dispose(); // Rollback the transaction
                }
                catch (Exception)
                {
                    transaction.Dispose(); // Rollback the transaction
                    throw; // Optionally rethrow the exception
                }

                return isAdded;
            }
        }

        /// <summary>
        /// Deletes a list of group expense shares and updates the balance of contacts.
        /// </summary>
        /// <param name="expenseShareIds">A list of IDs of the expense shares to be deleted.</param>
        /// <param name="payerId">The ID of the payer.</param>
        /// <returns>True if all expense shares were successfully deleted and balances updated; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown if an error occurs during the process.</exception>
        public async Task<bool> DeleteGroupExpenseShare(List<int> expenseShareIds, int payerId)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var isDeleted = true;

                try
                {
                    foreach (var item in expenseShareIds)
                    {
                        var itemToBeDeleted = await _groupExpenseShareRepo.GetByIdAsync(item);

                        if (itemToBeDeleted == null)
                        {
                            isDeleted = false;
                            break;
                        }

                        // Updating the balance of contacts from fromUser to toUser
                        var isBalanceUpdated = await _contactRepo.UpdateAsync(c => c.FromUserId == payerId && c.ToUserId == itemToBeDeleted.ReceiverId,
                                     c => c.SetProperty(c => c.Balance, c => c.Balance - itemToBeDeleted.ShareAmount));

                        // Updating the balance of contacts from toUser to fromUser if there is no relation b/w fromUser to toUser
                        if (!isBalanceUpdated)
                            await _contactRepo.UpdateAsync(c => c.ToUserId == payerId && c.FromUserId == itemToBeDeleted.ReceiverId,
                                        c => c.SetProperty(c => c.Balance, c => c.Balance + itemToBeDeleted.ShareAmount));
                        //finally deleting the expense share
                        await _groupExpenseShareRepo.DeleteAsync(item);
                    }

                    if (isDeleted)
                        transaction.Complete();
                    else
                        transaction.Dispose(); // Rollback the transaction
                }
                catch (Exception)
                {
                    transaction.Dispose(); // Rollback the transaction
                    throw; // Optionally rethrow the exception
                }

                return isDeleted;
            }
        }

        /// <summary>
        /// Deletes group expense shares based on receiver IDs and group expense ID, and updates the balance of contacts.
        /// </summary>
        /// <param name="expenseSharesReceiverId">A list of receiver IDs for which the shares are to be deleted.</param>
        /// <param name="groupExpenseId">The ID of the group expense.</param>
        /// <param name="payerId">The ID of the payer.</param>
        /// <returns>True if the group expense shares were successfully deleted and balances updated; otherwise, false.</returns>
        public async Task<bool> DeleteGroupExpenseShare(List<int> expenseSharesReceiverId, int groupExpenseId, int payerId)
        {
            var expenseShareIds = _groupExpenseShareRepo.Search(es => es.GroupExpenseId == groupExpenseId && expenseSharesReceiverId.Contains(es.ReceiverId)).Select(es => es.Id).ToList();
            return await DeleteGroupExpenseShare(expenseShareIds, payerId);
        }

        /// <summary>
        /// Updates a list of group expense shares and updates the balance of contacts.
        /// </summary>
        /// <param name="expenseShares">A list of <see cref="UpdateExpenseShare"/> objects representing the shares to be updated.</param>
        /// <param name="groupExpenseId">The ID of the group expense.</param>
        /// <param name="payerId">The ID of the payer.</param>
        /// <returns>True if all expense shares were successfully updated and balances adjusted; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown if an error occurs during the process.</exception>
        public async Task<bool> UpdateGroupExpenseShare(List<UpdateExpenseShare> expenseShares, int groupExpenseId, int payerId)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var isUpdated = true;

                try
                {
                    foreach (var item in expenseShares)
                    {
                        var previousItem = await _groupExpenseShareRepo.GetByIdAsync(item.Id);
                        isUpdated &= await _groupExpenseShareRepo.UpdateAsync(ge => ge.Id == item.Id,
                                                        ge => ge.SetProperty(ge => ge.ShareAmount, item.ShareAmount));

                        var isBalanceUpdated = await _contactRepo.UpdateAsync(c => c.FromUserId == payerId && c.ToUserId == item.ReceiverId,
                                                           c => c.SetProperty(c => c.Balance, c => c.Balance - previousItem.ShareAmount + item.ShareAmount));
                        if (!isBalanceUpdated)
                            await _contactRepo.UpdateAsync(c => c.ToUserId == payerId && c.FromUserId == item.ReceiverId,
                                        c => c.SetProperty(c => c.Balance, c => c.Balance + previousItem.ShareAmount - item.ShareAmount));
                    }

                    if (isUpdated)
                        transaction.Complete();
                    else
                        transaction.Dispose(); // Rollback the transaction
                }
                catch (Exception)
                {
                    transaction.Dispose(); // Rollback the transaction
                    throw; // Optionally rethrow the exception
                }

                return isUpdated;
            }
        }

        /// <summary>
        /// Updates group expense shares based on a list of create expense share objects and updates the balance of contacts.
        /// </summary>
        /// <param name="createExpenseShares">A list of <see cref="CreateExpenseShare"/> objects representing the shares to be updated.</param>
        /// <param name="groupExpenseId">The ID of the group expense.</param>
        /// <param name="payerId">The ID of the payer.</param>
        /// <returns>True if all expense shares were successfully updated and balances adjusted; otherwise, false.</returns>
        public async Task<bool> UpdateGroupExpenseShare(List<CreateExpenseShare> createExpenseShares, int groupExpenseId, int payerId)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var isUpdated = true;

                try
                {
                    foreach (var item in createExpenseShares)
                    {
                        var previousItem = _groupExpenseShareRepo.Search(es => es.GroupExpenseId == groupExpenseId && es.ReceiverId == item.ReceiverId).SingleOrDefault();

                        isUpdated &= await _groupExpenseShareRepo.UpdateAsync(ge => ge.GroupExpenseId == groupExpenseId && ge.ReceiverId == item.ReceiverId,
                                                        ge => ge.SetProperty(ge => ge.ShareAmount, item.ShareAmount));

                        var isBalanceUpdated = await _contactRepo.UpdateAsync(c => c.FromUserId == payerId && c.ToUserId == item.ReceiverId,
                                                           c => c.SetProperty(c => c.Balance, c => c.Balance - (previousItem.ShareAmount) + item.ShareAmount));
                        if (!isBalanceUpdated)
                            isBalanceUpdated = await _contactRepo.UpdateAsync(c => c.ToUserId == payerId && c.FromUserId == item.ReceiverId,
                                        c => c.SetProperty(c => c.Balance, c => c.Balance + (previousItem.ShareAmount ) - item.ShareAmount));
                    }

                    if (isUpdated)
                        transaction.Complete();
                    else
                        transaction.Dispose(); // Rollback the transaction
                }
                catch (Exception)
                {
                    transaction.Dispose(); // Rollback the transaction
                    throw; // Optionally rethrow the exception
                }

                return isUpdated;
            }
        }
    }
}
