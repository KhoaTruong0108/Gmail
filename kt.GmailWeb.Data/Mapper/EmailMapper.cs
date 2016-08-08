using kt.GmailWeb.Core.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kt.GmailWeb.Data.Map
{
    public class EmailMapper : EntityTypeConfiguration<Email>
    {
        public EmailMapper()
        {
            this.ToTable("Email");
            this.HasKey(e => e.Id);
           
        }
    }
}
