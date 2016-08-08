using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using kt.GmailWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace kt.GmailWeb.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _service;
        private readonly IAuthTokenService _authTokenService;

        public HomeController(IUserService service, IAuthTokenService authTokenService)
        {
            _service = service;
            _authTokenService = authTokenService;

        }

        public ActionResult Index()
        {
            _authTokenService.GetAuthTokens();
            return RedirectToAction("Inbox","Gmail");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}