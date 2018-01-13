using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa.Domain
{
    public class FeeDto
    {
        public double VisaFee { get; set; }
        public double ServiceFee { get; set; }
        public double SupportFee { get; set; }
        public double VIPFee { get; set; }
    }
}
