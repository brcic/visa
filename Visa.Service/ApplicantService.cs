using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Visa.Domain;

namespace Visa.Service
{
    public class ApplicantService
    {
        public static bool Exist(string email)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Queryable<Customer>().Any(q => q.Email == email);
        }
        public static int Add(Applicant model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Insertable<Applicant>(model).ExecuteReturnIdentity();
        }
        public static bool Edit(Applicant model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Updateable(model).ExecuteCommand() > 0;
        }
        public static Applicant GetModel(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Queryable<Applicant>().Where(q => q.Id == id).First();
        }

        public static bool UpdateStstus(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            string upSql = "UPDATE [Applicant] SET ApStatus=1 WHERE Id=@Id";
            return db.Ado.ExecuteCommand(upSql, new { Id = id }) > 0;
        }
        public static bool UpdateFingerStstus(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            string upSql = "UPDATE [Applicant] SET fingerStatus=1 WHERE Id=@Id";
            return db.Ado.ExecuteCommand(upSql, new { Id = id }) > 0;
        }
        public static bool UpdateVideoStstus(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            string upSql = "UPDATE [Applicant] SET videoStatus=1 WHERE Id=@Id";
            return db.Ado.ExecuteCommand(upSql, new { Id = id }) > 0;
        }
        public static bool UpdateVisaFee(string fee, int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            string upSql = "UPDATE [Applicant] SET VisaFee=@VisaFee WHERE Id=@Id";
            int js1 = db.Ado.ExecuteCommand(upSql, new { VisaFee = fee, Id = id });
            string getSql = "SELECT SUM([VisaFee]) [VisaFee],SUM([ServiceFee]) [ServiceFee],SUM([SupportFee]) [SupportFee] ,SUM([VIPFee]) [VIPFee] FROM [Applicant] ";
            getSql += " WHERE ApplyNumber in(SELECT ApplyNumber FROM Applicant WHERE Id=@Id)";
            FeeDto dto = db.Ado.SqlQuerySingle<FeeDto>(getSql, new { Id = id });
            string getSql2 = "SELECT ApplyNumber FROM Applicant WHERE Id=@Id";
            string applyNumber = db.Ado.GetString(getSql2, new { Id = id });
            if (dto != null)
            {
                string upSql2 = "UPDATE [ApplyList] SET totalVisaFee=@totalVisaFee,totalServiceFee=@totalServiceFee,totalSupportFee=@totalSupportFee,totalVIPFee=@totalVIPFee";
                upSql2 += ",totalOtherFee=0 WHERE ApplyNumber=@ApplyNumber";
                return db.Ado.ExecuteCommand(upSql2, new { totalVisaFee = dto.VisaFee, totalServiceFee = dto.ServiceFee, totalSupportFee = dto.SupportFee, totalVIPFee=dto.VIPFee, ApplyNumber = applyNumber }) > 0;
            }
            return false;
        }
        public static bool Approval(string approver, string transactionId, string id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            string upSql = "UPDATE [Applicant] SET ApStatus=1,Approver=@Approver,TransactionId=@TransactionId,ApprovalTime=GETDATE() WHERE Id=@Id";
            return db.Ado.ExecuteCommand(upSql, new { Approver = approver, TransactionId = transactionId, Id = id }) > 0;
        }
        public static bool Delete(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Deleteable<Applicant>().Where(q => q.Id == id).ExecuteCommand() > 0;
        }
        public static bool Delete(string[] ids)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Deleteable<Applicant>().In(ids).ExecuteCommand() > 0;
        }
        public static List<Applicant> GetApplicantList(string[] ids)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<Applicant>().In(q => q.Id, ids).ToList();
        }
        public static List<Applicant> GetApplicantList(int apid)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<Applicant>().Where(q => q.ApId == apid).OrderBy(q => q.Id, OrderByType.Desc).ToList();
        }
        public static List<FeeDto> GetApplicantFee(string number)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            string getSql = "SELECT  SUM([VisaFee]) [VisaFee],SUM([ServiceFee]) [ServiceFee],SUM([SupportFee]) [SupportFee] ,SUM([VIPFee]) [VIPFee] FROM [Applicant] ";
            getSql += " WHERE ApplyNumber=@ApplyNumber";
            return db.Ado.SqlQuery<FeeDto>(getSql, new { ApplyNumber = number }).ToList();
        }
        public static List<ApplicantView> GetApplicantView(List<Expression<Func<ApplicantView, bool>>> listexp)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            var sqlExp = db.Queryable<ApplicantView>();
            for (int i = 0; i < listexp.Count; i++)
            {
                sqlExp.Where(listexp[i]);
            }
            return sqlExp.OrderBy(it => it.Id, OrderByType.Desc).ToList();
        }
    }
}
