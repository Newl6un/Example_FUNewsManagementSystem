using LeQuangTrungMVC.BusinessLogicLayer.Interfaces;
using LeQuangTrungMVC.BusinessObjects.Constant;
using LeQuangTrungMVC.BusinessObjects.ExceptionModels;
using LeQuangTrungMVC.BusinessObjects.Models;
using LeQuangTrungMVC.Repository.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace LeQuangTrungMVC.BusinessLogicLayer.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<SystemAccount> GetAccount(string email, string password)
        {
            var account = await _accountRepository!.GetAccountAsync(email);

            if (account is null)
                throw new NotFoundException(AccountErrors.AccountNotFound);

            var f_password = GetMD5(password);

            if (!account!.AccountPassword.Equals(f_password))
                throw new UnauthorizedException(AccountErrors.WrongCredentials);

            return account;
        }

        public async Task CreateAccount(SystemAccount systemAccount)
        {
            var account = await _accountRepository!.GetAccountAsync(systemAccount.AccountEmail);

            if (account is not null)
                throw new BadRequestException(AccountErrors.EmailAlreadyExist);

            systemAccount.AccountPassword = GetMD5(systemAccount.AccountPassword);

            await _accountRepository.CreateAccount(systemAccount);

        }

        //create a string MD5
        private static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public async Task<IEnumerable<SystemAccount>> GetAccounts()
        {
            return await _accountRepository.GetAccountsAsync();
        }

        public async Task<SystemAccount?> GetAccountAsync(Guid id)
        {
            return await _accountRepository.GetAccountAsync(id);
        }

        public async Task UpdateAccount(Guid id, SystemAccount systemAccount)
        {
            var accountByInputEmail = await _accountRepository!.GetAccountAsync(systemAccount.AccountEmail);

            var accountById = await _accountRepository.GetAccountAsync(id);

            if (accountByInputEmail != null && !accountById.AccountId.Equals(accountByInputEmail!.AccountId))
            {
                throw new BadRequestException(AccountErrors.EmailAlreadyExist);
            }

            await _accountRepository.UpdateAccount(systemAccount);
        }

        public async Task DeleteAccount(Guid id)
        {
            var account = await _accountRepository!.GetAccountAsync(id);

            if (account is null)
                throw new BadRequestException(AccountErrors.AccountNotFound);

            await _accountRepository.DeleteAccount(account);
        }
    }
}
