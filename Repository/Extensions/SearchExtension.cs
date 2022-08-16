using Entities.DtoModels.AppModels;
using Entities.DtoModels.AuthModels;
using Entities.DtoModels.ShopModels;

namespace Repository.Extensions
{
    public static class SearchExtension
    {
        public static IQueryable<SystemUserDto> Search(this IQueryable<SystemUserDto> data, string searchColumns, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrWhiteSpace(searchColumns))
            {
                return data;
            }

            searchTerm = searchTerm.SafeTrim().SafeLower();

            Expression<Func<SystemUserDto, bool>> expression = SearchQueryBuilder.CreateSearchQuery<SystemUserDto>(searchColumns, searchTerm);

            return data.Where(expression);
        }
        public static IQueryable<BranchDto> Search(this IQueryable<BranchDto> data, string searchColumns, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrWhiteSpace(searchColumns))
            {
                return data;
            }

            searchTerm = searchTerm.SafeTrim().SafeLower();

            Expression<Func<BranchDto, bool>> expression = SearchQueryBuilder.CreateSearchQuery<BranchDto>(searchColumns, searchTerm);

            return data.Where(expression);
        }
        public static IQueryable<AppAttachmentDto> Search(this IQueryable<AppAttachmentDto> data, string searchColumns, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrWhiteSpace(searchColumns))
            {
                return data;
            }

            searchTerm = searchTerm.SafeTrim().SafeLower();

            Expression<Func<AppAttachmentDto, bool>> expression = SearchQueryBuilder.CreateSearchQuery<AppAttachmentDto>(searchColumns, searchTerm);

            return data.Where(expression);
        }
        public static IQueryable<ShopDto> Search(this IQueryable<ShopDto> data, string searchColumns, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrWhiteSpace(searchColumns))
            {
                return data;
            }

            searchTerm = searchTerm.SafeTrim().SafeLower();

            Expression<Func<ShopDto, bool>> expression = SearchQueryBuilder.CreateSearchQuery<ShopDto>(searchColumns, searchTerm);

            return data.Where(expression);
        }
        public static IQueryable<ShopGalleryDto> Search(this IQueryable<ShopGalleryDto> data, string searchColumns, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm) || string.IsNullOrWhiteSpace(searchColumns))
            {
                return data;
            }

            searchTerm = searchTerm.SafeTrim().SafeLower();

            Expression<Func<ShopGalleryDto, bool>> expression = SearchQueryBuilder.CreateSearchQuery<ShopGalleryDto>(searchColumns, searchTerm);

            return data.Where(expression);
        }
    }
}
