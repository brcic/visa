using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visa.Domain;

namespace Visa.Service
{
    public class ApplyformService
    {
        public static int Add(ApplyForm model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Insertable<ApplyForm>(model).ExecuteReturnIdentity();
        }
        public static bool Edit(ApplyForm model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Updateable<ApplyForm>(model).IgnoreColumns(q => new {  q.SqId }).ExecuteCommand() > 0;
        }
        public static ApplyForm GetModel(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<ApplyForm>().Where(q => q.Id == id).First();
        }
        public static ApplyForm GetModelBy(int sqid)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<ApplyForm>().Where(q => q.SqId == sqid).OrderBy("Id Desc").Take(1).First();
        }
        public static bool Delete(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Deleteable<ApplyForm>().Where(q => q.Id == id).ExecuteCommand() > 0;
        }
    }
}
