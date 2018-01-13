using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa.Domain
{
    public class ApplicantDto
    {
        public int Id { get; set; }
        public string Passport { get; set; }
        public string Givename { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; set; }
        /// <summary>
        /// 预约序号
        /// </summary>
        public string ApplyNumber { get; set; }
        /// <summary>
        /// 跟踪ID
        /// </summary>
        public string TransactionId { get; set; }
        public byte ApStatus { get; set; }
        public string VisitCountry { get; set; }
        public string DateName { get; set; }
        public string TimeRange { get; set; }
        public string Nationality { get; set; }
        public DateTime Expirydate { get; set; }
        public byte fingerStatus { get; set; }
        public byte videoStatus { get; set; }
    }
}
