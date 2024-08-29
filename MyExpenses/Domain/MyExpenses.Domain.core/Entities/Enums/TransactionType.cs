namespace MyExpenses.Domain.core.Entities.Enums
{
    /// <summary>
    /// Represents the type of a financial transaction.
    /// </summary>
    public enum TransactionType
    {
        /// <summary>
        /// A credit transaction, indicating money added to an account.
        /// </summary>
        Credit,

        /// <summary>
        /// A debit transaction, indicating money have been spend some where.
        /// </summary>
        Debit
    }
}
