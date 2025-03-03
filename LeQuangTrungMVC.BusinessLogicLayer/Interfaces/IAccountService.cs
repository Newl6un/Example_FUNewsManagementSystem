using LeQuangTrungMVC.BusinessObjects.Models;

namespace LeQuangTrungMVC.BusinessLogicLayer.Interfaces
{
    public interface IAccountService
    {
        Task<SystemAccount> GetAccount(string email, string password);

        Task<SystemAccount?> GetAccountAsync(Guid id);

        Task CreateAccount(SystemAccount systemAccount);

        Task<IEnumerable<SystemAccount>> GetAccounts();

        Task UpdateAccount(Guid id, SystemAccount systemAccount);

        Task DeleteAccount(Guid id);
    }
}
