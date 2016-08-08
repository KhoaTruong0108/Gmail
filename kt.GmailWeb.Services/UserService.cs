using kt.GmailWeb.Core.Domain;
using kt.GmailWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kt.GmailWeb.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User GetUser(int id);
        void SaveUser(User user);
        void DeleteUser(int id);
        void Save();
    }
    public class UserService : IUserService
    {
        private readonly IRepository<User> _UserRepsotiry;
        private readonly IUnitOfWork _uow;
        public UserService(IRepository<User> UserRepsotiry, IUnitOfWork uow)
        {
            _UserRepsotiry = UserRepsotiry;
            _uow = uow;
        }
        public IEnumerable<User> GetUsers()
        {
            return _UserRepsotiry.GetAll();
        }

        public User GetUser(int id)
        {
            return _UserRepsotiry.Get(e => e.Id == id);
        }

        public void SaveUser(User user)
        {
            _UserRepsotiry.Add(user);
        }

        public void DeleteUser(int id)
        {
            _UserRepsotiry.Delete(e => e.Id == id);
        }

        public void Save()
        {
            _uow.Commit();
        }
    }
}
