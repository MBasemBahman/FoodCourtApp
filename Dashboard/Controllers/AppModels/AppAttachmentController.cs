namespace Dashboard.Controllers.AppModels
{
    public class AppAttachmentController : ExtendControllerBase
    {
        public AppAttachmentController(RepositoryManager Repository, IMapper Mapper, IHttpContextAccessor HttpContextAccessor,
     LocalizationService Localizer, LinkGenerator linkGenerator,
            IWebHostEnvironment environment) : base(Repository, Mapper, HttpContextAccessor, Localizer, linkGenerator, environment)
        {

        }

        public IActionResult Index(int Id, bool ProfileLayOut = false)
        {

            ViewData["Active"] = "AppAttachment";

            CommonFilter filter = new()
            {
                Id = Id,
            };

            ViewData["ProfileLayOut"] = ProfileLayOut;
            return View("~/Views/AppModels/AppAttachment/Index.cshtml", filter);
        }

        [HttpPost]
        public async Task<IActionResult> LoadTable([FromBody] CommonFilter dtParameters)
        {
            RequestParameters parameters = new()
            {
                SearchColumns = "Id"
            };

            _ = _Mapper.Map(dtParameters, parameters);

            PagedList<AppAttachmentDto> data = await _Repository.AppAttachment.GetAppAttachmentsPaged(parameters);

            List<AppAttachmentDto> resultDto = _Mapper.Map<List<AppAttachmentDto>>(data);

            DataTable<AppAttachmentDto> dataTableManager = new();

            DataTableResult<AppAttachmentDto> dataTableResult = dataTableManager.LoadTable(dtParameters, resultDto, data.MetaData.TotalCount, _Repository.AppAttachment.Count());

            return Json(dataTableManager.ReturnTable(dataTableResult));
        }


        public async Task<IActionResult> Delete(int id)
        {
            AppAttachment data = await _Repository.AppAttachment.FindById(id, trackChanges: false);

            return View("~/Views/AppModels/AppAttachment/Delete.cshtml", data != null);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            AppAttachment data = await _Repository.AppAttachment.FindById(id, trackChanges: false);

            _Repository.AppAttachment.Delete(data);
            await _Repository.Save();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Uploud()
        {
            IFormFile Images = HttpContext.Request.Form.Files["file"];
            if (Images != null)
            {
                string storageUrl = _linkGenerator.GetUriByAction(HttpContext).GetBaseUri(HttpContext.Request.RouteValues["controller"].ToString());

                AppAttachment AppAttachment = new()
                {
                    StorageUrl = storageUrl,
                    FileUrl = await _Repository.AppAttachment.UploadAppAttachments(_environment.WebRootPath, Images),
                    FileLength = Images.Length,
                    FileName = Images.FileName,
                    FileType = Images.ContentType,
                };

                _Repository.AppAttachment.Create(AppAttachment);
                await _Repository.Save();
            }
            return Ok();
        }
    }
}
