using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa.Domain
{
   public  class HowToApply
    {
        public int Id { get; set; }
        public string SubmitInPerson { get; set; }
        public string MailYourApplication { get; set; }
        public string SubmitYourPassport { get; set; }
        public string RegionCode { get; set; }
        public byte TypeId { get; set; }
    }
}
