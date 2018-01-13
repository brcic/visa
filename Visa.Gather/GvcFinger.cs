using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Visa.Domain;

namespace Visa.Gather
{
    public partial class GvcFinger : Form
    {
        public GvcFinger()
        {
            InitializeComponent();
        }
        int sindex = 0;
        public GvcFinger(int _sindex)
        {
            InitializeComponent();
            sindex = _sindex;
        }
        int FMatchType;
        int fpcHandle;
        int FingerCount = 1;
        string filepath = "";
        List<int> lsf = new List<int>();
        int sqrid = 0;
        ApplicantDto model=null;
        private void GvcFinger_Load(object sender, EventArgs e)
        {
            filepath = Application.StartupPath;
            this.labelPress.Text = "开始采集,请放右手拇指！";
            if (!Directory.Exists(filepath + "\\fingertp"))
            {
                Directory.CreateDirectory(filepath + "\\fingertp");
            }
            model = ApplicantList.blCustomer[sindex];
            this.buttonSave.Enabled = false;
            if (model != null)
            {
                this.textBoxGive.Text = model.Givename;
                this.textBoxNumber.Text = model.ApplyNumber;
                this.textBoxPassport.Text = model.Passport;
                this.textBoxSur.Text = model.Surname;
                this.comboBoxGender.Text = model.Gender;
                this.textBoxNationality.Text = model.Nationality;
                this.textBoxTelphone.Text = model.Cellphone;
                this.textBoxEmail.Text = model.Email;
                this.textBoxExpire.Text = model.Expirydate.ToString("yyyy-MM-dd");
                sqrid = model.Id;
                if (model.fingerStatus == 1)
                {
                  
                    this.labelPress.Text = "该申请人指纹已采集";
                    this.pictureBox1.ImageLocation = filepath+ "\\zwtp\\" + this.textBoxPassport.Text + "_1.jpg";
                }
                else 
                {
                    Task.Run(() =>
                    {
                        try
                        {
                            this.axZKFPEngX1.FPEngineVersion = "10";
                            if (this.axZKFPEngX1.InitEngine() == 0)
                            {
                                fpcHandle = this.axZKFPEngX1.CreateFPCacheDB();
                                FMatchType = 1;
                                Console.WriteLine("初始化设备成功");
                                if (axZKFPEngX1.IsRegister)
                                {
                                    axZKFPEngX1.CancelEnroll();
                                }
                                //axZKFPEngX1.EnrollCount = 2;
                                this.axZKFPEngX1.BeginEnroll();
                                this.axZKFPEngX1.BeginCapture();
                            }
                            else
                            {
                                MessageBox.Show("没有检测到指纹采集设备或者版本不支持", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch
                        {
                            MessageBox.Show("没有检测到指纹采集设备或者版本不支持", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                    });
                }
            }
           
        }

        private void GvcFinger_FormClosed(object sender, FormClosedEventArgs e)
        {
            //axZKFPEngX1.CancelEnroll();
            //axZKFPEngX1.EndEngine();
        }
        private void axZKFPEngX1_OnCapture(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnCaptureEvent e)
        {
            bool RegChanged = false;
            char[] buffer = new char[80];
            if (FMatchType == 1)
            {
                RegChanged = true;
                if (axZKFPEngX1.VerRegFingerFile(filepath + "\\fingertp\\" + this.textBoxPassport.Text + ".tp", e.aTemplate, false, ref RegChanged))//VerFinger(ref FRegTemplate, e.aTemplate, false, ref RegChanged))
                {
                    this.buttonSave.Enabled = true;
                    this.labelPress.Text = "指纹比对成功";
                }
                else
                {
                    //axZKFPEngX1.RemoveRegTemplateFromFPCacheDB(fpcHandle, FingerCount);
                    this.buttonSave.Enabled = false;
                    this.labelPress.Text = "指纹比对失败";
                    if (axZKFPEngX1.IsRegister)
                    {
                        axZKFPEngX1.CancelEnroll();
                        axZKFPEngX1.CancelCapture();
                    }
                    this.axZKFPEngX1.BeginEnroll();
                    this.axZKFPEngX1.BeginCapture();
                    FingerCount = 1;
                    lsf.Clear();
                }
            }
        }

        private void axZKFPEngX1_OnImageReceived(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnImageReceivedEvent e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            int dc = g.GetHdc().ToInt32();
            axZKFPEngX1.PrintImageAt(dc, 0, 0, pictureBox1.Width, pictureBox1.Height);
            string cjxh = this.textBoxPassport.Text + "_" + sqrid + "_" + FingerCount + ".jpg";
            axZKFPEngX1.SaveJPG(filepath + "\\zwtp\\" + cjxh);

            Console.WriteLine("OnImageReceived=" + FingerCount);

        }

        private void axZKFPEngX1_OnEnroll(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnEnrollEvent e)
        {
            Console.WriteLine("OnEnroll=触发");
            Console.WriteLine("actionResult=" + e.actionResult);
            if (e.actionResult)
            {
                byte[] tmp = (byte[])e.aTemplate;

                //axZKFPEngX1.AddRegTemplateToFPCacheDB(fpcHandle, 1, e.aTemplate);

                File.WriteAllBytes(filepath + "\\fingertp\\" + this.textBoxPassport.Text + ".tp", tmp);

            }
        }

        private void axZKFPEngX1_OnFeatureInfo(object sender, AxZKFPEngXControl.IZKFPEngXEvents_OnFeatureInfoEvent e)
        {

            String sTemp = "";
            if (axZKFPEngX1.IsRegister)
            {
                sTemp = "登记状态: 还需要按压";
                sTemp = sTemp + (axZKFPEngX1.EnrollIndex).ToString() + "次手指!";
            }
            sTemp = sTemp + " 指纹质量";

            if (e.aQuality != 0)
            { sTemp = sTemp + "不合格: " + e.aQuality.ToString(); }
            else
            {
                sTemp = sTemp + "合格";
                lsf.Add(FingerCount);
            }
            Console.WriteLine("OnFeatureInfo=" + FingerCount);
            FingerCount++;
            this.labelPress.Text = sTemp;
        }
        private async void buttonSave_Click(object sender, EventArgs e)
        {
            //if (axZKFPEngX1.IsRegister)
            //{
            //    axZKFPEngX1.CancelEnroll();
            //}
            //this.axZKFPEngX1.BeginCapture();
            //FingerCount = 1;
            //lsf.Clear();
            Fingerprint finger = new Fingerprint();
            finger.ApplyNumber = this.textBoxNumber.Text;
            finger.Cellphone = this.textBoxTelphone.Text;
            finger.Email = this.textBoxEmail.Text;
            finger.Expirydate = this.textBoxExpire.Text;
            string fileStr = "";
            foreach (int f in lsf)
            {
                fileStr += this.textBoxPassport.Text + "_" +sqrid+"_"+ f + ".jpg" + ",";
            }
            finger.fingerFile = fileStr.TrimEnd(',');
            finger.Gender = this.comboBoxGender.Text;
            finger.Givename = this.textBoxGive.Text;
            finger.Nationality = this.textBoxNationality.Text;
            finger.Passport = this.textBoxPassport.Text;
            finger.Surname = this.textBoxSur.Text;
            finger.SqrId = sqrid;
            finger.Operator = UserLogin._userName;
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            string jsonStr = JsonConvert.SerializeObject(finger, Newtonsoft.Json.Formatting.Indented, timeFormat);

            this.pictureBox2.Visible = true;
            this.buttonSave.Enabled = false;
            //创建HttpClient（注意传入HttpClientHandler）
            using (HttpClient client = new HttpClient())
            {
                #region
                MultipartFormDataContent form = new MultipartFormDataContent();
                foreach (int f in lsf)
                {
                    if (System.IO.File.Exists(filepath + "\\zwtp\\" + this.textBoxPassport.Text + "_" + sqrid + "_" + f + ".jpg"))
                    {
                        StreamContent fileContent = new StreamContent(File.OpenRead(filepath + "\\zwtp\\" + this.textBoxPassport.Text + "_" + sqrid + "_" + f + ".jpg"));
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                        fileContent.Headers.ContentDisposition.FileName = this.textBoxPassport.Text + "_" + sqrid + "_" + f + ".jpg";
                        form.Add(fileContent);
                    }
                }

                #endregion
                var x = await client.PostAsync(UserLogin.hosturl + "/GatherAPI/UploadF", form);
            }
            string url = UserLogin.hosturl + "/GatherAPI/FingerAdd";
            using (HttpClient http = new HttpClient())
            {
                //使用FormUrlEncodedContent做HttpContent
                var response = await http.PostAsync(url, new StringContent(jsonStr, Encoding.UTF8, "application/json"));
                //确保HTTP成功状态值
                HttpResponseMessage hrm = response.EnsureSuccessStatusCode();
                this.pictureBox1.Visible = false;
                this.buttonSave.Enabled = true;
                if (hrm.IsSuccessStatusCode == false)
                {
                    MessageBox.Show("无法连接服务器");

                }
                //await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip）
                string returnid = response.Content.ReadAsStringAsync().Result;
                //ObjectsMapper<Delivery, FH> mapper = ObjectMapperManager.DefaultInstance.GetMapper<Delivery, FH>();
                //FH dst = mapper.Map(model);
                //CustomerList.blDelivery[rindex] = dst;
                model.fingerStatus = 1;
                ApplicantList.blCustomer[sindex] = model;
                this.pictureBox2.Visible = false;
                this.buttonSave.Enabled = true;
                this.Close();
            }
        }
    }
}
