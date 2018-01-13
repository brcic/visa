using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visa.Domain
{
    public class DownloadForm
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileValue { get; set; }
        public DateTime AddTime { get; set; }
        public int QzId { get; set; }
    }
}
