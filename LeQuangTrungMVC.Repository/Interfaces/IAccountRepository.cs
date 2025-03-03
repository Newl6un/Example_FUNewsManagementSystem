using LeQuangTrungMVC.BusinessObjects.Features;
using LeQuangTrungMVC.BusinessObjects.Models;

namespace LeQuangTrungMVC.Repository.Interfaces
{
    public interface IAccountRepository
    {
        public Task<SystemAccount?> GetAccountAsync(string email);

        Task<SystemAccount?> GetAccountAsync(Guid id);

        Task<IList<SystemAccount>> GetAccountsAsync();

        Task<PagedList<SystemAccount>> GetAccountsAsync(AccountParameters accountParameters);

        Task CreateAccount(SystemAccount systemAccount);

        Task UpdateAccount(SystemAccount systemAccount);

        Task DeleteAccount(SystemAccount systemAccount);
    }
}
