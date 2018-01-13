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
    public class AdminService
    {
        public static bool Exist(string userName)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Queryable<SysAdmin>().Any(q => q.UserName == userName);
        }
        public static int Add(SysAdmin model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Insertable<SysAdmin>(model).ExecuteReturnIdentity();
        }

        public static bool Edit(SysAdmin model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Updateable(model).ExecuteCommand() > 0;
        }

        public static SysAdmin GetModel(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Queryable<SysAdmin>().Where(q => q.Id == id).First();
        }

        public static bool Delete(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Deleteable<SysAdmin>().Where(q => q.Id == id).ExecuteCommand() > 0;
        }

        public static List<SysAdmin> CheckLogin(string userName, string userPwd)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Queryable<SysAdmin>().Where(q => q.UserName==userName&&q.UserPwd==userPwd).ToList();
        }
        public static bool ChangePwd(string userName, string userPwd)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Updateable<SysAdmin>().UpdateColumns(it => new SysAdmin() { UserPwd = userPwd }).Where(q => q.UserName == userName).ExecuteCommand() > 0;
        }
        public static List<SysAdmin> GetAdminList(List<Expression<Func<SysAdmin, bool>>> listexp)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            var sqlExp = db.Queryable<SysAdmin>();
            for (int i = 0; i < listexp.Count; i++)
            {
                sqlExp.Where(listexp[i]);
            }
            return sqlExp.OrderBy(it => it.Id).ToList();
        }
        public static List<SysAdmin> GetAdminList(int PageIndex, ref int totalRecord, int PageSize, List<Expression<Func<SysAdmin, bool>>> listexp)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            var sqlExp = db.Queryable<SysAdmin>();
            for (int i = 0; i < listexp.Count; i++)
            {
                sqlExp.Where(listexp[i]);
            }
            return sqlExp.OrderBy(q => q.Id, OrderByType.Desc).ToPageList(PageIndex, PageSize, ref totalRecord);
        }
    }
}
