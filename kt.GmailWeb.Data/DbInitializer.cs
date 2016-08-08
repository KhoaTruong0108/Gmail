using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kt.GmailWeb.Data
{
    class DbInitializer<TContext> : DropCreateDatabaseIfModelChanges<TContext> where TContext : DbContext
    {

    }

}
