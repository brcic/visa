using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Visa.Domain;

namespace Visa.Service
{
    public class CustomerService
    {
        public static bool Exist(string email)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Queryable<Customer>().Any(q=>q.Email==email);
        }
        public static bool Exist2(string phone)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Queryable<Customer>().Any(q => q.Cellphone == phone);
        }
        public static int Add(Customer model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Insertable<Customer>(model).ExecuteReturnIdentity();
        }
        public static bool Edit(Customer model)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Updateable(model).ExecuteCommand() > 0;
        }
        public static bool EditOnly(Dictionary<string,object> dic,string uEmail)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Updateable<Customer>(dic).Where(q => q.Email == uEmail).ExecuteCommand() > 0;
        }
        public static Customer GetModel(int id)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Queryable<Customer>().Where(q => q.Id == id).First();
        }
        public static List<Customer> CheckLogin(string userName,string userPwd)
        {
            SqlSugarClient db = SugarDao.GetInstance();
            return db.Queryable<Customer>().Where(q => q.Email == userName && q.UserPwd == userPwd).ToList();
        }
    }
}
