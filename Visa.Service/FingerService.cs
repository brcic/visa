using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visa.Domain;

namespace Visa.Service
{
    public class FingerService
    {
        public static int Add(Fingerprint model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Insertable<Fingerprint>(model).ExecuteReturnIdentity();
        }
        public static bool Edit(Fingerprint model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Updateable<Fingerprint>(model).ExecuteCommand() > 0;
        }
        public static Fingerprint GetModel(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<Fingerprint>().Where(q => q.Id == id).First();

        }
        public static bool Delete(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Deleteable<Fingerprint>().Where(q => q.Id == id).ExecuteCommand() > 0;
        }
    }
}
