using AutoMapper;
using LinqKit;
using MyExpenses.Application.Abstraction;
using MyExpenses.Domain.core.Entities.Enums;
using MyExpenses.Domain.core.Entities.Expenses;
using MyExpenses.Domain.core.Models.Expense;
using MyExpenses.Domain.core.Models.ExpenseFilter;
using MyExpenses.Domain.core.Models.PersonalExpense;
using MyExpenses.Domain.core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExpenses.Application.Providers
{
    public class PersonalExpenseProvider : IPersonalExpenseContract
    {
        private readonly IPersonalExpenseRepo _personalExpenseRepo;
        private readonly IMapper _mapper;

        public PersonalExpenseProvider(IPersonalExpenseRepo personalExpenseRepo, IMapper mapper)
        {
            _personalExpenseRepo = personalExpenseRepo;
            _mapper = mapper;
        } 


        /// <summary>
        /// Create the personal expense 
        /// </summary>
        /// <param name="expense"></param>
        /// <returns></returns>
        public async Task<CreatePersonalExpense> CreatePersonalExpenses(CreatePersonalExpense expense)
        {
            var personalExpense = _mapper.Map<PersonalExpenses>(expense);
            var result = await _personalExpenseRepo.CreateAsync(personalExpense);
            return _mapper.Map<CreatePersonalExpense>(personalExpense);
        }


        /// <summary>
        /// Get the personal expense based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ApiPersonalExpense> GetPersonalExpense(int id)
        {
            var personalExpense = await _personalExpenseRepo.GetByIdAsync(id);
            return _mapper.Map<ApiPersonalExpense>(personalExpense);
        }


        /// <summary>
        /// Get all the personal expenses of a particular user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ApiPersonalExpense>> GetPersonalExpenses(int userId)
        {

            var personalExpenses =await _personalExpenseRepo.Search(pe => pe.Id == userId).ToListAsync();
            return _mapper.Map<IEnumerable<ApiPersonalExpense>>(personalExpenses);  
        }

        /// <summary>
        /// Get the personal expenses based on the filter
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="filter"></param>
        /// <returns> </returns>
        public async Task<ApiPersonalExpenseWithSummary> GetPersonalExpenses(int userId, PersonalExpenseFilter filter)
        {
            //var predicate = PredicateBuilder.New<PersonalExpenses>(true);

            //predicate = predicate.And(e => e.Id == userId);
            //if (filter.DateFilter!=null && filter.DateFilter.ExpenseDate.HasValue)
            //{
            //    predicate = predicate.And(e => e.Date.Date == filter.DateFilter.ExpenseDate.Value.Date);
            //}

            //if (filter.DateFilter != null && filter.DateFilter.StartDate.HasValue && filter.DateFilter.EndDate.HasValue)
            //{
            //    predicate = predicate.And(e => e.Date.Date >= filter.DateFilter.StartDate.Value.Date && e.Date.Date <= filter.DateFilter.EndDate.Value.Date);
            //}

            //if (!string.IsNullOrEmpty(filter.Category))
            //{
            //    predicate = predicate.And(e => e.Category == filter.Category);
            //}

            //if (filter.Type != null && filter.Type.HasValue)
            //{
            //    predicate = predicate.And(e => e.Type == filter.Type.Value);
            //}

            //if (filter.AmountFilter != null && filter.AmountFilter.Amount.HasValue)
            //{
            //    predicate = predicate.And(e => e.Amount == filter.AmountFilter.Amount.Value);
            //}

            //if (filter.AmountFilter != null &&  filter.AmountFilter.MinAmount.HasValue && filter.AmountFilter.MaxAmount.HasValue)
            //{
            //    predicate = predicate.And(e => e.Amount >= filter.AmountFilter.MinAmount.Value && e.Amount <= filter.AmountFilter.MaxAmount.Value);
            //}

            //if (filter.Month != null && filter.Year != null && filter.Month.HasValue && filter.Year.HasValue)
            //{
            //    predicate = predicate.And(e => e.Date.Month == filter.Month.Value && e.Date.Year == filter.Year.Value);
            //}

            var result =(await _personalExpenseRepo.GetFilteredExpenses(userId, filter)).ToList();
            //var summary =await _personalExpenseRepo.GetPersonalExpenseSummary(userId, predicate);
            PersonalExpenseSummary summary = new PersonalExpenseSummary()
            {
                TotalSpent = result.Where(e => e.Type == TransactionType.Debit).Sum(e => e.Amount),
                TotalEarning = result.Where(e => e.Type == TransactionType.Credit).Sum(e => e.Amount)
            };
            var apiPersonalExpenses = _mapper.Map<List<ApiPersonalExpense>>(result);

            var apiPersonalExpenseWithSummary = new ApiPersonalExpenseWithSummary
            {
                Expenses = apiPersonalExpenses.ToList(),
                Summary = summary
            };
            return apiPersonalExpenseWithSummary;
            //var result = _personalExpenseRepo.Search(predicate).ToList();
            //return _context.Set<PersonalExpenses>().AsExpandable().Where(predicate);
        }


        /// <summary>
        /// Get the summary of the personal expenses of a user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PersonalExpenseSummary> GetPersonalExpenseSummary(int userId)
        {
            var result =await _personalExpenseRepo.GetPersonalExpenseSummary(userId);
            return result;
        }


        /// <summary>
        /// Update the existing personal expense
        /// </summary>
        /// <param name="expense"></param>
        /// <returns>Return true if updated successfully</returns>
        public async Task<bool> UpdatePersonalExpense(UpdatePersonalExpense expense)
        {
            var isUpdated = await _personalExpenseRepo.UpdateAsync(pe => pe.Id == expense.Id,
                                                                    pe => pe.SetProperty(p => p.Category, expense.Category)
                                                                            .SetProperty(p => p.Description, expense.Description)
                                                                            .SetProperty(p => p.Amount, expense.Amount)
                                                                            .SetProperty(p => p.Date, expense.Date)
                                                                            .SetProperty(p => p.Type, expense.Type));
            return isUpdated;
        }

        /// <summary>
        /// Delete the personal expense
        /// </summary>
        /// <param name="id"></param>
        /// <returns> Return true if Deleted successfully </returns>
        public async Task<bool> DeletePersonalExpense(int id)
        {
            var deletedExpense = await _personalExpenseRepo.DeleteAsync(id);
            return deletedExpense!=null;
        }
    }
}
