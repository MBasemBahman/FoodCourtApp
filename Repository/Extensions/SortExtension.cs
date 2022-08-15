using Entities.DtoModels.AppModels;
using Entities.DtoModels.ShopModels;
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
        public static IQueryable<AppAttachmentDto> Sort(this IQueryable<AppAttachmentDto> data, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return data.OrderBy(a => a.Id);
            }

            string orderQuery = OrderQueryBuilder.CreateOrderQuery<AppAttachmentDto>(orderByQueryString);

            return data.OrderBy(orderQuery);
        }
        public static IQueryable<ShopDto> Sort(this IQueryable<ShopDto> data, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return data.OrderBy(a => a.Id);
            }

            string orderQuery = OrderQueryBuilder.CreateOrderQuery<ShopDto>(orderByQueryString);

            return data.OrderBy(orderQuery);
        }
        public static IQueryable<ShopGalleryDto> Sort(this IQueryable<ShopGalleryDto> data, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return data.OrderBy(a => a.Id);
            }

            string orderQuery = OrderQueryBuilder.CreateOrderQuery<ShopGalleryDto>(orderByQueryString);

            return data.OrderBy(orderQuery);
        }

    }
}
