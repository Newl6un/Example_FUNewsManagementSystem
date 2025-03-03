using LeQuangTrungMVC.BusinessObjects.Models;

namespace LeQuangTrungMVC.DataAccessObjects
{
    public class AccountDAO : GenericDAO<SystemAccount>
    {
        private static AccountDAO? _instance;

        public static AccountDAO Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AccountDAO();

                return _instance;
            }
        }

        public AccountDAO() : base(new()) { }
    }

}
