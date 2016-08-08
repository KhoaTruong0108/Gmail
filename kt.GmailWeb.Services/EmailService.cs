using kt.GmailWeb.Core.Domain;
using kt.GmailWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kt.GmailWeb.Services
{
    public interface IEmailService
    {
        IEnumerable<Email> GetEmails();
        Email GetEmail(string mailId);
        void SaveEmail(Email mail);
        void DeleteEmail(int id);
        void Save();
    }
    public class EmailService : IEmailService
    {
        private readonly IRepository<Email> _emailRepsotiry;
        private readonly IUnitOfWork _uow;
        public EmailService(IRepository<Email> emailRepsotiry, IUnitOfWork uow)
        {
            _emailRepsotiry = emailRepsotiry;
            _uow = uow;
        }
        public IEnumerable<Email> GetEmails()
        {
            return _emailRepsotiry.GetAll();
        }

        public Email GetEmail(string mailId)
        {
            return _emailRepsotiry.Get(e => e.MailId == mailId);
        }

        public void SaveEmail(Email mail)
        {
            _emailRepsotiry.Add(mail);
        }

        public void DeleteEmail(int id)
        {
            _emailRepsotiry.Delete(e => e.Id == id);
        }

        public void Save()
        {
            _uow.Commit();
        }
    }
}
