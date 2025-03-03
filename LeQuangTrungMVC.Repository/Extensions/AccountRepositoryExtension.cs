using LeQuangTrungMVC.BusinessObjects.Models;
using LeQuangTrungMVC.Repository.Utilities;
using System.Linq.Dynamic.Core;

namespace LeQuangTrungMVC.Repository.Extensions
{
    public static class AccountRepositoryExtension
    {
        public static IQueryable<SystemAccount> Sort(this IQueryable<SystemAccount> employees, string? orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString)) return employees.OrderBy(e => e.AccountName);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<SystemAccount>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return employees.OrderBy(e => e.AccountName);

            return employees.OrderBy(orderQuery);
        }
    }
}
