using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visa.Domain;

namespace Visa.Service
{
    public class _AppService
    {
        public static bool Exist(string email)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Queryable<_AppTemp>().Any(q => q.Email == email);
        }
        public static int Add(_AppTemp model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Insertable<_AppTemp>(model).ExecuteReturnIdentity();
        }
        public static bool Edit(_AppTemp model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Updateable(model).ExecuteCommand() > 0;
        }
        public static bool EditOnly(_AppTemp model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Updateable<_AppTemp>(model).IgnoreColumns(q => new { q.ApplyNumber, q.ApId, q.TransactionId, q.Nationality }).ExecuteCommand() > 0;
        }
        public static _AppTemp GetModel(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Queryable<_AppTemp>().Where(q => q.Id == id).First();
        }
        public static string GetYYXH(int sqrId)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Ado.GetString("select ApplyNumber from [_AppTemp] where [ApId]=@ApId", new { ApId = sqrId });
        }
        public static bool Delete(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Deleteable<_AppTemp>().Where(q => q.Id == id).ExecuteCommand()>0;
        }
        public static List<_AppTemp> GetApplicantList(int apid)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<_AppTemp>().Where(q => q.ApId == apid).OrderBy(q => q.Id, OrderByType.Desc).ToList();
        }
    }
}
