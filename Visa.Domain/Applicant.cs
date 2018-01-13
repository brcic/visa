using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa.Domain
{
    public class Applicant
    {
        public int Id { get; set; }
        public string Passport { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime Expirydate { get; set; }
        public string Nationality { get; set; }
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
        public int ApId { get; set; }
        public byte ApStatus { get; set; }
        public byte fingerStatus { get; set; }
        public byte videoStatus { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        public string Approver { get; set; }
        public DateTime ApprovalTime { get; set; }
        public double VisaFee { get; set; }
        public double ServiceFee { get; set; }
        public double SupportFee { get; set; }
        public double VIPFee { get; set; }
    }
}
