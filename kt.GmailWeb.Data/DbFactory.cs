using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kt.GmailWeb.Data
{
    public class DbFactory : Disposable, IDbFactory
    {
        StorageContext dbContext;

        public StorageContext Init()
        {
            return dbContext ?? (dbContext = new StorageContext("DBConnection"));
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
                dbContext.Dispose();
        }
    }
}
