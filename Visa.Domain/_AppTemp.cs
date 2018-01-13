using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa.Domain
{
    public class _AppTemp
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
        /// <summary>
        /// 注册人ID
        /// </summary>
        public int ApId { get; set; }
    }
}
