using LeQuangTrungMVC.BusinessObjects.Models;

namespace LeQuangTrungMVC.DataAccessObjects
{
    public class CategoryDAO : GenericDAO<Category>
    {
        private static CategoryDAO? _instance;

        public static CategoryDAO Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CategoryDAO();

                return _instance;
            }
        }

        public CategoryDAO() : base(new()) { }
    }
}
