using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa.Domain
{
    public class ApplyForm
    {
        public ApplyForm()
        {


        }
        /// <summary>
        /// ID
        /// </summary>           
        public int Id { get; set; }

        /// <summary>
        /// Desc:姓
        /// </summary>           
        public string Surname { get; set; }

        /// <summary>
        /// Desc:名字
        /// </summary>           
        public string Givname { get; set; }

        /// <summary>
        /// Desc:性别
        /// </summary>           
        public string Gender { get; set; }

        /// <summary>
        /// Desc:曾用名
        /// </summary>           
        public string Prename { get; set; }

        /// <summary>
        /// Desc:中文名
        /// </summary>           
        public string Chiname { get; set; }

        /// <summary>
        /// Desc:当前国籍
        /// </summary>           
        public string CNationality { get; set; }

        /// <summary>
        /// Desc:之前国籍
        /// </summary>           
        public string FNationality { get; set; }

        /// <summary>
        /// Desc:出生日期
        /// </summary>           
        public string Dateofbirth { get; set; }

        /// <summary>
        /// Desc:出生地点
        /// </summary>           
        public string Placeofbirth { get; set; }

        /// <summary>
        /// Desc:身份证号
        /// </summary>           
        public string IDNumber { get; set; }

        /// <summary>
        /// Desc:婚姻状况
        /// </summary>           
        public string MaritalStatus { get; set; }

        /// <summary>
        /// Desc:联系地址
        /// </summary>           
        public string ContactAddress { get; set; }

        /// <summary>
        /// Desc:联系电话
        /// </summary>           
        public string Telephone { get; set; }

        /// <summary>
        /// Desc:职业
        /// </summary>           
        public string Occupation { get; set; }

        /// <summary>
        /// Desc:护照类型
        /// </summary>           
        public string PassportType { get; set; }

        /// <summary>
        /// Desc:护照号
        /// </summary>           
        public string PassportNumber { get; set; }

        /// <summary>
        /// Desc:签发日期
        /// </summary>           
        public string DateIssue { get; set; }

        /// <summary>
        /// Desc:签发地点
        /// </summary>           
        public string PlaceIssue { get; set; }

        /// <summary>
        /// Desc:到期日期
        /// </summary>           
        public string ExpiryDate { get; set; }

        /// <summary>
        /// Desc:是否加急1-24；2-72
        /// </summary>           
        public string RushService { get; set; }

        /// <summary>
        /// Desc:主要事由
        /// </summary>           
        public string VisitPurpose { get; set; }

        /// <summary>
        /// Desc:计划入境次数
        /// </summary>           
        public string EntryNumber { get; set; }

        /// <summary>
        /// Desc:预计入境加纳的日期
        /// </summary>           
        public string ProposedDateEntry { get; set; }
        /// <summary>
        ///邀请人或邀请单位的名字
        /// </summary>   
        public string InviterName { get; set; }
        /// <summary>
        ///邀请人或邀请单位的地址
        /// </summary>   
        public string InviterAddress { get; set; }
        /// <summary>
        ///邀请人或邀请单位的电话和邮箱
        /// </summary>  
        public string InviterPhone { get; set; }
        /// <summary>
        ///逗留期间的地点酒店名称
        /// </summary>  
        public string ResidenceName { get; set; }
        /// <summary>
        ///逗留期间的地点的详细地址
        /// </summary>  
        public string ResidenceAddress { get; set; }
        /// <summary>
        ///逗留期间的地点的电话
        /// </summary>  
        public string ResidencePhone { get; set; }
        /// <summary>
        ///谁将承担往返加纳及在加纳的费用
        /// </summary>           
        public string WhoCostYourExpense { get; set; }

        /// <summary>
        /// Desc:保险公司名称及账号
        /// </summary>           
        public string InsuranceCompanyAccount { get; set; }

        /// <summary>
        /// Desc:详细家庭邮政地址
        /// </summary>           
        public string HomeAddress { get; set; }

        /// <summary>
        /// Desc:家庭电话
        /// </summary>           
        public string HomeTelephone { get; set; }

        /// <summary>
        /// Desc:手机
        /// </summary>           
        public string MobilePhone { get; set; }

        /// <summary>
        /// Desc:电子邮箱
        /// </summary>           
        public string EmailAddress { get; set; }

        /// <summary>
        /// Desc:工作单位或学校名称
        /// </summary>           
        public string EmployerName { get; set; }

        /// <summary>
        /// Desc:工作单位或学校邮箱
        /// </summary>           
        public string EmployerMail { get; set; }

        /// <summary>
        ///工作单位或学校电话
        /// </summary>           
        public string EmployerPhone { get; set; }

        /// <summary>
        /// 紧急情况下的联系人
        /// </summary>           
        public string EmergencyContact { get; set; }

        /// <summary>
        /// 紧急情况下的联系人电话号码
        /// </summary>           
        public string EmergencyPhone { get; set; }

        public string VisitedPlace { get; set; }
        public string VisitedPurpose { get; set; }
        public string VisitedDate { get; set; }
        /// <summary>
        /// 是否曾在目的地国超过签证或居留许可允许的期限停留
        /// </summary>           
        public string HaveOverstayed { get; set; }

        /// <summary>
        /// 是否曾经被拒绝颁发目的地国签证,或被拒绝进入目的地国
        /// </summary>           
        public string HaveRefused { get; set; }

        /// <summary>
        /// 是否在目的地或其他国家有违法记录
        /// </summary>           
        public string HaveCriminal { get; set; }

        /// <summary>
        ///是否患有以下任一种疾病
        /// </summary>           
        public string HaveDisease { get; set; }

        /// <summary>
        /// 近30日内是否前往过流行性疾病传染的国家或地区
        /// </summary>           
        public string VisitInfected { get; set; }

        /// <summary>
        /// 如果对3.i到3.m的任何一个问题选择“是”，请在下面详细说明。
        /// </summary>           
        public string Givedetail { get; set; }

        /// <summary>
        /// 如果有本表未涉及而需专门陈述的其他与签证申请相关的事项，请在此说明。
        /// </summary>           
        public string DeclaredAbove { get; set; }

      

        /// <summary>
        /// 申请人的ID不是登录系统的人的ID
        /// </summary>           
        public int SqId { get; set; }
    }
}
