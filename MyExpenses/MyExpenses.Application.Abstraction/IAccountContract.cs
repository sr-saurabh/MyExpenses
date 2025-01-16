using MyExpenses.Domain.core.Models.Accounts;

namespace MyExpenses.Application.Abstraction
{
    public interface IAccountContract
    {

        Task<List<ApiAccount>> GetAccounts(int userId);
        Task<ApiAccount> GetAccount(int accountId);

        Task<ApiAccount> CreateAccount(CreateAccount account);
        Task<bool> UpdateAccount(UpdateAccount account);
        Task<bool> DeleteAccount(int accountId);

    }
}
