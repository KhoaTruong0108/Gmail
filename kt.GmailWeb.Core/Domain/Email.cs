using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kt.GmailWeb.Core.Domain
{
    public class Email : BaseEntity
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
    }
}
