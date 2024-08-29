namespace MyExpenses.Domain.core.Entities.Enums
{
    /// <summary>
    /// Represents the status of a financial settlement.
    /// </summary>
    public enum SettlementStatus
    {
        /// <summary>
        /// The settlement is pending and has not yet been completed.
        /// </summary>
        Pending,

        /// <summary>
        /// The settlement has been completed.
        /// </summary>
        Completed
    }
}
