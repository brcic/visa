using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa.Domain
{
    public class ApplyList
    {
        public int Id { get; set; }
        public string ApplyNumber { get; set; }
        public string VisitCountry { get; set; }
        public string QzType { get; set; }
        public string Residence { get; set; }
        public string PlaceName { get; set; }
        public DateTime AddTime { get; set; }
        public string DateName { get; set; }
        public string TimeRange { get; set; }
        public int SqrId { get; set; }
        public byte CheckStatus { get; set; }

        public string WayofTake { get; set; }
        public string PostAddress { get; set; }
        public string TakePhone { get; set; }
        public double totalVisaFee { get; set; }
        public double totalServiceFee { get; set; }
        public double totalSupportFee { get; set; }
        public double totalVIPFee { get; set; }
        public double totalOtherFee { get; set; }
        /// <summary>
        /// 收款人
        /// </summary>
        public string Payee { get; set; }
        /// <summary>
        /// 付款日期
        /// </summary>
        public DateTime? Paydate { get; set; }
        /// <summary>
        /// 付款状态
        /// </summary>
        public byte PayStatus { get; set; }
        /// <summary>
        /// 收据号
        /// </summary>
        public string ReceiptNo { get; set; }
    }
}
