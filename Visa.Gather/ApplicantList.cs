using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Visa.Domain;

namespace Visa.Gather
{
    public partial class ApplicantList : Form
    {
        public ApplicantList()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x0014) // 禁掉清除背景消息
                return;
            base.WndProc(ref m);
        }
        public static BindingList<ApplicantDto> blCustomer;
        private async void buttonFind_Click(object sender, EventArgs e)
        {
            string urlData = "s=pf";
            if (this.textBoxPassport.Text.Trim() != "")
            {
                urlData += "&passport=" + this.textBoxPassport.Text.Trim();
            }
            if (this.textBoxGive.Text.Trim() != "")
            {
                urlData += "&give=" + this.textBoxGive.Text.Trim();
            }
            if (this.textBoxSur.Text.Trim() != "")
            {
                urlData += "&sur=" + this.textBoxSur.Text.Trim();
            }
            if (this.textBoxTelphone.Text.Trim() != "")
            {
                urlData += "&phone=" + this.textBoxTelphone.Text.Trim();
            }
            if (this.textBoxNumber.Text.Trim() != "")
            {
                urlData += "&number=" + this.textBoxNumber.Text.Trim();
            }
            if (this.comboBoxGender.SelectedIndex > 0)
            {
                urlData += "&gender=" + this.comboBoxGender.Text.Trim();
            }
            string url = UserLogin.hosturl + "/GatherAPI/Applicant?" + urlData;
            this.pictureBox1.Visible = true;
            this.buttonFind.Enabled = false;
            //设置HttpClientHandler的AutomaticDecompression
            HttpClientHandler handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
            //创建HttpClient（注意传入HttpClientHandler）
            using (HttpClient http = new HttpClient(handler))
            {
                var response = await http.GetAsync(url);
                //确保HTTP成功状态值
                HttpResponseMessage hrm = response.EnsureSuccessStatusCode();
                this.pictureBox1.Visible = false;
                this.buttonFind.Enabled = true;

                string returnStr = response.Content.ReadAsStringAsync().Result;

                List<ApplicantDto> lsitem = JsonConvert.DeserializeObject<List<ApplicantDto>>(returnStr);
                //DataTable dt = JsonConvert.DeserializeObject<DataTable>(returnStr);
                blCustomer = new BindingList<ApplicantDto>(lsitem);
                this.dataGridView1.DataSource = blCustomer;
                this.dataGridView1.AutoGenerateColumns = false;//不自动  
                this.dataGridView1.Columns[0].Visible = false;
                this.dataGridView1.ClearSelection();
            }
        }
        public string SendMsg(string formUrl, string formData)
        {
            try
            {
                //注意提交的编码 这边是需要改变的 这边默认的是Default：系统当前编码
                byte[] postData = Encoding.UTF8.GetBytes(formData);

                // 设置提交的相关参数 
                HttpWebRequest request = WebRequest.Create(formUrl) as HttpWebRequest;
                Encoding myEncoding = Encoding.UTF8;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = postData.Length;

                // 提交请求数据 
                System.IO.Stream outputStream = request.GetRequestStream();
                outputStream.Write(postData, 0, postData.Length);
                outputStream.Close();

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new System.IO.StreamReader(responseStream, Encoding.GetEncoding("UTF-8"));
                string srcString = reader.ReadToEnd();
                string result = srcString;   //返回值赋值
                reader.Close();
                responseStream.Close();
                response.Close();
                return result;
            }
            catch
            {
                return "error";
            }
        }

        private void ApplicantList_Load(object sender, EventArgs e)
        {
            this.comboBoxGender.SelectedIndex = 0;
            this.BackColor = Color.White;
            this.groupBox1.BackColor = Color.White;

        }

        private void buttonFinger_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow != null)
            {
                int index = this.dataGridView1.CurrentRow.Index;
                GvcFinger gf = new GvcFinger(index);
                gf.ShowDialog(this);
            }
            else
                MessageBox.Show("请选择一个申请人", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex > -1)
            {
                if (e.ColumnIndex == 15)
                {
                    if (e.Value.ToString() == "0")
                    {
                        e.Value = "未采集";
                    }
                    else
                    {
                        e.Value = "已采集"; e.CellStyle.BackColor = Color.Pink;
                    }
                }
                else if (e.ColumnIndex == 16)
                {
                    if (e.Value.ToString() == "0")
                    {
                        e.Value = "未采集";
                    }
                    else
                    {
                        e.Value = "已采集"; e.CellStyle.BackColor = Color.Pink;
                    }
                }
            }
        }

        private void buttonVideo_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.CurrentRow != null)
            {
                int index = this.dataGridView1.CurrentRow.Index;
                GvcVideo gf = new GvcVideo(index);
                gf.ShowDialog(this);
            }
            else
                MessageBox.Show("请选择一个申请人", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
