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
    public class DistrictService
    {
        public static bool Edit(vDistrict model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Updateable<vDistrict>(model).IgnoreColumns(q => new { q.ContinentId }).ExecuteCommand() > 0;
        }
        public static vDistrict GetModel(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Queryable<vDistrict>().Where(q => q.Id == id).First();
        }
        public static string GetCountry(string region)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            string getSql = "select CountryName from [vDistrict] where CountryCode=@CountryCode";
            return db.Ado.GetString(getSql, new { CountryCode=region });
        }
        public static string GetTemplateFile(string sqrid)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            string getSql = "SELECT TemplateFile FROM [vDistrict] WHERE CountryCode in(SELECT [VisitCountry] FROM [ApplicantView] where Id=@Id)";
            return db.Ado.GetString(getSql, new { Id = sqrid });
        }
        public static string GetFill(string region)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            string getSql = "select [FillMode] from [vDistrict] where CountryCode=@CountryCode";
            return db.Ado.GetString(getSql, new { CountryCode = region });
        }
        public static List<vDistrict> GetCountryList()
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<vDistrict>().Where(q=>q.Id>0).ToList();
        }
        public static List<vDistrict> GetCountryList(List<Expression<Func<vDistrict, bool>>> listexp)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            var sqlExp = db.Queryable<vDistrict>();
            for (int i = 0; i < listexp.Count; i++)
            {
                sqlExp.Where(listexp[i]);
            }
            return sqlExp.OrderBy(it => it.Id).ToList();
        }
        public static List<vDistrict> GetCountryItem()
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<vDistrict>().Where(q => q.IsShow==1).ToList();
        }
        public static List<vDistrict> GetDistrictData()
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<vDistrict>().Where(q => q.Id > 0).OrderBy(q=>q.CountryName).ToList();
        }
        public static List<KeyValuePair<string, string>> GetDistrictList()
        {
            SqlSugarClient db = SugarDao.GetInstance();
            string getSql = "select CountryCode,ChineseName+'('+CountryName+')' as CountryName from [vDistrict]";
            List<KeyValuePair<string, string>> listitem = db.Ado.SqlQuery<KeyValuePair<string, string>>(getSql).ToList();

            return listitem;
        }
    }
}
