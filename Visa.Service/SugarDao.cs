using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa.Service
{
    public class SugarDao
    {
        private SugarDao()
        {

        }
        public static SqlSugarClient GetInstance()
        {
            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["ConnStr"].ConnectionString; //这里可以动态根据cookies或session实现多库切换
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig() { ConnectionString = connection, IsAutoCloseConnection = true, DbType = DbType.SqlServer });
            return db;
        }
    }
}
