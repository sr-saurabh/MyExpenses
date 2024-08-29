using MyExpenses.Domain.core.Models.GroupExpense;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyExpenses.Application.Abstraction
{
    /// <summary>
    /// Defines the contract for managing group expense shares.
    /// </summary>
    public interface IGroupExpenseShareContract
    {
        /// <summary>
        /// Adds shares to a group expense.
        /// </summary>
        /// <param name="expenseShares">A list of <see cref="CreateExpenseShare"/> objects representing the shares to add.</param>
        /// <param name="groupExpenseId">The ID of the group expense to which the shares are to be added.</param>
        /// <param name="payerId">The ID of the user paying for the shares.</param>
        /// <returns><c>true</c> if the shares were successfully added; otherwise, <c>false</c>.</returns>
        Task<bool> AddGroupExpenseShare(List<CreateExpenseShare> expenseShares, int groupExpenseId, int payerId);

        /// <summary>
        /// Deletes shares from a group expense based on their IDs.
        /// </summary>
        /// <param name="expenseShareIds">A list of IDs representing the shares to delete.</param>
        /// <param name="payerId">The ID of the user who is deleting the shares.</param>
        /// <returns><c>true</c> if the shares were successfully deleted; otherwise, <c>false</c>.</returns>
        Task<bool> DeleteGroupExpenseShare(List<int> expenseShareIds, int payerId);

        /// <summary>
        /// Deletes shares from a group expense based on the receiver IDs.
        /// </summary>
        /// <param name="expenseSharesReceiverId">A list of receiver IDs for the shares to delete.</param>
        /// <param name="groupExpenseId">The ID of the group expense from which the shares are to be deleted.</param>
        /// <param name="payerId">The ID of the user who is deleting the shares.</param>
        /// <returns><c>true</c> if the shares were successfully deleted; otherwise, <c>false</c>.</returns>
        Task<bool> DeleteGroupExpenseShare(List<int> expenseSharesReceiverId, int groupExpenseId, int payerId);

        /// <summary>
        /// Updates existing shares in a group expense.
        /// </summary>
        /// <param name="expenseShares">A list of <see cref="UpdateExpenseShare"/> objects representing the updated shares.</param>
        /// <param name="groupExpenseId">The ID of the group expense containing the shares to update.</param>
        /// <param name="payerId">The ID of the user performing the update.</param>
        /// <returns><c>true</c> if the shares were successfully updated; otherwise, <c>false</c>.</returns>
        Task<bool> UpdateGroupExpenseShare(List<UpdateExpenseShare> expenseShares, int groupExpenseId, int payerId);

        /// <summary>
        /// Updates existing shares in a group expense with new share details.
        /// </summary>
        /// <param name="createExpenseShares">A list of <see cref="CreateExpenseShare"/> objects representing the new share details.</param>
        /// <param name="groupExpenseId">The ID of the group expense containing the shares to update.</param>
        /// <param name="payerId">The ID of the user performing the update.</param>
        /// <returns><c>true</c> if the shares were successfully updated; otherwise, <c>false</c>.</returns>
        Task<bool> UpdateGroupExpenseShare(List<CreateExpenseShare> createExpenseShares, int groupExpenseId, int payerId);
    }
}
