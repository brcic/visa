using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Visa.Service;

namespace Visa.Admin.Models
{
    public class SingleParameter
    {
        private static SingleParameter instance;
        private static readonly object obj = new object();
        private static List<KeyValuePair<string,string>> lsregion;
        public static SingleParameter CreateInstance()
        {
            if (instance == null)
            {
                lock (obj)
                {
                    if (instance == null)
                    {
                        instance = new SingleParameter();
                    }
                }
            }
            return instance;
        }
        private SingleParameter()
        {
            lsregion = DistrictService.GetDistrictList();
        }
     
        public string GetRegionName(string region)
        {
            lock (obj)
            {
                return lsregion.Find(q => q.Key == region).Value;
            }
        }
    }
}