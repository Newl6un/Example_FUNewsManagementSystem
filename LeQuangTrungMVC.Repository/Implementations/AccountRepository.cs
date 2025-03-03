using LeQuangTrungMVC.BusinessObjects.Features;
using LeQuangTrungMVC.BusinessObjects.Models;
using LeQuangTrungMVC.DataAccessObjects;
using LeQuangTrungMVC.Repository.Extensions;
using LeQuangTrungMVC.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LeQuangTrungMVC.Repository.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        public async Task CreateAccount(SystemAccount systemAccount)
        {
            await AccountDAO.Instance.Create(systemAccount);
        }

        public async Task DeleteAccount(SystemAccount systemAccount)
        {
            await AccountDAO.Instance.Delete(systemAccount);
        }

        public async Task<SystemAccount?> GetAccountAsync(string email)
        {
            return await AccountDAO.Instance.FindByCondition(a => a.AccountEmail.Equals(email)).SingleOrDefaultAsync();
        }

        public async Task<SystemAccount?> GetAccountAsync(Guid id)
        {
            return await AccountDAO.Instance.FindByCondition(a => a.AccountId.Equals(id)).SingleOrDefaultAsync();
        }

        public async Task<IList<SystemAccount>> GetAccountsAsync()
        {
            return await AccountDAO.Instance.FindAll().ToListAsync();
        }

        public async Task<PagedList<SystemAccount>> GetAccountsAsync(AccountParameters accountParameters)
        {
            var accounts = await AccountDAO.Instance.FindAll()
                .Sort(accountParameters.OrderBy)
                .Skip((accountParameters.PageNumber - 1) * accountParameters.PageSize)
                .Take(accountParameters.PageSize)
                .ToListAsync();

            var count = await AccountDAO.Instance.FindAll()
                .CountAsync();


            return new PagedList<SystemAccount>(
                accounts,
                count,
                accountParameters.PageNumber,
                accountParameters.PageSize);
        }

        public async Task UpdateAccount(SystemAccount systemAccount)
        {
            await AccountDAO.Instance.Update(systemAccount);
        }
    }
}
