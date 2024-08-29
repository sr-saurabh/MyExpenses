using AutoMapper;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Models.GroupExpense;
using MyExpenses.Domain.core.Repositories;
using System.Data.Entity;
using System.Transactions;

namespace MyExpenses.Application.Providers
{
    /// <summary>
    /// Provides functionality for managing group expenses, including creating, updating, retrieving, and deleting group expenses.
    /// </summary>
    public class GroupExpenseProvider : IGroupExpenseContract
    {
        private readonly IGroupExpenseRepo _groupExpenseRepo;
        private readonly IGroupExpenseShareContract _groupExpenseShareContract;
        private readonly IGroupExpenseShareRepo _groupExpenseShareRepo;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupExpenseProvider"/> class.
        /// </summary>
        /// <param name="groupExpenseRepo">The repository for handling group expense operations.</param>
        /// <param name="groupExpenseShareContract">The contract for managing group expense shares.</param>
        /// <param name="mapper">The AutoMapper instance for mapping objects.</param>
        /// <param name="expenseShareRepo">The repository for handling group expense share operations.</param>
        public GroupExpenseProvider(IGroupExpenseRepo groupExpenseRepo, IGroupExpenseShareContract groupExpenseShareContract, IMapper mapper, IGroupExpenseShareRepo expenseShareRepo)
        {
            _groupExpenseRepo = groupExpenseRepo;
            _groupExpenseShareContract = groupExpenseShareContract;
            _mapper = mapper;
            _groupExpenseShareRepo = expenseShareRepo;
        }

        /// <summary>
        /// Adds a new group expense and its associated shares.
        /// </summary>
        /// <param name="groupExpenseRequest">The <see cref="CreateGroupExpense"/> object containing the details of the group expense to be added.</param>
        /// <returns>True if the group expense and its shares were successfully added; otherwise, false.</returns>
        public async Task<bool> AddGroupExpense(CreateGroupExpense groupExpenseRequest)
        {
            var groupExpense = _mapper.Map<GroupExpenses>(groupExpenseRequest);
            var res = await _groupExpenseRepo.CreateAsync(groupExpense);

            res &= await _groupExpenseShareContract.AddGroupExpenseShare(groupExpenseRequest.ExpenseShare, groupExpense.Id, groupExpense.PayerId);
            return res;
        }

        /// <summary>
        /// Deletes a group expense and its associated shares.
        /// </summary>
        /// <param name="groupExpenseId">The ID of the group expense to be deleted.</param>
        /// <returns>True if the group expense and its shares were successfully deleted; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown if the group expense is not found or if an error occurs during the delete operation.</exception>
        public async Task<bool> DeleteGroupExpense(int groupExpenseId)
        {
            var groupExpense = _groupExpenseRepo.Search(u => u.Id == groupExpenseId).Include(u => u.GroupExpenseShares).SingleOrDefault();
            var isDeleted = false;
            var groupExpenseShares = groupExpense?.GroupExpenseShares.ToList() ?? new List<GroupExpenseShare>();
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (groupExpense != null && groupExpenseShares.Count > 0)
                {
                    isDeleted = await _groupExpenseShareContract.DeleteGroupExpenseShare(groupExpenseShares.Select(ge => ge.Id).ToList(), groupExpense.PayerId);
                }
                var deletedItem = await _groupExpenseRepo.DeleteAsync(groupExpenseId);
                isDeleted &= deletedItem != null;
                if (isDeleted)
                    transaction.Complete();
                else
                    transaction.Dispose(); // Rollback the transaction
                return isDeleted;
            }
        }

        /// <summary>
        /// Retrieves a group expense by its ID.
        /// </summary>
        /// <param name="groupExpenseId">The ID of the group expense to retrieve.</param>
        /// <returns>An <see cref="ApiGroupExpense"/> object representing the group expense, or null if not found.</returns>
        public async Task<ApiGroupExpense> GetGroupExpense(int groupExpenseId)
        {
            var groupExpenses = await _groupExpenseRepo.GetGroupExpenses(groupExpenseId);
            return groupExpenses.Where(ge => ge.GroupExpenseId == groupExpenseId).SingleOrDefault();
        }

        /// <summary>
        /// Retrieves all group expenses for a given group ID.
        /// </summary>
        /// <param name="groupId">The ID of the group to retrieve expenses for.</param>
        /// <returns>A list of <see cref="ApiGroupExpense"/> objects representing the group expenses.</returns>
        public async Task<List<ApiGroupExpense>> GetGroupExpenses(int groupId)
        {
            var groupExpenses = await _groupExpenseRepo.GetGroupExpenses(groupId);
            return groupExpenses.Where(ge => ge.GroupId == groupId).ToList();
        }

        /// <summary>
        /// Updates an existing group expense and its associated shares.
        /// </summary>
        /// <param name="groupExpenseRequest">The <see cref="UpdateGroupExpense"/> object containing the updated details of the group expense.</param>
        /// <returns>True if the group expense and its shares were successfully updated; otherwise, false.</returns>
        /// <exception cref="Exception">Thrown if the group expense is not found.</exception>
        public async Task<bool> UpdateGroupExpense(UpdateGroupExpense groupExpenseRequest)
        {
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var isUpdated = true;

                var groupExpenses = _groupExpenseRepo.Search(u => u.Id == groupExpenseRequest.Id).Include(u => u.GroupExpenseShares).SingleOrDefault();
                if (groupExpenses == null)
                    throw new Exception("Group Expense not found");

                var currentDebitor = groupExpenseRequest.ExpenseShare.ToList();
                var previousDebitor = _groupExpenseShareRepo.Search(es => es.GroupExpenseId == groupExpenseRequest.Id).Select(es => new CreateExpenseShare()
                {
                    ReceiverId = es.ReceiverId,
                    ShareAmount = es.ShareAmount
                }).ToList();

                // List toBeAdded => items of currentDebitor that are not in previousDebitor
                var toBeAdded = currentDebitor.Where(newItem => !previousDebitor.Any(oldItem => oldItem.ReceiverId == newItem.ReceiverId)).ToList();

                // List toBeUpdated => common items from both the lists
                var toBeUpdated = currentDebitor.Where(newItem => previousDebitor.Any(oldItem => oldItem.ReceiverId == newItem.ReceiverId)).ToList();

                // List toBeDeleted => items of previousDebitor that are not in currentDebitor
                var toBeDeleted = previousDebitor.Where(oldItem => !currentDebitor.Any(newItem => newItem.ReceiverId == oldItem.ReceiverId)).ToList();

                if (toBeDeleted.Count > 0)
                {
                    isUpdated &= await _groupExpenseShareContract.DeleteGroupExpenseShare(toBeDeleted.Select(ge => ge.ReceiverId).ToList(), groupExpenses.Id, groupExpenseRequest.PayerId);
                }

                if (toBeUpdated.Count > 0)
                {
                    isUpdated &= await _groupExpenseShareContract.UpdateGroupExpenseShare(toBeUpdated, groupExpenses.Id, groupExpenseRequest.PayerId);
                }

                if (toBeAdded.Count > 0)
                {
                    isUpdated &= await _groupExpenseShareContract.AddGroupExpenseShare(toBeAdded, groupExpenses.Id, groupExpenses.PayerId);
                }

                isUpdated &= await _groupExpenseRepo.UpdateAsync(ge => ge.Id == groupExpenseRequest.Id,
                                            ge => ge.SetProperty(ge => ge.Amount, groupExpenseRequest.Amount)
                                                    .SetProperty(ge => ge.Category, groupExpenseRequest.Category)
                                                    .SetProperty(ge => ge.Description, groupExpenseRequest.Description)
                                                    .SetProperty(ge => ge.PayerId, groupExpenseRequest.PayerId)
                                                    .SetProperty(ge => ge.Date, groupExpenseRequest.Date));
                if (isUpdated)
                    transaction.Complete();
                else
                    transaction.Dispose(); // Rollback the transaction
                return isUpdated;
            }
        }
    }
}
