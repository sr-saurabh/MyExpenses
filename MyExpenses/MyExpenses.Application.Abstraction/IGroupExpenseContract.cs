using MyExpenses.Domain.core.Models.GroupExpense;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyExpenses.Application.Abstraction
{
    /// <summary>
    /// Defines the contract for managing group expenses.
    /// </summary>
    public interface IGroupExpenseContract
    {
        /// <summary>
        /// Adds a new group expense based on the provided details.
        /// </summary>
        /// <param name="groupExpenseRequest">The details of the group expense to add.</param>
        /// <returns><c>true</c> if the group expense was successfully added; otherwise, <c>false</c>.</returns>
        Task<bool> AddGroupExpense(CreateGroupExpense groupExpenseRequest);

        // /// <summary>
        // /// Adds a new share to an existing group expense.
        // /// </summary>
        // /// <param name="groupExpenseShareRequest">The details of the group expense share to add.</param>
        // /// <returns><c>true</c> if the group expense share was successfully added; otherwise, <c>false</c>.</returns>
        // Task<bool> AddGroupExpenseShare(GroupExpenseShareRequest groupExpenseShareRequest);

        /// <summary>
        /// Deletes a group expense by its ID.
        /// </summary>
        /// <param name="groupExpenseId">The ID of the group expense to delete.</param>
        /// <returns><c>true</c> if the group expense was successfully deleted; otherwise, <c>false</c>.</returns>
        Task<bool> DeleteGroupExpense(int groupExpenseId);

        // /// <summary>
        // /// Deletes a group expense share by its ID.
        // /// </summary>
        // /// <param name="groupExpenseShareId">The ID of the group expense share to delete.</param>
        // /// <returns><c>true</c> if the group expense share was successfully deleted; otherwise, <c>false</c>.</returns>
        // Task<bool> DeleteGroupExpenseShare(int groupExpenseShareId);

        /// <summary>
        /// Retrieves a list of group expenses for a specific group.
        /// </summary>
        /// <param name="groupId">The ID of the group whose expenses are to be retrieved.</param>
        /// <returns>A list of <see cref="ApiGroupExpense"/> objects representing the group's expenses.</returns>
        Task<List<ApiGroupExpense>> GetGroupExpenses(int groupId);

        /// <summary>
        /// Retrieves a specific group expense by its ID.
        /// </summary>
        /// <param name="groupExpenseId">The ID of the group expense to retrieve.</param>
        /// <returns>An <see cref="ApiGroupExpense"/> object representing the group expense.</returns>
        Task<ApiGroupExpense> GetGroupExpense(int groupExpenseId);

        // /// <summary>
        // /// Retrieves a list of shares for a specific group expense.
        // /// </summary>
        // /// <param name="groupExpenseId">The ID of the group expense whose shares are to be retrieved.</param>
        // /// <returns>A list of <see cref="GroupExpenseShareResponse"/> objects representing the group expense's shares.</returns>
        // Task<List<GroupExpenseShareResponse>> GetGroupExpenseShares(int groupExpenseId);

        /// <summary>
        /// Updates an existing group expense with the provided details.
        /// </summary>
        /// <param name="groupExpenseRequest">The updated group expense details.</param>
        /// <returns><c>true</c> if the group expense was successfully updated; otherwise, <c>false</c>.</returns>
        Task<bool> UpdateGroupExpense(UpdateGroupExpense groupExpenseRequest);

        // /// <summary>
        // /// Updates an existing share in a group expense with the provided details.
        // /// </summary>
        // /// <param name="groupExpenseShareRequest">The updated group expense share details.</param>
        // /// <returns><c>true</c> if the group expense share was successfully updated; otherwise, <c>false</c>.</returns>
        // Task<bool> UpdateGroupExpenseShare(GroupExpenseShareRequest groupExpenseShareRequest);
    }
}
