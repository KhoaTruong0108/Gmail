using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kt.GmailWeb.Core.Domain
{
    public class AuthToken : BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
