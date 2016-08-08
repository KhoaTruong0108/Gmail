using kt.GmailWeb.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kt.GmailWeb.Data.Map
{
    public class AuthTokenMapper : EntityTypeConfiguration<AuthToken>
    {
        public AuthTokenMapper()
        {
            this.ToTable("AuthToken");
            this.HasKey(e => e.Id);
           
        }
    }
}
