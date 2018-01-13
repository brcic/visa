using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa.Domain
{
    public class vArticle
    {
        public int Id { get; set; }
        public string ArticleTitle { get; set; }
        public string PageTitle { get; set; }
        public string KeyWords { get; set; }
        public string PageDescription { get; set; }
        public string ArticleContent { get; set; }
        public byte TypeId { get; set; }
        public string RegionCode { get; set; }
    }
}
