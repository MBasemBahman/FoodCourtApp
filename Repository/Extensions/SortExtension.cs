using Entities.DtoModels.AppModels;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions
{
    public static class SortExtension
    {
        public static IQueryable<BranchDto> Sort(this IQueryable<BranchDto> data, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return data.OrderBy(a => a.Id);
            }

            string orderQuery = OrderQueryBuilder.CreateOrderQuery<BranchDto>(orderByQueryString);

            return data.OrderBy(orderQuery);
        }

    }
}
