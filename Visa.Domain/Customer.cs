using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa.Domain
{
    public class Customer
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Cellphone { get; set; }
        /// <summary>
        /// 姓
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// 名
        /// </summary>
        public string Givename { get; set; }
        public string UserPwd { get; set; }
        public DateTime AddTime { get; set; }
        public byte UserStatus { get; set; }
    }
}
