using kt.GmailWeb.Core.Domain;
using kt.GmailWeb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kt.GmailWeb.Services
{
    public interface IAuthTokenService
    {
        IEnumerable<AuthToken> GetAuthTokens();
        AuthToken GetAuthToken(string key);
        void SaveAuthToken(AuthToken mail);
        void DeleteAuthToken(int id);
        void Save();

        Task CleaAsync();
        Task DeleteAsync(string key);
        Task<AuthToken> GetAsync(string key);
        Task StoreAsync(string key, string value);
    }
    public class AuthTokenService : IAuthTokenService
    {
        private readonly IRepository<AuthToken> _authTokenRepsotiry;
        private readonly IUnitOfWork _uow;
        public AuthTokenService(IRepository<AuthToken> AuthTokenRepsotiry, IUnitOfWork uow)
        {
            _authTokenRepsotiry = AuthTokenRepsotiry;
            _uow = uow;
        }
        public IEnumerable<AuthToken> GetAuthTokens()
        {
            return _authTokenRepsotiry.GetAll();
        }

        public AuthToken GetAuthToken(string key)
        {
            return _authTokenRepsotiry.Get(e => e.Key == key);
        }

        public void SaveAuthToken(AuthToken mail)
        {
            _authTokenRepsotiry.Add(mail);
        }

        public void DeleteAuthToken(int id)
        {
            _authTokenRepsotiry.Delete(e => e.Id == id);
        }

        public void Save()
        {
            _uow.Commit();
        }


        public async Task CleaAsync()
        {
            await _authTokenRepsotiry.ClearAsync();
        }

        public async Task DeleteAsync(string key)
        {
            await _authTokenRepsotiry.DeleteAsync(e => e.Key == key);
        }

        public async Task<AuthToken> GetAsync(string key)
        {
            return await _authTokenRepsotiry.GetAsync(i => i.Key == key);
        }

        public async Task StoreAsync(string key, string value)
        {
            var item = await _authTokenRepsotiry.GetAsync(i => i.Key == key);

            if (item == null)
            {
                _authTokenRepsotiry.Add(new AuthToken { Key = key, Value = value });
            }
            else
            {
                item.Value = value;
            }

            await _authTokenRepsotiry.SaveAsync();
        }
    }
}
