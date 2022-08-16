namespace Dashboard.Controllers
{
    public class AuthenticationController : ExtendControllerBase
    {

        public AuthenticationController(RepositoryManager Repository, IMapper Mapper, IHttpContextAccessor HttpContextAccessor,
          LocalizationService Localizer, LinkGenerator linkGenerator,
                 IWebHostEnvironment environment) : base(Repository, Mapper, HttpContextAccessor, Localizer, linkGenerator, environment)
        {

        }



        public IActionResult Index()
        {
            string UserName = Request.Cookies["UserName"];
            if ((!string.IsNullOrEmpty(UserName)) && _Repository.SystemUser.FindByCondition(a => a.UserName == UserName,trackChanges:false).Any())
            {
                SystemUser systemUser = _Repository.SystemUser.FindByCondition(a => a.UserName == UserName,trackChanges:false).FirstOrDefault();
                SetCookies(systemUser);

                return Request.Headers.Referer.Any()
                        ? Redirect(Request.Headers.Referer)
                        : RedirectToAction("SetSettings", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(LoginViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SystemUser systemUser = _Repository.SystemUser.FindByCondition(a => a.UserName == model.UserName, trackChanges: false).FirstOrDefault();

                    if (systemUser == null || (systemUser != null && !BC.Verify(model.Password, systemUser.Password)))
                    {
                        throw new Exception("UserName or password is incorrect");
                    }
                    SetCookies(systemUser);

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewData["Error"] = _ExceptionHandler.GetException(ex);
            }
            return View(model);
        }



        public IActionResult Logout()
        {
            foreach (string Key in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(Key);
            }

            CookieOptions cookie = new()
            {
                Expires = DateTime.UtcNow.AddDays(-1)
            };

            return RedirectToAction(nameof(Index));
        }

        // helper methods

        private void SetCookies(SystemUser systemUser)
        {
            foreach (string Key in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(Key);
            }

            CookieOptions cookie = new()
            {
                Expires = DateTime.UtcNow.AddDays(14)
            };

            Response.Cookies.Append(ViewDataConstants.AccountName, systemUser.UserName, cookie);

        }

    }
}
