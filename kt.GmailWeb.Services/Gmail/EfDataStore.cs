using Google.Apis.Util.Store;
using kt.GmailWeb.Core.Domain;
using kt.GmailWeb.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kt.GmailWeb.Services.Gmail
{
    public class EfDataStore : IDataStore
    {
        IAuthTokenService _service;
        public EfDataStore(IAuthTokenService service)
        {
            _service = service;
        }
        public async Task ClearAsync()
        {
            await _service.CleaAsync();
        }

        public async Task DeleteAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key MUST have a value");
            }

            var generatedKey = GenerateStoredKey(key, typeof(T));
            await _service.DeleteAsync(generatedKey);
        }

        public Task<T> GetAsync<T>(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key MUST have a value");
            }

            var generatedKey = GenerateStoredKey(key, typeof(T));
            var item = _service.GetAuthToken(generatedKey);
            T value = item == null ? default(T) : JsonConvert.DeserializeObject<T>(item.Value);
            return Task.FromResult<T>(value);
        }

        public async Task StoreAsync<T>(string key, T value)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key MUST have a value");
            }

            var generatedKey = GenerateStoredKey(key, typeof(T));
            string json = JsonConvert.SerializeObject(value);

            await _service.StoreAsync(generatedKey, json);
        }

        private static string GenerateStoredKey(string key, Type t)
        {
            return string.Format("{0}-{1}", t.FullName, key);
        }
    }
}
