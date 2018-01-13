using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word;
namespace DataTool
{
    public class Wps2Pdf : IDisposable
    {
        dynamic wps;
        public Wps2Pdf()
        {
            //这里创建wps实例不知道用了什么骚操作就没有报错过 本机安装的是wps2016

            Type type = Type.GetTypeFromProgID("KWps.Application");
            wps = Activator.CreateInstance(type);
        }
        public void ToPdf(string wpsFilename, string pdfFilename = null)
        {
            if (wpsFilename == null) { throw new ArgumentNullException("wpsFilename"); }
            if (pdfFilename == null)
            {
                pdfFilename = Path.ChangeExtension(wpsFilename, "pdf");
            }
            Console.WriteLine(string.Format(@"正在转换 [{0}] -> [{1}]", wpsFilename, pdfFilename));
            //到处都是dynamic   看的我一脸懵逼
            dynamic doc = wps.Documents.Open(wpsFilename, Visible: false);//这句大概是用wps 打开  word  不显示界面
            doc.ExportAsFixedFormat(pdfFilename, WdExportFormat.wdExportFormatPDF);//doc  转pdf 
            doc.Close();
        }
        public void Dispose()
        {
            if (wps != null) { wps.Quit(); }
        }
    }
}
