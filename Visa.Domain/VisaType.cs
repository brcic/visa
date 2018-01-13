using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa.Domain
{
    public class VisaType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public string EngName { get; set; }
        public string Overview { get; set; }
        public string VisaFee { get; set; }
        public string DocumentRequired { get; set; }
        public string PhotoSpecification { get; set; }
        public string ProcessingTime { get; set; }
        public string DownloadForm { get; set; }
        public string RegionCode { get; set; }
    }
}
