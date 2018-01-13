using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Visa.Domain;
using Xceed.Words.NET;

namespace DataTool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonDo_Click(object sender, EventArgs e)
        {
            this.buttonDo.Enabled = false;
            string constr = "Data Source=DESKTOP-2B71689;Initial Catalog=GlobalVisa;User Id=sa;Password=123456;";

            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig() { ConnectionString = constr, IsAutoCloseConnection = true, DbType = DbType.SqlServer });
            List<vDistrict> lsr = db.Queryable<vDistrict>().Where(q => q.Id > 0).ToList();
            string submitInPerson = this.textBoxDownloadForm.Text.Trim();
            List<vArticle> lsv = new List<vArticle>();
            foreach (var item in lsr)
            {
                vArticle model = new vArticle();
                model.RegionCode = item.CountryCode;
                model.ArticleContent = submitInPerson.Replace("加拿大", item.ChineseName);
                model.ArticleTitle = "附加服务";
                model.KeyWords = "附加服务";
                model.PageTitle = "附加服务";
                model.PageDescription = "附加服务";
                model.TypeId = 4;
                lsv.Add(model);
            }
            db.Insertable<vArticle>(lsv).ExecuteCommand();
            this.buttonDo.Enabled = true;
            MessageBox.Show("ok");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.button1.Enabled = false;
            string constr = "Data Source=DQ6866;Initial Catalog=GlobalVisa;User Id=sa;Password=wangjunjx8868?!@#;";

            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig() { ConnectionString = constr, IsAutoCloseConnection = true, DbType = DbType.SqlServer });
            List<VisaType> lsr = db.Queryable<VisaType>().Where(q => q.RegionCode == "GH").ToList();
            VisaType vmo = db.Queryable<VisaType>().Where(q => q.Id == 31).First();
            List<VisaType> lsv = new List<VisaType>();
            foreach (var item in lsr)
            {
                VisaType model = new VisaType();

                model.DocumentRequired = vmo.DocumentRequired;
                model.DownloadForm = vmo.DownloadForm;
                model.Overview = vmo.Overview;
                model.PhotoSpecification = vmo.PhotoSpecification;
                model.ProcessingTime = vmo.ProcessingTime;
                model.VisaFee = vmo.VisaFee;
                model.Id = item.Id;
                lsv.Add(model);
            }
            db.Updateable<VisaType>(lsv).UpdateColumns(q => new { q.DocumentRequired, q.DownloadForm, q.Overview, q.PhotoSpecification, q.ProcessingTime, q.VisaFee }).ExecuteCommand();
            this.button1.Enabled = true;
            MessageBox.Show("ok");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string constr = "Data Source=DESKTOP-2B71689;Initial Catalog=GlobalVisa; User Id=sa;Password=123456;";
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig() { ConnectionString = constr, DbType = DbType.SqlServer, IsAutoCloseConnection = true }); //默认SystemTable
            Dictionary<string, string> dic = db.DbFirst.Where(this.textBoxEng.Text).ToClassStringList();
            foreach (var item in dic)
            {
                this.textBoxDocument.Text = item.Value;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Wps2Pdf pdf = new Wps2Pdf();
            pdf.ToPdf(@"C:\Users\lenovo\Desktop\索马里签证表.docx", @"C:\Users\lenovo\Desktop\索马里签证表.pdf");
            MessageBox.Show("ok");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (DocX document = DocX.Load(@"日期.docx"))
            {

                Paragraph p1 = document.ParagraphsDeepSearch[9];
                var t = document.AddTable(2, 5);
                t.Design = TableDesign.TableGrid;
                t.Alignment = Alignment.center;
                t.Rows[0].Cells[0].Paragraphs[0].Append("申请人姓名");
                t.Rows[0].Cells[1].Paragraphs[0].Append("预约序号");
                t.Rows[0].Cells[2].Paragraphs[0].Append("护照号码");
                t.Rows[0].Cells[3].Paragraphs[0].Append("预约日期和时间");
                t.Rows[0].Cells[4].Paragraphs[0].Append("签证类型");
                // Add a row at the end of the table and sets its values.

                t.Rows[1].Cells[0].Paragraphs[0].Append("QI WANG");
                t.Rows[1].Cells[1].Paragraphs[0].Append("XXXXX1");

                p1.InsertTableAfterSelf(t);
                document.SaveAs("ceshi.docx");
                MessageBox.Show("ok");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
        }
    }
}
