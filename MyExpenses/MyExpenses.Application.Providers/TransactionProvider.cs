using AutoMapper;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Models.PersonalExpense;
using MyExpenses.Domain.core.Repositories;

namespace MyExpenses.Application.Providers
{
    public class TransactionProvider : ITransactionContract
    {
        private readonly IActivityRepo _activityRepo;
        private readonly ITransactionRepo _transactionRepo;
        private readonly IGoalRepo _goalRepo;
        private readonly IAccountRepo _accountRepo;
        private readonly IMapper _mapper;

        public TransactionProvider(IActivityRepo activityRepo, IMapper mapper, IGoalRepo goalRepo, IAccountRepo accountRepo, ITransactionRepo transactionRepo)
        {
            _activityRepo = activityRepo;
            _mapper = mapper;
            _goalRepo = goalRepo;
            _accountRepo = accountRepo;
            _transactionRepo = transactionRepo;
        }

        public async Task<List<ApiTransaction>> GetTransactions(int accountId)
        {
           var transactions= _transactionRepo.Search(t=>t.AccountId==accountId).ToList();
           return _mapper.Map<List<ApiTransaction>>(transactions);  
        }
    }
}
