using LeQuangTrungMVC.BusinessObjects.Models;

namespace LeQuangTrungMVC.DataAccessObjects
{
    public class TagDAO : GenericDAO<Tag>
    {
        private static TagDAO? _instance;

        public static TagDAO Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TagDAO();

                return _instance;
            }
        }

        public TagDAO() : base(new()) { }
    }
}
