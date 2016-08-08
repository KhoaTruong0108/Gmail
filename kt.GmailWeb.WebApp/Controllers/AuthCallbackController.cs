using Google.Apis.Auth.OAuth2.Mvc;
using kt.GmailWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kt.GmailWeb.WebApp.Controllers
{
    public class AuthCallbackController : Google.Apis.Auth.OAuth2.Mvc.Controllers.AuthCallbackController
    {
         private readonly IAuthTokenService _service;

         public AuthCallbackController(IAuthTokenService service)
        {
            _service = service;
        }
        protected override FlowMetadata FlowData
        {
            get { return new AppFlowMetadata(_service); }
        }
    }
}