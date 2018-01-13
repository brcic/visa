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
    public class ApplyService
    {

        public static int Add(ApplyList model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Insertable<ApplyList>(model).ExecuteReturnIdentity();
        }
        public static void AddTran(ApplyList model,string vfee, out string _number)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            try
            {
                db.Ado.BeginTran();
                string getSql = "select [ApplyNumber] from [_AppTemp] where [ApId]=@ApId";
                string appplyNumber = db.Ado.GetString(getSql, new { ApId =model.SqrId});
                model.ApplyNumber = appplyNumber;
                _number = appplyNumber;

                db.Insertable<ApplyList>(model).ExecuteCommand();
                //签证服务费=填表助理+确认基本文件服务
                //其他服务支持费=快递费+短信跟踪费
                string inSql = "INSERT INTO [Applicant] ([Passport],[Birthdate],[Expirydate],[Nationality],[Givename],[Surname],[Gender],[Cellphone],[Email],[ApplyNumber],[TransactionId],[ApId],VisaFee,ServiceFee,SupportFee,VIPFee)";
                inSql += " select [Passport],[Birthdate],[Expirydate],[Nationality],[Givename],[Surname],[Gender],[Cellphone],[Email],[ApplyNumber],[TransactionId],[ApId],"+vfee+",350,80,0";
                inSql += " from [_AppTemp] where ApId=@ApId";
                db.Ado.ExecuteCommand(inSql, new { ApId = model.SqrId });
                string delSql = "delete from [_AppTemp] where ApId=@ApId";
                db.Ado.ExecuteCommand(delSql, new { ApId = model.SqrId });
                db.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                db.Ado.RollbackTran();
                throw ex;
            }
        }
        public static bool Edit(ApplyList model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Updateable(model).ExecuteCommand() > 0;
        }
        public static bool EditOnly(Dictionary<string,object> dic)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Updateable<ApplyList>(dic).ExecuteCommand() > 0;
        }
        public static ApplyList GetModel(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Queryable<ApplyList>().Where(q => q.Id == id).First();
        }
       
        public static bool Delete(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Deleteable<ApplyList>().Where(q => q.Id == id).ExecuteCommand() > 0;
        }
        public static bool Clear()
        {
            SqlSugarClient db = SugarDao.GetInstance();
            string delSql = "  delete from [ApplyList]  where  ApplyNumber in(SELECT ApplyNumber FROM [ApplyList] c WHERE  ";
            delSql += "  NOT EXISTS(SELECT ApplyNumber FROM [Applicant] o WHERE o.ApplyNumber=c.ApplyNumber))";
            return db.Ado.ExecuteCommand(delSql)>0;
        }
        public static bool Payconfirm(string jsr,string sjh,string id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            string upSql = " UPDATE ApplyList SET [Payee]=@Payee,[Paydate]=getdate(),[PayStatus]=1,[ReceiptNo]=@ReceiptNo ";
            upSql += " WHERE Id=@Id";
            return db.Ado.ExecuteCommand(upSql, new { Payee = jsr, ReceiptNo = sjh, Id =id}) > 0;
        }
        public static List<ApplyList> GetApplyList(string date)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<ApplyList>().Where(q => q.DateName == date).OrderBy(q => q.Id, OrderByType.Desc).ToList();
        }
        public static List<ApplyList> GetApplyList(int apid)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<ApplyList>().Where(q => q.SqrId == apid&&q.CheckStatus==0).OrderBy(q => q.Id, OrderByType.Desc).ToList();
        }
        public static ApplyList GetModelBy(string applyNumber)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<ApplyList>().Where(q => q.ApplyNumber == applyNumber).First();
        }
        public static List<ApplyView> GetApplyViewList(List<Expression<Func<ApplyView, bool>>> listexp)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            var sqlExp = db.Queryable<ApplyView>();
            for (int i = 0; i < listexp.Count; i++)
            {
                sqlExp.Where(listexp[i]);
            }
            return sqlExp.OrderBy(it => it.Id).ToList() ;
        }
        public static List<AppointView> GetAppointList(int PageIndex, int PageSize, ref int totalRecord, List<Expression<Func<AppointView, bool>>> listexp)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            var sqlExp = db.Queryable<AppointView>();

            for (int i = 0; i < listexp.Count; i++)
            {
                sqlExp.Where(listexp[i]);
            }
            return sqlExp.OrderBy(q => q.Id, OrderByType.Desc).ToPageList(PageIndex, PageSize, ref totalRecord).ToList();
        }
    }
}
