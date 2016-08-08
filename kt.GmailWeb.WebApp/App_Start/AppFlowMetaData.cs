using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Gmail.v1;
using kt.GmailWeb.Core.Domain;
using kt.GmailWeb.Data;
using kt.GmailWeb.Services;
using kt.GmailWeb.Services.Gmail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kt.GmailWeb.WebApp
{
    public class AppFlowMetadata : FlowMetadata
    {
        IAuthTokenService _service;
        public AppFlowMetadata(IAuthTokenService service){
            _service = service;
        }
        public override string GetUserId(Controller controller)
        {
            return controller.User.Identity.Name;

        }

        public override IAuthorizationCodeFlow Flow
        {
            get
            {
                return new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                {
                    ClientSecrets = new ClientSecrets
                    {
                        ClientId = "144118925827-01vi6avvis0cpvi625vbh6hvq1mt8t5v.apps.googleusercontent.com",
                        ClientSecret = "QzIGbLuNKWvOHn6KfxW2Pj6N"
                    },
                    Scopes = new[] { GmailService.Scope.MailGoogleCom,GmailService.Scope.GmailReadonly, GmailService.Scope.GmailCompose, GmailService.Scope.GmailModify, GmailService.Scope.GmailLabels },
                    DataStore = new EfDataStore(_service),
                });
            }
        }
    }
}