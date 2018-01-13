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
    public class TypeService
    {

        public static int Add(VisaType model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Insertable<VisaType>(model).ExecuteReturnIdentity();
        }
        public static bool Edit(VisaType model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Updateable<VisaType>(model).ExecuteCommand() > 0;
        }
        public static VisaType GetModel(string region,string engName)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<VisaType>().Where(q => q.RegionCode == region && q.EngName == engName).First();

        }
        public static VisaType GetModel(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<VisaType>().Where(q => q.Id == id).First();

        }
        public static bool Delete(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Deleteable<VisaType>().Where(q => q.Id == id).ExecuteCommand() > 0;
        }
        public static List<VisaType> GetTypeList(string countryName)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            string getSql = "SELECT [Id],[TypeName],[EngName] FROM VisaType WHERE RegionCode=@RegionCode";
            List<VisaType> sqlExp = db.Ado.SqlQuery<VisaType>(getSql, new { RegionCode = countryName }).ToList();
            return sqlExp;
        }
        public static List<VisaType> GetTypeList(int PageIndex, int PageSize,ref int totalRecord, List<Expression<Func<VisaType, bool>>> listexp)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            var sqlExp = db.Queryable<VisaType>();

            for (int i = 0; i < listexp.Count; i++)
            {
                sqlExp.Where(listexp[i]);
            }
            return sqlExp.OrderBy(q => q.Id, OrderByType.Desc).ToPageList(PageIndex, PageSize, ref totalRecord).ToList();
        }
        public static List<KeyValuePair<string, string>> GetVisaList(string countryName)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            string getSql = "SELECT distinct [TypeName],[EngName] FROM VisaType WHERE RegionCode=@RegionCode";
            List<KeyValuePair<string, string>> sqlExp = db.Ado.SqlQuery<KeyValuePair<string, string>>(getSql, new { RegionCode = countryName }).ToList();
            return sqlExp;
        }
       
    }
}
