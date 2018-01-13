using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa.Domain
{
    public class vDistrict
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public string ChineseName { get; set; }
        public string CountryCode { get; set; }
        public byte ContinentId { get; set; }
        public byte IsShow { get; set; }
        public string TemplateFile { get; set; }
        public string FillMode { get; set; }
    }
}
