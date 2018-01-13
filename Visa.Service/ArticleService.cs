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
    public class ArticleService
    {
        public static bool Edit(vArticle model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Updateable(model).IgnoreColumns(q => new { q.RegionCode, q.TypeId }).ExecuteCommand() > 0;
        }
        public static vArticle GetModel(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<vArticle>().Where(q => q.Id == id).First();

        }
        public static vArticle GetModel(string region,int typeId)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<vArticle>().Where(q => q.TypeId == typeId && q.RegionCode == region).First();

        }
        public static List<vArticle> GetArticleList(string countryName)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            string getSql = "SELECT [Id],[ArticleTitle],[PageTitle] FROM vArticle WHERE RegionCode=@RegionCode";
            List<vArticle> sqlExp = db.Ado.SqlQuery<vArticle>(getSql, new { RegionCode = countryName }).ToList();
            return sqlExp;
        }
        public static List<vArticle> GetArticleList(int PageIndex, int PageSize, ref int totalRecord, List<Expression<Func<vArticle, bool>>> listexp)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            var sqlExp = db.Queryable<vArticle>();

            for (int i = 0; i < listexp.Count; i++)
            {
                sqlExp.Where(listexp[i]);
            }
            string selectStr = "[Id],[ArticleTitle],[PageTitle]";
            return sqlExp.OrderBy(q => q.Id, OrderByType.Desc).Select(selectStr).ToPageList(PageIndex, PageSize, ref totalRecord).ToList();
        }
    }
}
