namespace Dashboard.Controllers.ShopModels
{
    public class ShopController : ExtendControllerBase
    {

        public ShopController(RepositoryManager Repository, IMapper Mapper, IHttpContextAccessor HttpContextAccessor,
          LocalizationService Localizer, LinkGenerator linkGenerator,
                 IWebHostEnvironment environment) : base(Repository, Mapper, HttpContextAccessor, Localizer, linkGenerator, environment)
        {

        }

        public IActionResult Index(int Id,int Fk_Branch, bool ProfileLayOut = false)
        {
            
            ViewData["Active"] = "Shop";

            ShopFilter filter = new ShopFilter
            {
                Id = Id,
                Fk_Branch = Fk_Branch
            };

            ViewData["ProfileLayOut"] = ProfileLayOut;
            return View("~/Views/ShopModels/Shop/Index.cshtml",filter);
        }

        [HttpPost]
        public async Task<IActionResult> LoadTable([FromBody] ShopFilter dtParameters)
        {
            ShopParameters parameters = new()
            {
                SearchColumns = "Id,Name"
            };

            _Mapper.Map(dtParameters, parameters);

            PagedList<ShopDto> data = await _Repository.Shop.GetShopsPaged(parameters);

            List<ShopDto> resultDto = _Mapper.Map<List<ShopDto>>(data);

            DataTable<ShopDto> dataTableManager = new();

            DataTableResult<ShopDto> dataTableResult = dataTableManager.LoadTable(dtParameters, resultDto, data.MetaData.TotalCount, _Repository.Shop.Count());

            return Json(dataTableManager.ReturnTable(dataTableResult));
        }


        public IActionResult Details(int id)
        {
            ViewData["Active"] = "Shop";

            ShopDto Shop = _Repository.Shop.GetShopbyId(id);

            ViewData["ProfileLayOut"] = true;

            return Shop == null ? NotFound() : View("~/Views/ShopModels/Shop/Details.cshtml", Shop);
        }


        public IActionResult Profile(int id)
        {
            ViewData["Active"] = "Shop";

            bool otherLang = (bool)Request.HttpContext.Items[ApiConstants.Language];

            ShopDto Shop = _Repository.Shop.GetShopbyId(id);

            ViewData[ApiConstants.Language] = otherLang;

            return Shop == null ? NotFound() : View("~/Views/ShopModels/Shop/Profile.cshtml", Shop);
        }

        public async Task<IActionResult> CreateOrEdit(int id = 0, bool IsProfile = false)
        {
            ShopCreateOrEditDto model = new();

            if (id > 0)
            {
                var dataDb = await _Repository.Shop.FindById(id, trackChanges: false);
                model = _Mapper.Map<ShopCreateOrEditDto>(dataDb);
            }

            SetViewData(IsProfile,id);

            return View("~/Views/ShopModels/Shop/CreateOrEdit.cshtml", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(int id, ShopCreateOrEditDto model, bool IsProfile = false)
        {

            if (!ModelState.IsValid)
            {
                SetViewData(IsProfile,id);
                return View("~/Views/ShopModels/Shop/CreateOrEdit.cshtml", model);
            }
            try
            {

                Shop dataDB = new();
                if (id == 0)
                {
                    dataDB = _Mapper.Map<Shop>(model);

                    dataDB.CreatedBy = Request.Cookies[ViewDataConstants.AccountName];

                    _Repository.Shop.Create(dataDB);
                }
                else
                {
                    dataDB = await _Repository.Shop.FindById(id, trackChanges: true);

                    _Mapper.Map(model, dataDB);

                    dataDB.LastModifiedBy = Request.Cookies[ViewDataConstants.AccountName];
                }


                IFormFile imageFile = HttpContext.Request.Form.Files["ImageFile"];

                string storageUrl = _linkGenerator.GetUriByAction(HttpContext).GetBaseUri(HttpContext.Request.RouteValues["controller"].ToString());
                if (imageFile != null)
                {
                    dataDB.ImageUrl = await _Repository.Shop.UploadShopImage(_environment.WebRootPath, imageFile);
                    dataDB.StorageUrl = storageUrl;
                }

                await _Repository.Save();


                return IsProfile ? RedirectToAction(nameof(Profile), new { id }) : (IActionResult)RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData[ViewDataConstants.Error] = ex.Message;
            }
            SetViewData(IsProfile,id);
            return View("~/Views/ShopModels/Shop/CreateOrEdit.cshtml", model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Shop data = await _Repository.Shop.FindById(id, trackChanges: false);

            return View("~/Views/ShopModels/Shop/Delete.cshtml", data != null);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Shop data = await _Repository.Shop.FindById(id, trackChanges: false);

            _Repository.Shop.Delete(data);
            await _Repository.Save();

            return RedirectToAction(nameof(Index));
        }

        private void SetViewData(bool IsProfile,int id)
        {
            ViewData["IsProfile"] = IsProfile;
            ViewData["id"] = id;

            ViewData["Branches"] = _Repository.Branch.GetBranchesLoopUp(new RequestParameters());
        }

    }
}
