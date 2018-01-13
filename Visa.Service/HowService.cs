using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visa.Domain;

namespace Visa.Service
{
    public class HowService
    {
        public static HowToApply GetModel(string region, int typeId)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<HowToApply>().Where(q => q.RegionCode == region && q.TypeId == typeId).First();

        }
    }
}
