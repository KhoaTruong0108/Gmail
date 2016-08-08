using kt.GmailWeb.Data.Map;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kt.GmailWeb.Data
{
    public class StorageContext : DbContext
    {
        public StorageContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<StorageContext>(new DbInitializer<StorageContext>());
        }
 
        public virtual void Commit()
        {
            base.SaveChanges();
        }
 
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmailMapper());
            modelBuilder.Configurations.Add(new UserMapper());
            modelBuilder.Configurations.Add(new AuthTokenMapper());
            base.OnModelCreating(modelBuilder);
        }
    }
}
