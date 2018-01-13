using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Visa.Domain;

namespace Visa.Gather
{
    public partial class GvcVideo : Form
    {
        IVideo.IVideo iVideo;
        public GvcVideo()
        {
            InitializeComponent();

        }
        int sindex = 0;
        public GvcVideo(int _sindex)
        {
            InitializeComponent();
            sindex = _sindex;
        }
        string filepath = "";
        int sqrid = 0;
        ApplicantDto model = null;
        private async void GvcVideo_Load(object sender, EventArgs e)
        {
            filepath = Application.StartupPath;
            if (!Directory.Exists(filepath + "\\material"))
            {
                Directory.CreateDirectory(filepath + "\\material");
            }
          
            model = ApplicantList.blCustomer[sindex];
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
                if (model.videoStatus == 1)
                {
                    this.labelTip.Text = "材料已采集";
                    this.buttonCamera.Enabled = false;
                    this.buttonGather.Enabled = false;
                    this.buttonSave.Enabled = false;
                    this.buttonDevicesProtity.Enabled = false;
                    HttpClientHandler handlerfile = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
                    //创建HttpClient（注意传入HttpClientHandler）
                    using (HttpClient http = new HttpClient(handlerfile))
                    {
                        var response = await http.GetAsync(UserLogin.hosturl + "/GatherAPI/getVideoFile?sqrid=" + sqrid);
                        //确保HTTP成功状态值
                        HttpResponseMessage hrm = response.EnsureSuccessStatusCode();
                        this.pictureBox1.Visible = false;
                        string returnStr = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine("文件="+returnStr);
                        string[] vfiles = returnStr.Split(',');
                        this.comboBoxMar.Items.Add("选择要展示的图片");
                        for (int x = 0; x < vfiles.Length; x++)
                        {
                            this.comboBoxMar.Items.Add(vfiles[x]);
                        }
                        this.comboBoxMar.SelectedIndex = 0;
                    }
                    this.comboBoxMar.Visible = true;
                }
                else 
                {
                    this.comboBoxMar.Visible = false;
                    iVideo = new IVideo.IVideo();
                    iVideo.LoadMe(this.Handle.ToInt32());
                    iVideo.SetLoc(10, 400, 760, 600);
                    iVideo.SetImageFormat("jpeg");
                    this.buttonGather.Enabled = false;
                    int result = iVideo.OpenDevices();
                    if (result == 1)
                    {

                        Console.WriteLine("打开设备成功");
                        // 取得视频能力
                        if (iVideo.IsRunning)
                        {
                            string[] res = iVideo.GetListResolution().Split(',');
                            for (var i = 0; i < res.Length; i++)
                            {
                                Console.WriteLine(res[i]);
                            }
                            this.buttonGather.Enabled = true;
                        }
                        else
                        {
                            this.buttonCamera.Enabled = false;
                            this.buttonDevicesProtity.Enabled = false;
                            this.buttonGather.Enabled = false;
                            this.buttonSave.Enabled = false;
                            MessageBox.Show("未检测到视频采集设备！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        this.buttonCamera.Enabled = false;
                        this.buttonDevicesProtity.Enabled = false;
                        this.buttonGather.Enabled = false;
                        this.buttonSave.Enabled = false;
                        MessageBox.Show("未检测到视频采集设备", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }
        int photoCount = 1;
        private void buttonGather_Click(object sender, EventArgs e)
        {
            int result = iVideo.GetSnap();
            if (result == 1)
            {
                this.Invoke(new Action(() => { this.labelTip.Text = "拍摄成功,请放下一个材料！";  }));
                //保存图片
                string pxxh = this.textBoxPassport.Text + "_" + sqrid + "_" + photoCount + ".jpg";
                iVideo.SaveSnap(filepath + "\\material\\" + pxxh, 100);
                //if (showFrm == null)
                //    showFrm = new ShowFrm();
                //showFrm.TopMost = true;
                //showFrm.ShowPic(iVideo.SnapBitmap);
                //showFrm.FormClosing += (a, b) => { showFrm = null; };
                //showFrm.Show();
                lsf.Add(photoCount);
                photoCount++;
                Task.Delay(2000).ContinueWith(duan => {
                    this.Invoke(new Action(() =>
                    {
                        this.labelTip.Text = "";
                    }));
                });
            }
            else
            {
                this.labelTip.Text = "拍摄失败";
            }
        }

        private void buttonCamera_Click(object sender, EventArgs e)
        {
            int result = iVideo.ToggleDevice();
            if (result == 1)
            {
                this.labelTip.Text = "切换成功";
            }
            else
            {
                this.labelTip.Text = "切换失败";
            }
        }

        private void GvcVideo_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (iVideo != null)
            {
                iVideo.CloseDevices();
                iVideo.Dispose();
            }
        }

        private void buttonDevicesProtity_Click(object sender, EventArgs e)
        {
            int result = iVideo.OpenPropertyPage();
            if (result == 1) { this.labelTip.Text = "操作成功"; }
            else
            { this.labelTip.Text = "操作失败"; }
        }
        List<int> lsf = new List<int>();
      
        private async void buttonSave_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("此申请人所需材料都采集完成了吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) 
            {
                return;
            }
            VideoMaterial finger = new VideoMaterial();
            finger.ApplyNumber = this.textBoxNumber.Text;
            finger.Cellphone = this.textBoxTelphone.Text;
            finger.Email = this.textBoxEmail.Text;
            finger.Expirydate = this.textBoxExpire.Text;
            string fileStr = "";
            foreach (int f in lsf)
            {
                fileStr += this.textBoxPassport.Text + "_" +sqrid+ "_"+f+ ".jpg" + ",";
            }
            if (fileStr == "") 
            {
                MessageBox.Show("请采集此申请人的各种证件材料！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            finger.videoFile = fileStr.TrimEnd(',');
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

            this.pictureBox1.Visible = true;
            this.buttonSave.Enabled = false;
            //创建HttpClient（注意传入HttpClientHandler）
            using (HttpClient client = new HttpClient())
            {
                #region
                MultipartFormDataContent form = new MultipartFormDataContent();
                foreach (int f in lsf)
                {
                    if (System.IO.File.Exists(filepath + "\\material\\" + this.textBoxPassport.Text + "_" + sqrid + "_" + f + ".jpg"))
                    {
                        StreamContent fileContent = new StreamContent(File.OpenRead(filepath + "\\material\\" + this.textBoxPassport.Text + "_" + sqrid + "_" + f + ".jpg"));
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                        fileContent.Headers.ContentDisposition.FileName = this.textBoxPassport.Text + "_" + sqrid + "_" + f + ".jpg";
                        form.Add(fileContent);
                    }
                }

                #endregion
                var x = await client.PostAsync(UserLogin.hosturl  + "/GatherAPI/UploadV", form);
            }
            string url = UserLogin.hosturl + "/GatherAPI/VideoAdd";
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
                model.videoStatus = 1;
                ApplicantList.blCustomer[sindex] = model;
                this.pictureBox1.Visible = false;
                this.buttonSave.Enabled = true;
                this.Close();
            }
        }

        private void comboBoxMar_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string filename = this.comboBoxMar.Text;
            if (this.comboBoxMar.SelectedIndex > 0) 
            {
                if (System.IO.File.Exists(filepath + "\\material\\" + filename))
                    this.pictureBoxMar.ImageLocation = filepath + "\\material\\" + filename;
            }
        }
    }
}
