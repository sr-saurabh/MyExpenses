using AutoMapper;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Common;
using MyExpenses.Domain.core.Models.Accounts;
using MyExpenses.Domain.core.Repositories;

namespace MyExpenses.Application.Providers
{
    public class AccountProvider : IAccountContract
    {
        private readonly IAccountRepo _accountRepo;
        private readonly IMapper _mapper;
        public AccountProvider(IAccountRepo accountRepo, IMapper mapper)
        {
            _accountRepo = accountRepo;
            _mapper = mapper;
        }


        public async Task<ApiAccount> CreateAccount(CreateAccount CreateAccount)
        {
            var account = _mapper.Map<Account>(CreateAccount);
            var res = await _accountRepo.CreateAsync(account);
            if (res!=null)
                return _mapper.Map<ApiAccount>(res);
            throw new ArgumentException("Unable to create Account");


        }

        public async Task<ApiAccount> GetAccount(int accountId)
        {
            var account = await _accountRepo.GetByIdAsync(accountId);
            return _mapper.Map<ApiAccount>(account);
        }

        public async Task<List<ApiAccount>> GetAccounts(int userId)
        {
            var accounts= _accountRepo.Search(a=>a.AppUserId==userId).ToList();
            return _mapper.Map<List<ApiAccount>>(accounts);

        }

        public Task<bool> UpdateAccount(UpdateAccount UpdateAccount)
        {
            var account= _mapper.Map<Account>(UpdateAccount); 
            var res= _accountRepo.UpdateBankDetailsAsync(account);
            return res;
        }
        public Task<bool> DeleteAccount(int accountId)
        {
            var res= _accountRepo.DeleteAsync(accountId);
            return res;
        }
    }
}
