using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa.Domain
{
    public class VideoMaterial
    {
        /// <summary>
        /// ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// 护照
        /// </summary>           
        public string Passport { get; set; }

        /// <summary>
        ///过期日期
        /// </summary>           
        public string Expirydate { get; set; }

        /// <summary>
        /// 国籍
        /// </summary>           
        public string Nationality { get; set; }

        /// <summary>
        /// 名
        /// </summary>           
        public string Givename { get; set; }

        /// <summary>
        /// 姓
        /// </summary>           
        public string Surname { get; set; }

        /// <summary>
        /// 性别
        /// </summary>           
        public string Gender { get; set; }

        /// <summary>
        /// 电话
        /// </summary>           
        public string Cellphone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>           
        public string Email { get; set; }

        /// <summary>
        /// 预约序号
        /// </summary>           
        public string ApplyNumber { get; set; }

        /// <summary>
        /// 采集文件
        /// </summary>           
        public string videoFile { get; set; }
        /// <summary>
        /// 申请人ID
        /// </summary>           
        public int SqrId { get; set; }
        public string Operator { get; set; }
        public DateTime AddTime { get; set; }
    }
}
