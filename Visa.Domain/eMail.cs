using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa.Domain
{
   public class eMail
    {
        public string fromMail { get; set; }
        public string toMail { get; set; }
        public string mTitle { get; set; }
        public string mBody { get; set; }
        public string userName { get; set; }
        public string uPassword { get; set; }
        public string uHost { get; set; }
    }
}
