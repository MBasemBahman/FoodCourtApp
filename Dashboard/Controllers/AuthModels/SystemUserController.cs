namespace Dashboard.Controllers.AuthModels
{
    public class SystemUserController : ExtendControllerBase
    {

        public SystemUserController(RepositoryManager Repository, IMapper Mapper, IHttpContextAccessor HttpContextAccessor,
          LocalizationService Localizer, LinkGenerator linkGenerator,
                 IWebHostEnvironment environment) : base(Repository, Mapper, HttpContextAccessor, Localizer, linkGenerator, environment)
        {

        }

        public IActionResult Index(int Id)
        {
            CommonFilter filter = new CommonFilter
            {
                Id = Id
            };
            ViewData["Active"] = "SystemUser";


            return View("~/Views/AuthModels/SystemUser/Index.cshtml", filter);
        }

        [HttpPost]
        public async Task<IActionResult> LoadTable([FromBody] CommonFilter dtParameters)
        {
            RequestParameters parameters = new()
            {
                SearchColumns = "Id,UserName"
            };

            _Mapper.Map(dtParameters, parameters);

            PagedList<SystemUserDto> data = await _Repository.SystemUser.GetSystemUsersPaged(parameters);

            List<SystemUserDto> resultDto = _Mapper.Map<List<SystemUserDto>>(data);

            DataTable<SystemUserDto> dataTableManager = new();

            DataTableResult<SystemUserDto> dataTableResult = dataTableManager.LoadTable(dtParameters, resultDto, data.MetaData.TotalCount, _Repository.SystemUser.Count());

            return Json(dataTableManager.ReturnTable(dataTableResult));
        }


        public IActionResult Details(int id)
        {
            ViewData["Active"] = "SystemUser";

            SystemUserDto SystemUser = _Repository.SystemUser.GetSystemUserbyId(id);

            ViewData["ProfileLayOut"] = true;

            return SystemUser == null ? NotFound() : View("~/Views/AuthModels/SystemUser/Details.cshtml", SystemUser);
        }

        public async Task<IActionResult> CreateOrEdit(int id = 0)
        {
            SystemUserCreateOrEditDto model = new();

            if (id > 0)
            {
                var dataDb = await _Repository.SystemUser.FindById(id, trackChanges: false);
                model = _Mapper.Map<SystemUserCreateOrEditDto>(dataDb);
            }

            return View("~/Views/AuthModels/SystemUser/CreateOrEdit.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(int id, SystemUserCreateOrEditDto model)
        {

            if (!ModelState.IsValid)
            {
                return View("~/Views/AuthModels/SystemUser/CreateOrEdit.cshtml", model);
            }
            try
            {

                SystemUser dataDB = new();
                if (id == 0)
                {
                    dataDB = _Mapper.Map<SystemUser>(model);

                    dataDB.CreatedBy = Request.Cookies[ViewDataConstants.AccountName];

                    dataDB.Password = BC.HashPassword(model.Password);

                    _Repository.SystemUser.Create(dataDB);
                }
                else
                {
                    dataDB = await _Repository.SystemUser.FindById(id, trackChanges: true);

                    if (model.Password != dataDB.Password)
                    {
                        model.Password = BC.HashPassword(model.Password);
                    }
                    _Mapper.Map(model, dataDB);

                    dataDB.LastModifiedBy = Request.Cookies[ViewDataConstants.AccountName];
                }


                await _Repository.Save();


                return  (IActionResult)RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData[ViewDataConstants.Error] = ex.Message;
            }

            return View("~/Views/AuthModels/SystemUser/CreateOrEdit.cshtml", model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            SystemUser data = await _Repository.SystemUser.FindById(id, trackChanges: false);

            return View("~/Views/AuthModels/SystemUser/Delete.cshtml", data != null);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            SystemUser data = await _Repository.SystemUser.FindById(id, trackChanges: false);

            _Repository.SystemUser.Delete(data);
            await _Repository.Save();

            return RedirectToAction(nameof(Index));
        }

    }
}
