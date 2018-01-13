using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visa.Domain;

namespace Visa.Service
{
    public class DownloadServce
    {
        public static int Add(DownloadForm model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Insertable<DownloadForm>(model).ExecuteReturnIdentity();
        }
        public static bool Edit(DownloadForm model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Updateable<DownloadForm>(model).IgnoreColumns(q => new { q.AddTime, q.QzId }).ExecuteCommand()>0;
        }
        public static DownloadForm GetModel(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<DownloadForm>().Where(q => q.Id==id).First();

        }
        public static bool Delete(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Deleteable<DownloadForm>().Where(q => q.Id == id).ExecuteCommand() > 0;
        }
        public static List<DownloadForm> GetFormList(string qzId)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            string getSql = "SELECT * FROM [DownloadForm] WHERE QzId=@QzId order by Id desc";
            List<DownloadForm> sqlExp = db.Ado.SqlQuery<DownloadForm>(getSql, new { QzId = qzId }).ToList();
            return sqlExp;
        }
        public static List<DownloadForm> GetFormBy(string region)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            string getSql = "Select * from [DownloadForm] where QzId in(SELECT [Id] FROM [VisaType] where [RegionCode]=@RegionCode)";
            List<DownloadForm> sqlExp = db.Ado.SqlQuery<DownloadForm>(getSql, new { RegionCode = region }).ToList();
            return sqlExp;
        }
    }
}
