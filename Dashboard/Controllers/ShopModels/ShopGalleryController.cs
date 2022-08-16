namespace Dashboard.Controllers.ShopModels
{
    public class ShopGalleryController : ExtendControllerBase
    {

        public ShopGalleryController(RepositoryManager Repository, IMapper Mapper, IHttpContextAccessor HttpContextAccessor,
          LocalizationService Localizer, LinkGenerator linkGenerator,
                 IWebHostEnvironment environment) : base(Repository, Mapper, HttpContextAccessor, Localizer, linkGenerator, environment)
        {

        }

        public IActionResult Index(int Id, int Fk_Shop, bool ProfileLayOut = false)
        {

            ViewData["Active"] = "Shop";

            ShopGalleryFilter filter = new ShopGalleryFilter
            {
                Id = Id,
                Fk_Shop = Fk_Shop
            };

            ViewData["ProfileLayOut"] = ProfileLayOut;
            return View("~/Views/ShopModels/ShopGallery/Index.cshtml", filter);
        }

        [HttpPost]
        public async Task<IActionResult> LoadTable([FromBody] ShopGalleryFilter dtParameters)
        {
            ShopGalleryParameters parameters = new()
            {
                SearchColumns = "Id"
            };

            _Mapper.Map(dtParameters, parameters);

            PagedList<ShopGalleryDto> data = await _Repository.ShopGallery.GetShopGallerysPaged(parameters);

            List<ShopGalleryDto> resultDto = _Mapper.Map<List<ShopGalleryDto>>(data);

            DataTable<ShopGalleryDto> dataTableManager = new();

            DataTableResult<ShopGalleryDto> dataTableResult = dataTableManager.LoadTable(dtParameters, resultDto, data.MetaData.TotalCount, _Repository.ShopGallery.Count());

            return Json(dataTableManager.ReturnTable(dataTableResult));
        }

        public async Task<IActionResult> Edit(int id = 0)
        {
            ShopGalleryEditDto model = new();

            if (id > 0)
            {
                var dataDb = await _Repository.ShopGallery.FindById(id, trackChanges: false);
                model = _Mapper.Map<ShopGalleryEditDto>(dataDb);
            }

            return View("~/Views/ShopModels/ShopGallery/Edit.cshtml", model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ShopGalleryEditDto model)
        {

            if (!ModelState.IsValid)
            {

                return View("~/Views/ShopModels/ShopGallery/Edit.cshtml", model);
            }
            try
            {

                ShopGallery dataDB = new();

                dataDB = await _Repository.ShopGallery.FindById(id, trackChanges: true);

                _Mapper.Map(model, dataDB);

                await _Repository.Save();


                return  (IActionResult)RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewData[ViewDataConstants.Error] = ex.Message;
            }
            return View("~/Views/ShopModels/ShopGallery/Edit.cshtml", model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            ShopGallery data = await _Repository.ShopGallery.FindById(id, trackChanges: false);

            return View("~/Views/ShopModels/ShopGallery/Delete.cshtml", data != null);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ShopGallery data = await _Repository.ShopGallery.FindById(id, trackChanges: false);

            _Repository.ShopGallery.Delete(data);
            await _Repository.Save();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Uploud(int Fk_Shop)
        {
            IFormFile Images = HttpContext.Request.Form.Files["file"];
            if (Images != null)
            {
                string storageUrl = _linkGenerator.GetUriByAction(HttpContext).GetBaseUri(HttpContext.Request.RouteValues["controller"].ToString());

                ShopGallery ShopGallery = new()
                {
                    Fk_Shop = Fk_Shop,
                    StorageUrl = storageUrl,
                    ImageUrl = await _Repository.ShopGallery.UploadShopGalleryImages(_environment.WebRootPath,Images)
                };

                _Repository.ShopGallery.Create(ShopGallery);
                await _Repository.Save();
            }
            return Ok();
        }

       
    }
}
