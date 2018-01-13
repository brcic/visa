using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Visa.Domain;

namespace Visa.Gather
{
    public partial class UserLogin : Form
    {
        public UserLogin()
        {
            InitializeComponent();
        }
        public static string _userName = string.Empty;
        public static string _qxzName = string.Empty;
        //public static string hosturl = "http://localhost:41765";
        public static string hosturl = "http://apply.globalvisacenter.org";
        //http://apply.globalvisacenter.org/
        Form1 frm1;
        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            if (this.textBoxUser.Text.Trim() == string.Empty)
            {
                MessageBox.Show("用户名不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (this.textBoxPwd.Text.Trim() == string.Empty)
            {
                MessageBox.Show("密码不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.buttonLogin.Enabled = false;
            this.pictureBox1.Visible = true;
            string url = hosturl + "/GatherAPI/checkLogin?userName=" + this.textBoxUser.Text + "&userPwd=" + this.textBoxPwd.Text;
            //设置HttpClientHandler的AutomaticDecompression
            HttpClientHandler handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip, MaxRequestContentBufferSize = 1024000 };
            //创建HttpClient（注意传入HttpClientHandler）
            using (HttpClient http = new HttpClient(handler))
            {
                http.Timeout = TimeSpan.FromSeconds(12000);
                var response = await http.GetAsync(url);
                string jsonStr = response.Content.ReadAsStringAsync().Result;
                this.buttonLogin.Enabled = true;
                this.pictureBox1.Visible = false;
                List<SysAdmin> lsuser = JsonConvert.DeserializeObject<List<SysAdmin>>(jsonStr);
                if (lsuser.Count>0 )
                {
                    _userName = lsuser[0].UserName;
                    _qxzName = lsuser[0].Permission;
                    if (frm1 == null || frm1.IsDisposed)
                    {
                        frm1 = new Form1();
                        frm1.Show(this);
                    }
                    else
                        frm1.Show(this);
                    this.Hide();
                }
                else
                    MessageBox.Show("密码不正确！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void UserLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
