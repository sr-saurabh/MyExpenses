using AutoMapper;
using System.Transactions;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Models.UserExpense;
using MyExpenses.Domain.core.Repositories;

namespace MyExpenses.Application.Providers
{
    /// <summary>
    /// Provides functionalities for managing user expenses.
    /// </summary>
    public class UserExpenseProvider : IUserExpenseContract
    {
        private readonly IMapper _mapper;
        private readonly IUserExpenseRepo _userExpenseRepo;
        private readonly IContactRepo _contactRepo;

        public UserExpenseProvider(IMapper mapper, IUserExpenseRepo userExpenseRepo, IContactRepo contactRepo)
        {
            _mapper = mapper;
            _userExpenseRepo = userExpenseRepo;
            _contactRepo = contactRepo;
        }

        /// <summary>
        /// Adds a new user expense and updates contact balances.
        /// </summary>
        /// <param name="userExpense">The expense to add.</param>
        /// <returns>The added user expense.</returns>
        public async Task<ApiUserExpense> AddUserExpense(CreateUserExpense userExpense)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var userExpenseModel = _mapper.Map<UserExpense>(userExpense);

                    // Adding data to userExpense table
                    var result = await _userExpenseRepo.CreateAsync(userExpenseModel);

                    // Updating the balance in the contact table
                    var isBalanceUpdated = await UpdateContactBalance(userExpense.FromUserId, userExpense.ToUserId, userExpense.Amount, 0);

                    if (!result || !isBalanceUpdated)
                    {
                        transaction.Dispose(); // Rollback the transaction
                        return null;
                    }

                    transaction.Complete(); // Commit the transaction

                    return _mapper.Map<ApiUserExpense>(userExpenseModel);
                }
                catch (Exception)
                {
                    transaction.Dispose(); // Rollback the transaction
                    throw; // Optionally rethrow the exception
                }
            }
        }

        /// <summary>
        /// Deletes a user expense and updates contact balances.
        /// </summary>
        /// <param name="id">The ID of the expense to delete.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        public async Task<bool> DeleteUserExpense(int id)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    // Fetch the user expense to be deleted
                    var userExpense = await _userExpenseRepo.GetByIdAsync(id);

                    // Update the balance in the contact table
                    var isBalanceUpdated = await UpdateContactBalance(userExpense.FromUserId, userExpense.ToUserId, 0, userExpense.Amount);

                    // Delete the user expense record
                    var res = await _userExpenseRepo.DeleteAsync(id);

                    if (res && isBalanceUpdated)
                    {
                        transaction.Complete(); // Commit the transaction
                        return true;
                    }

                    transaction.Dispose(); // Rollback the transaction
                    return false;
                }
                catch (Exception)
                {
                    transaction.Dispose(); // Rollback the transaction
                    throw; // Optionally rethrow the exception
                }
            }
        }

        /// <summary>
        /// Gets a specific user expense by ID.
        /// </summary>
        /// <param name="id">The ID of the expense.</param>
        /// <returns>The requested user expense.</returns>
        public async Task<ApiUserExpense> GetUserExpense(int id)
        {
            var userExpense = await _userExpenseRepo.GetByIdAsync(id);
            return _mapper.Map<ApiUserExpense>(userExpense);
        }

        /// <summary>
        /// Gets user expenses and a summary of total amounts between two users.
        /// </summary>
        /// <param name="fromUserId">The ID of the user who initiated the expense.</param>
        /// <param name="toUserId">The ID of the user who received the expense.</param>
        /// <returns>A summary of the expenses and total amounts.</returns>
        public async Task<ApiUserExpenseWithSummary> GetUserExpenses(int fromUserId, int toUserId)
        {
            var fromUserExpenses = _userExpenseRepo.Search(x => x.FromUserId == fromUserId && x.ToUserId == toUserId).ToList();
            var toUserExpenses = _userExpenseRepo.Search(x => x.FromUserId == toUserId && x.ToUserId == fromUserId).ToList();

            var fromUserAmount = fromUserExpenses.Sum(x => x.Amount);
            var toUserAmount = toUserExpenses.Sum(x => x.Amount);

            var fromApiUserExpenses = _mapper.Map<List<ApiUserExpense>>(fromUserExpenses);
            var toApiUserExpenses = _mapper.Map<List<ApiUserExpense>>(toUserExpenses);

            ApiUserExpenseWithSummary apiUserExpenseWithSummary = new ApiUserExpenseWithSummary
            {
                ApiUserExpenses = fromApiUserExpenses.Concat(toApiUserExpenses).ToList(),
                TotalAmount = fromUserAmount - toUserAmount
            };

            return apiUserExpenseWithSummary;
        }

        /// <summary>
        /// Updates a user expense and the corresponding contact balances.
        /// </summary>
        /// <param name="userExpense">The expense to update.</param>
        /// <returns>True if the operation succeeded, otherwise false.</returns>
        public async Task<bool> UpdateUserExpense(UpdateUserExpense userExpense)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var previousUserExpense = await _userExpenseRepo.GetByIdAsync(userExpense.Id);
                    if (previousUserExpense == null)
                    {
                        return false;
                    }

                    var isBalanceUpdated = true;
                    var isUpdated = true;

                    if (userExpense.FromUserId != previousUserExpense.FromUserId || userExpense.ToUserId != previousUserExpense.ToUserId)
                    {
                        isBalanceUpdated &= await UpdateContactBalance(previousUserExpense.FromUserId, previousUserExpense.ToUserId, 0, previousUserExpense.Amount);
                        isBalanceUpdated &= await UpdateContactBalance(userExpense.FromUserId, userExpense.ToUserId, userExpense.Amount, 0);
                    }
                    else
                    {
                        isBalanceUpdated &= await UpdateContactBalance(userExpense.FromUserId, userExpense.ToUserId, userExpense.Amount, previousUserExpense.Amount);
                    }

                    isUpdated &= await _userExpenseRepo.UpdateAsync(ue => ue.Id == userExpense.Id,
                                         ue => ue.SetProperty(ue => ue.Amount, userExpense.Amount)
                                                 .SetProperty(ue => ue.Description, userExpense.Description)
                                                 .SetProperty(ue => ue.Category, userExpense.Category)
                                                 .SetProperty(ue => ue.Date, userExpense.Date)
                                                 .SetProperty(ue => ue.FromUserId, userExpense.FromUserId)
                                                 .SetProperty(ue => ue.ToUserId, userExpense.ToUserId));

                    if (isUpdated && isBalanceUpdated)
                    {
                        transaction.Complete(); // Commit the transaction
                        return true;
                    }

                    transaction.Dispose(); // Rollback the transaction
                    return false;
                }
                catch (Exception)
                {
                    transaction.Dispose(); // Rollback the transaction
                    throw; // Optionally rethrow the exception
                }
            }
        }

        /// <summary>
        /// Updates the balance between two users in the contact table.
        /// </summary>
        /// <param name="fromUserId">The ID of the user who initiated the balance change.</param>
        /// <param name="toUserId">The ID of the user who received the balance change.</param>
        /// <param name="currentAmount">The current amount being updated.</param>
        /// <param name="previousAmount">The previous amount being replaced.</param>
        /// <returns>True if the balance was updated successfully, otherwise false.</returns>
        public async Task<bool> UpdateContactBalance(int fromUserId, int toUserId, decimal currentAmount, decimal previousAmount)
        {
            var isBalanceUpdated = await _contactRepo.UpdateAsync(c => c.FromUserId == fromUserId && c.ToUserId == toUserId,
                                                          c => c.SetProperty(c => c.Balance, c => c.Balance - previousAmount + currentAmount));

            if (!isBalanceUpdated)
            {
                isBalanceUpdated = await _contactRepo.UpdateAsync(c => c.ToUserId == fromUserId && c.FromUserId == toUserId,
                            c => c.SetProperty(c => c.Balance, c => c.Balance + previousAmount - currentAmount));
            }

            return isBalanceUpdated;
        }
    }
}
