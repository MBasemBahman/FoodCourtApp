using Entities.DtoModels.AppModels;
namespace Repository.Extensions
{
    public static class SearchExtension
    {
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
    }
}
