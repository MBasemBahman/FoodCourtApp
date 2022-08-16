using Entities.DBModels.AuthModels;
using Entities.DtoModels.AuthModels;
using Microsoft.AspNetCore.Http;
using Services;

namespace Repository.DBModels.AuthModels
{
    public class SystemUserRepository : RepositoryBase<SystemUser>
    {

        public SystemUserRepository(DBContext context) : base(context)
        {
        }

        public IQueryable<SystemUserDto> GetSystemUsers(RequestParameters parameters)
        {
            return FindAll(parameters, trackChanges: false)
                   .Select(a => new SystemUserDto
                   {
                       Id = a.Id,
                       Name = a.Name,
                       Password = a.Password,
                       UserName = a.UserName,
                       LastModifiedAtVal = a.LastModifiedAt,
                       LastModifiedBy = a.LastModifiedBy,
                       CreatedBy = a.CreatedBy,
                       CreatedAtVal = a.CreatedAt,
                   })
                   .Search(parameters.SearchColumns, parameters.SearchTerm)
                   .Sort(parameters.OrderBy);
        }

        public async Task<PagedList<SystemUserDto>> GetSystemUsersPaged(
          RequestParameters parameters)
        {
            return await PagedList<SystemUserDto>.ToPagedList(GetSystemUsers(parameters), parameters.PageNumber, parameters.PageSize);
        }

       

        public SystemUserDto GetSystemUserbyId(int id)
        {
            return GetSystemUsers(new RequestParameters { Id = id }).SingleOrDefault();
        }

        public IQueryable<SystemUser> FindAll(RequestParameters parameters, bool trackChanges)
        {
            return FindByCondition(a => (parameters.Id == 0 || a.Id == parameters.Id),trackChanges);
        }

        public async Task<SystemUser> FindById(int id, bool trackChanges)
        {
            return id == 0 ? null
                         : await FindByCondition(a => a.Id == id, trackChanges: trackChanges)
                         .SingleOrDefaultAsync();
        }
    }
    }
