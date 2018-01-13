using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa.Domain
{
    public class SysAdmin
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string UserPhone { get; set; }
        public string Permission { get; set; }
        public string UserXm { get; set; }
        public string Department { get; set; }
        public string UserMark { get; set; }
        public string UserCountry { get; set; }
    }
}
