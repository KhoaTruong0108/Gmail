using kt.GmailWeb.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kt.GmailWeb.Data.Map
{
    public class UserMapper : EntityTypeConfiguration<User>
    {
        public UserMapper()
        {
            this.ToTable("User");
            this.HasKey(e => e.Id);
            this.Property(e => e.Email).HasMaxLength(100);
        }
    }
}
