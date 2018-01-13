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
    public class EntryService
    {
       
        public static int Add(vEntry model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Insertable<vEntry>(model).ExecuteReturnIdentity();
        }

        public static bool Edit(vEntry model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Updateable(model).IgnoreColumns(q => new { q.Vid }).ExecuteCommand() > 0;
        }

        public static vEntry GetModel(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Queryable<vEntry>().Where(q => q.Id == id).First();
        }

        public static bool Delete(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Deleteable<vEntry>().Where(q => q.Id == id).ExecuteCommand() > 0;
        }


        public static List<vEntry> GetEntryList(List<Expression<Func<vEntry, bool>>> listexp)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            var sqlExp = db.Queryable<vEntry>();
            for (int i = 0; i < listexp.Count; i++)
            {
                sqlExp.Where(listexp[i]);
            }
            return sqlExp.OrderBy(it => it.Id).ToList();
        }
        public static List<vEntry> GetEntryList(string vid)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Queryable<vEntry>().Where(q=>q.Vid==Convert.ToInt32(vid)).ToList();
        }
        public static List<KeyValuePair<string, string>> GetEntryData(string eng,string code)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            string getSql = "SELECT [NumberEntry],[EntryFee] FROM vEntry WHERE Vid IN(SELECT Id FROM VisaType where [EngName]=@EngName and [RegionCode]=@RegionCode)";
            List<KeyValuePair<string, string>> sqlExp = db.Ado.SqlQuery<KeyValuePair<string, string>>(getSql, new { EngName = eng, RegionCode=code }).ToList();
            return sqlExp;
        }
    }
}
