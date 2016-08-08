using Google.Apis.Gmail.v1.Data;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace kt.GmailWeb.WebApp.Models
{
    public class InboxViewModel
    {
        public InboxViewModel()
        {
            Emails = new List<EmailViewModel>();
        }
        public List<EmailViewModel> Emails { get; set; }
        public PagedList<Message> PagerList { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}