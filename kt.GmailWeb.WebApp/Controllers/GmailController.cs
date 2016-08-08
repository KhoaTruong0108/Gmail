using Google.Apis.Auth.OAuth2.Mvc;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Google.Apis.Services;
using kt.GmailWeb.Core.Util;
using kt.GmailWeb.Services;
using kt.GmailWeb.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.IO;

namespace kt.GmailWeb.WebApp.Controllers
{
    [Authorize]
    public class GmailController : Controller
    {
        private readonly IEmailService _emailService;
        private readonly IAuthTokenService _authTokenService;

        public GmailController(IEmailService emailService, IAuthTokenService authTokenService)
        {
            _emailService = emailService;
            _authTokenService = authTokenService;
        }
        // GET: Gmail
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Inbox(int page = 1)
        {
            var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetadata(_authTokenService)).
                AuthorizeAsync(CancellationToken.None);

            if (result.Credential == null)
            {
                return new RedirectResult(result.RedirectUri);
            }
            else
            {
                var service = new GmailService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = result.Credential,
                    ApplicationName = "Gmail webapp"
                });

                List<string> labels = new List<string>() { "INBOX" };

                var listReq = service.Users.Messages.List("me");
                listReq.LabelIds = new Google.Apis.Util.Repeatable<string>(labels);
                var response = await listReq.ExecuteAsync();
                int pageSize = 10;

                List<string> metadata = new List<string>() { "Subject", "To", "From" };
                List<EmailViewModel> emailModels = new List<EmailViewModel>();
                for (int i = (page - 1) * 10; i < pageSize + page * 10 && i < response.Messages.Count(); i++)
                {
                    var msg = response.Messages[i];
                    var getReq = service.Users.Messages.Get("me", msg.Id);
                    getReq.Format = UsersResource.MessagesResource.GetRequest.FormatEnum.Metadata;
                    getReq.MetadataHeaders = new Google.Apis.Util.Repeatable<string>(metadata);
                    Message message = getReq.ExecuteAsync().Result;

                    emailModels.Add(EmailViewModel.FromEntity(GmailParser.FromGmailApi(message)));
                }

                return View("Inbox",new InboxViewModel
                {
                    Emails = emailModels,
                    PagerList = new PagedList.PagedList<Message>(response.Messages.ToList(), page, pageSize)
                });
            }
        }

        public async Task<ActionResult> Detail(string mailId)
        {
            var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetadata(_authTokenService)).
                AuthorizeAsync(CancellationToken.None);

            if (result.Credential == null)
            {
                return new RedirectResult(result.RedirectUri);
            }
            else
            {
                var service = new GmailService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = result.Credential,
                    ApplicationName = "Gmail webapp"
                });

                var getReq = service.Users.Messages.Get("me", mailId);
                getReq.Format = UsersResource.MessagesResource.GetRequest.FormatEnum.Full;
                Message message = getReq.ExecuteAsync().Result;

                return View(EmailViewModel.FromEntity(GmailParser.FromGmailApi(message)));
            }
        }

        public async Task<ActionResult> Delete(string mailId)
        {
            var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetadata(_authTokenService)).
                AuthorizeAsync(CancellationToken.None);

            if (result.Credential == null)
            {
                return new RedirectResult(result.RedirectUri);
            }
            else
            {
                var service = new GmailService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = result.Credential,
                    ApplicationName = "Gmail webapp"
                });

                await service.Users.Messages.Delete("me", mailId).ExecuteAsync();
                return RedirectToAction("Inbox");
            }
        }

        public ActionResult Compose()
        {
            return View(new EmailViewModel { From = User.Identity.Name });
        }

        [HttpPost]
        public async Task<ActionResult> Compose(EmailViewModel model)
        {
            var result = await new AuthorizationCodeMvcApp(this, new AppFlowMetadata(_authTokenService)).
                AuthorizeAsync(CancellationToken.None);

            if (result.Credential == null)
            {
                return new RedirectResult(result.RedirectUri);
            }
            else
            {
                var service = new GmailService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = result.Credential,
                    ApplicationName = "Gmail webapp"
                });

                var msg = new AE.Net.Mail.MailMessage
                {
                    ContentType = "text/plain",
                    Subject = model.Subject,
                    Body = model.Body,
                    From = new MailAddress(model.From)
                };

                var toAddresses = model.To.Split(',');
                foreach (var to in toAddresses)
                {
                    msg.To.Add(new MailAddress(to));
                }

                var ccAddresses = model.Cc.Split(',');
                foreach (var cc in ccAddresses)
                {
                    msg.Cc.Add(new MailAddress(cc));
                }

                var BccAddresses = model.Bcc.Split(',');
                foreach (var bcc in BccAddresses)
                {
                    msg.Bcc.Add(new MailAddress(bcc));
                }
                msg.ReplyTo.Add(msg.From);

                var msgStr = new StringWriter();
                msg.Save(msgStr);

                service.Users.Messages.Send(new Message
                {
                    Raw = Base64UrlEncode(msgStr.ToString())
                }, "me").Execute();

                return RedirectToAction("Inbox");
            }
        }
        private string Base64UrlEncode(string input)
        {
            var inputBytes = System.Text.Encoding.UTF8.GetBytes(input);
            return Convert.ToBase64String(inputBytes)
              .Replace('+', '-')
              .Replace('/', '_')
              .Replace("=", "");
        }
    }
}