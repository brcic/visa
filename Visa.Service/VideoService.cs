using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visa.Domain;

namespace Visa.Service
{
    public class VideoService
    {

        public static int Add(VideoMaterial model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Insertable<VideoMaterial>(model).ExecuteReturnIdentity();
        }
        public static bool Edit(VideoMaterial model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Updateable<VideoMaterial>(model).ExecuteCommand() > 0;
        }
        public static VideoMaterial GetModel(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();

            return db.Queryable<VideoMaterial>().Where(q => q.Id == id).First();

        }
        public static string GetFileBy(string id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            string getSql = "SELECT top 1 videoFile FROM VideoMaterial WHERE SqrId=@SqrId order by Id desc";
            return db.Ado.GetString(getSql, new { SqrId=id });

        }
        public static bool Delete(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Deleteable<VideoMaterial>().Where(q => q.Id == id).ExecuteCommand() > 0;
        }
    }
}
