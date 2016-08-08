using kt.GmailWeb.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kt.GmailWeb.WebApp.Models
{
    public class EmailViewModel
    {
        public string MailId { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Bcc { get; set; }
        public string Cc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string BodyHtml { get; set; }
        public string Header { get; set; }
        public string Snippet { get; set; }
        public DateTime? SendDate { get; set; }

        public static EmailViewModel FromEntity(Email entity){
            AutoMapper.Mapper.CreateMap<Email, EmailViewModel>();
            return AutoMapper.Mapper.Map<EmailViewModel>(entity);
        }
    }
}