namespace Dashboard.Controllers.AppModels
{
    public class BranchController : ExtendControllerBase
    {

        public BranchController(RepositoryManager Repository, IMapper Mapper, IHttpContextAccessor HttpContextAccessor,
          LocalizationService Localizer, LinkGenerator linkGenerator,
                 IWebHostEnvironment environment) : base(Repository, Mapper, HttpContextAccessor, Localizer, linkGenerator, environment)
        {

        }

        public IActionResult Index(int Id)
        {
            CommonFilter filter = new()
            {
                Id = Id
            };
            ViewData["Active"] = "Branch";


            return View("~/Views/AppModels/Branch/Index.cshtml", filter);
        }

        [HttpPost]
        public async Task<IActionResult> LoadTable([FromBody] CommonFilter dtParameters)
        {
            RequestParameters parameters = new()
            {
                SearchColumns = "Id,Name"
            };

            _ = _Mapper.Map(dtParameters, parameters);

            PagedList<BranchDto> data = await _Repository.Branch.GetBranchsPaged(parameters);

            List<BranchDto> resultDto = _Mapper.Map<List<BranchDto>>(data);

            DataTable<BranchDto> dataTableManager = new();

            DataTableResult<BranchDto> dataTableResult = dataTableManager.LoadTable(dtParameters, resultDto, data.MetaData.TotalCount, _Repository.Branch.Count());

            return Json(dataTableManager.ReturnTable(dataTableResult));
        }


        public IActionResult Details(int id)
        {
            ViewData["Active"] = "Branch";

            BranchDto Branch = _Repository.Branch.GetBranchbyId(id);

            ViewData["ProfileLayOut"] = true;

            return Branch == null ? NotFound() : View("~/Views/AppModels/Branch/Details.cshtml", Branch);
        }


        public IActionResult Profile(int id)
        {
            ViewData["Active"] = "Branch";

            bool otherLang = (bool)Request.HttpContext.Items[ApiConstants.Language];

            BranchDto Branch = _Repository.Branch.GetBranchbyId(id);

            ViewData[ApiConstants.Language] = otherLang;
            return Branch == null ? NotFound() : View("~/Views/AppModels/Branch/Profile.cshtml", Branch);
        }

        public async Task<IActionResult> CreateOrEdit(int id = 0, bool IsProfile = false)
        {
            BranchCreateOrEditDto model = new();

            if (id > 0)
            {
                Branch dataDb = await _Repository.Branch.FindById(id, trackChanges: false);
                model = _Mapper.Map<BranchCreateOrEditDto>(dataDb);
            }

            ViewData["IsProfile"] = IsProfile;
            ViewData["id"] = id;

            return View("~/Views/AppModels/Branch/CreateOrEdit.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(int id, BranchCreateOrEditDto model, bool IsProfile = false)
        {

            if (!ModelState.IsValid)
            {
                ViewData["IsProfile"] = IsProfile;
                ViewData["id"] = id;
                return View("~/Views/AppModels/Branch/CreateOrEdit.cshtml", model);
            }
            try
            {

                Branch dataDB = new();
                if (id == 0)
                {
                    dataDB = _Mapper.Map<Branch>(model);

                    dataDB.CreatedBy = Request.Cookies[ViewDataConstants.AccountName];

                    _Repository.Branch.Create(dataDB);
                }
                else
                {
                    dataDB = await _Repository.Branch.FindById(id, trackChanges: true);

                    _ = _Mapper.Map(model, dataDB);

                    dataDB.LastModifiedBy = Request.Cookies[ViewDataConstants.AccountName];
                }


                IFormFile imageFile = HttpContext.Request.Form.Files["ImageFile"];

                string storageUrl = _linkGenerator.GetUriByAction(HttpContext).GetBaseUri(HttpContext.Request.RouteValues["controller"].ToString());
                if (imageFile != null)
                {
                    dataDB.ImageUrl = await _Repository.Branch.UploadBranchImage(_environment.WebRootPath, imageFile);
                    dataDB.StorageUrl = storageUrl;
                }

                await _Repository.Save();


                return IsProfile ? RedirectToAction(nameof(Profile), new { id }) : (IActionResult)RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData[ViewDataConstants.Error] = ex.Message;
            }
            ViewData["IsProfile"] = IsProfile;
            ViewData["id"] = id;

            return View("~/Views/AppModels/Branch/CreateOrEdit.cshtml", model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Branch data = await _Repository.Branch.FindById(id, trackChanges: false);

            return View("~/Views/AppModels/Branch/Delete.cshtml", data != null && !_Repository.Shop.FindAll(new
                ShopParameters
            { Fk_Branch = id }, trackChanges: false).Any());
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Branch data = await _Repository.Branch.FindById(id, trackChanges: false);

            _Repository.Branch.Delete(data);
            await _Repository.Save();

            return RedirectToAction(nameof(Index));
        }

    }
}
