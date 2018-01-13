using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Visa.Bespeak.Models;
using Visa.Domain;
using Visa.Service;
using Xceed.Words.NET;

namespace Visa.Bespeak.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Place()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null)
                {
                    string userData = authTicket.UserData;
                    Customer model = JsonConvert.DeserializeObject<Customer>(userData);
                    CookiesHelper.RemoveAllCache(model.Cellphone);
                }
            }
            List<KeyValuePair<string, string>> lsk = TypeService.GetVisaList("");
            return View();
        }
        [HttpPost]
        public ActionResult GetVisa(FormCollection fc)
        {
            string regioncode = fc["visit"];
            List<KeyValuePair<string, string>> lsk = TypeService.GetVisaList(regioncode);
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append("<option value=\"\">选择签证类别</option>");
            foreach (var item in lsk)
            {
                sb.Append("<option value=\"" + item.Value + "\">" + item.Key + "</option>");
            }

            return Content(sb.ToString(), "text/plain");
        }
        [HttpPost]
        public ActionResult GetEntry(FormCollection fc)
        {
            string eng = fc["eng"];
            string visit = fc["visit"];
            List<KeyValuePair<string, string>> lsk = EntryService.GetEntryData(eng,visit);
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append("<option value=\"0\">无</option>");
            foreach (var item in lsk)
            {
                sb.Append("<option value=\"" + item.Value + "\">" + item.Key + "</option>");
            }

            return Content(sb.ToString(), "text/plain");
        }
        [HttpPost]
        public ActionResult GetFill(FormCollection fc)
        {
            string regioncode = fc["visit"];
            string fmode = DistrictService.GetFill(regioncode);

           return Content(fmode, "text/plain");
        }
        [HttpPost]
        public ActionResult Place(FormCollection fc)
        {
            string visit = fc["visit"];
            string residence = fc["residence"];
            string placename = fc["placename"];
            string vtype = fc["vtype"];
            Sqr sqr = new Sqr();
            sqr.PlaceName = placename;
            sqr.Residence = residence;
            sqr.QzType = vtype;
            sqr.VisitCountry = visit;
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null)
                {
                    string userData = authTicket.UserData;
                    Customer model = JsonConvert.DeserializeObject<Customer>(userData);
                    object objSqr = CookiesHelper.GetCache(model.Cellphone);
                    if (objSqr == null)
                    {
                        CookiesHelper.SetCache(model.Cellphone + "_sqr", sqr);
                    }
                }
            }

            return Json(new { status = "1", message = "验证成功" });
        }
        public ActionResult Applicant(string burl)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return RedirectToAction("Login", "Account");
            }
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
            {
                return RedirectToAction("Login", "Account");
            }
            string userData = authTicket.UserData;
            Customer model = JsonConvert.DeserializeObject<Customer>(userData);
            List<_AppTemp> lsitems = _AppService.GetApplicantList(model.Id);
            if (string.IsNullOrEmpty(burl))
                ViewData["vburl"] = Eip.EncodeBase64(Request.Url.Query);
            else
                ViewData["vburl"] = burl;
            return View(lsitems);
        }
        public ActionResult Add(string burl)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return Json(new { status = "0", message = "会话超时,请重新登录." });
            }
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
            {
                return Json(new { status = "0", message = "会话超时,请重新登录." });
            }
            string userData = authTicket.UserData;
            Customer model = JsonConvert.DeserializeObject<Customer>(userData);
            ViewData["vburl"] = burl;
            return View(model);
        }
        [HttpPost]
        public ActionResult Add(FormCollection fc)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return Json(new { status = "0", message = "会话超时,请重新登录." });
            }
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
            {
                return Json(new { status = "0", message = "会话超时,请重新登录." });
            }

            string userData = authTicket.UserData;
            Customer model = JsonConvert.DeserializeObject<Customer>(userData);
            string givename = fc["givename"];
            string surname = fc["surname"];
            string gender = fc["gender"];
            string uEmail = fc["uEmail"];
            string phone = fc["phone"];
            string passport = fc["passport"];
            string birthday = fc["birthday"];
            string expirydate = fc["expirydate"];
            string[] _births = birthday.Split('/');
            string[] _expis = expirydate.Split('/');
            _AppTemp _model = new _AppTemp();
            _model.ApId = model.Id;//注册人ID
            object objSqr = CookiesHelper.GetCache(model.Cellphone + "_sqr");
            if (objSqr == null)
            {
                return Json(new { status = "0", message = "会话超时,请重新登录." });
            }
            Sqr sqr = objSqr as Sqr;
            _model.ApplyNumber = sqr.VisitCountry + DateTime.Now.ToString("yyyyMMddHHmmss");
            _model.Birthdate = DateTime.Parse(_births[2] + "-" + _births[1] + "-" + _births[0]);
            _model.Cellphone = phone.Trim();
            _model.Email = uEmail.Trim();
            _model.Expirydate = DateTime.Parse(_expis[2] + "-" + _expis[1] + "-" + _expis[0]);
            _model.Gender = gender;
            _model.Givename = givename.ToUpper();
            _model.Nationality = "CHINA";
            _model.Passport = passport;
            _model.Surname = surname.ToUpper();
            _model.TransactionId = "";
            int insertid = _AppService.Add(_model);
            if (insertid > 0)
            {
                return Json(new { status = "1", message = "添加成功！" });
            }
            return Json(new { status = "2", message = "添加失败！" });
        }
        [HttpPost]
        public ActionResult Delete(FormCollection fc)
        {
            string id = fc["id"];
            _AppService.Delete(Convert.ToInt32(id));
            return Json(new { status = "1", message = "操作成功！" });
        }
        public ActionResult Edit(int id)
        {
            _AppTemp model = _AppService.GetModel(id);
            if (model == null)
                return HttpNotFound();
            ViewData["vid"] = id;
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection fc)
        {
            string id = fc["id"];
            string givename = fc["givename"];
            string surname = fc["surname"];
            string gender = fc["gender"];
            string uEmail = fc["uEmail"];
            string phone = fc["phone"];
            string passport = fc["passport"];
            string birthday = fc["birthday"];
            string expirydate = fc["expirydate"];
            string[] _expis = expirydate.Split('/');
            _AppTemp _model = new _AppTemp();
            _model.Birthdate = DateTime.Parse(birthday);
            _model.Cellphone = phone.Trim();
            _model.Email = uEmail.Trim();
            _model.Expirydate = DateTime.Parse(_expis[2] + "-" + _expis[1] + "-" + _expis[0]);
            _model.Gender = gender;
            _model.Givename = givename.ToUpper();
            _model.Nationality = "CHINA";
            _model.Passport = passport;
            _model.Surname = surname.ToUpper();
            _model.TransactionId = "";
            _model.Id = Convert.ToInt32(id);
            if (_AppService.EditOnly(_model))
            {
                return Json(new { status = "1", message = "修改成功！" });
            }
            return Json(new { status = "2", message = "修改失败！" });
        }
        public ActionResult Appointment(string burl)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return Json(new { status = "0", message = "会话超时,请重新登录." });
            }
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
            {
                return Json(new { status = "0", message = "会话超时,请重新登录." });
            }
            string userData = authTicket.UserData;
            Customer model = JsonConvert.DeserializeObject<Customer>(userData);
            string yyxh = _AppService.GetYYXH(model.Id);
            ViewData["vyyxh"] = yyxh;
            //解析参数
            string _queryStr = Eip.DecodeBase64(burl).Replace("?", "");
            ViewData["vquery"] = _queryStr.Replace("&amp;", "&");
            return View();
        }
        public ActionResult Appointment2(int id)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return Json(new { status = "0", message = "会话超时,请重新登录." });
            }
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
            {
                return Json(new { status = "0", message = "会话超时,请重新登录." });
            }
            string userData = authTicket.UserData;
            Customer model = JsonConvert.DeserializeObject<Customer>(userData);
            ViewData["vid"] = id;
            ApplyList apm = ApplyService.GetModel(id);
            if (apm != null)
            {
                ViewData["vyyxh"] = apm.ApplyNumber;
                ViewData["vdate"] = apm.DateName;
                ViewData["vtimerange"] = apm.TimeRange;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Appointment(FormCollection fc)
        {
            string dateName = fc["dateName"];
            string timeName = fc["timeName"];

            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return Json(new { status = "0", message = "会话超时,请重新登录." });
            }
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
            {
                return Json(new { status = "0", message = "会话超时,请重新登录." });
            }
            string userData = authTicket.UserData;
            Customer model = JsonConvert.DeserializeObject<Customer>(userData);
            timeRange tran = new timeRange();
            tran.dateName = dateName;
            tran.timeName = timeName;
            CookiesHelper.SetCache(model.Cellphone + "_confirm", tran);

            //object objSqr = CookiesHelper.GetCache(model.Cellphone);
            //if (objSqr == null)
            //{
            //    return Json(new { status = "0", message = "会话超时,请重新登录." });
            //}
            //Sqr sqr = objSqr as Sqr;


            //ApplyList apply = new ApplyList();
            //apply.AddTime = DateTime.Now;
            //apply.ApplyNumber = "";
            //apply.DateName = dateName;
            //apply.PlaceName = sqr.PlaceName;
            //apply.QzType = int.Parse(sqr.QzType);
            //apply.Residence = sqr.Residence;
            //apply.SqrId = model.Id;
            //apply.TimeRange = timeName;
            //apply.VisitCountry = sqr.VisitCountry;
            //ApplyService.AddTran(apply);
            //CookiesHelper.RemoveAllCache(model.Cellphone);
            return Json(new { status = "1", message = "操作成功！" });
        }
        [HttpPost]
        public ActionResult ResetAppoint(FormCollection fc)
        {
            string dateName = fc["dateName"];
            string timeName = fc["timeName"];
            string sqid = fc["sqid"];
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return Json(new { status = "0", message = "会话超时,请重新登录." });
            }
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
            {
                return Json(new { status = "0", message = "会话超时,请重新登录." });
            }
            string userData = authTicket.UserData;
            Customer model = JsonConvert.DeserializeObject<Customer>(userData);
            timeRange tran = new timeRange();
            tran.dateName = dateName;
            tran.timeName = timeName;
            CookiesHelper.SetCache(model.Cellphone + "_confirm", tran);

            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("Id", sqid);
            dic.Add("DateName", dateName);
            dic.Add("TimeRange", timeName);
            ApplyService.EditOnly(dic);
            //object objSqr = CookiesHelper.GetCache(model.Cellphone);
            //if (objSqr == null)
            //{
            //    return Json(new { status = "0", message = "会话超时,请重新登录." });
            //}
            //Sqr sqr = objSqr as Sqr;


            //ApplyList apply = new ApplyList();
            //apply.AddTime = DateTime.Now;
            //apply.ApplyNumber = "";
            //apply.DateName = dateName;
            //apply.PlaceName = sqr.PlaceName;
            //apply.QzType = int.Parse(sqr.QzType);
            //apply.Residence = sqr.Residence;
            //apply.SqrId = model.Id;
            //apply.TimeRange = timeName;
            //apply.VisitCountry = sqr.VisitCountry;
            //ApplyService.AddTran(apply);
            //CookiesHelper.RemoveAllCache(model.Cellphone);
            return Json(new { status = "1", message = "操作成功！" });
        }
        [HttpPost]
        public ActionResult checkRange(FormCollection fc)
        {
            string dateName = fc["dateName"];
            List<string> lstime = new List<string>();
            lstime.Add("08:00-09:00");
            lstime.Add("09:00-10:00");
            lstime.Add("10:00-11:00");
            lstime.Add("13:00-14:00");
            lstime.Add("14:00-15:00");
            lstime.Add("15:00-16:00");
            lstime.Add("16:00-17:00");
            lstime.Add("17:00-18:00");
            List<ApplyList> lsitems = ApplyService.GetApplyList(dateName);
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (var item in lstime)
            {
                int ysqsl = lsitems.Count(q => q.TimeRange == item);
                if (10 - ysqsl > 0)
                {
                    sb.Append("<tr>");
                    sb.Append("<td><input type=\"radio\" name=\"radioRange\" value=\"" + item + "\" ></td>");
                    sb.Append("<td>" + item + "</td>");
                    sb.Append("</tr>");
                }
            }
            return Content(sb.ToString(), "text/plain");
        }
        public ActionResult Confirm(string dateName, string timeName, string visit, string residence, string placename, string vtype,string vfee)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return RedirectToAction("Login", "Account");
            }
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
            {
                return RedirectToAction("Login", "Account");
            }
            string userData = authTicket.UserData;
            Customer model = JsonConvert.DeserializeObject<Customer>(userData);
            List<_AppTemp> lsitems = _AppService.GetApplicantList(model.Id);

            string tipStr = "";
            //object objSqr = CookiesHelper.GetCache(model.Cellphone + "_sqr");
            //string placeName = "";
            //if (objSqr != null)
            //{
            //    Sqr sqr = objSqr as Sqr;
            //    placeName = sqr.PlaceName;
            //}

            tipStr = "<p>您的预约已经确认于" + dateName + "，时间" + timeName + " 在" + placename + ".请点击链接下载预约信 <a href=\"#\">预约确认单</a></p>";
            ViewData["vtip"] = tipStr;
            string yyxh = _AppService.GetYYXH(model.Id);
            ViewData["vyyxh"] = yyxh;
            return View(lsitems);
        }
        [HttpPost]
        public ActionResult Confirm(FormCollection fc)
        {

            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return Json(new { status = "0", message = "会话超时,请重新登录." });
            }
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
            {
                return Json(new { status = "0", message = "会话超时,请重新登录." });
            }
            string userData = authTicket.UserData;
            Customer model = JsonConvert.DeserializeObject<Customer>(userData);
            List<_AppTemp> lsitems = _AppService.GetApplicantList(model.Id);

            //object objSqr = CookiesHelper.GetCache(model.Cellphone + "_sqr");
            //if (objSqr == null)
            //{
            //    return Json(new { status = "0", message = "会话超时,请重新登录." });
            //}
            //Sqr sqr = objSqr as Sqr;
            //object objConfirm = CookiesHelper.GetCache(model.Cellphone + "_confirm");
            //if (objConfirm == null)
            //{
            //    return Json(new { status = "0", message = "会话超时,请重新登录." });
            //}
            //timeRange trange = objConfirm as timeRange;
            ApplyList apply = new ApplyList();
            apply.AddTime = DateTime.Now;
            apply.ApplyNumber = "";
            apply.DateName = fc["dateName"];
            apply.PlaceName = fc["placename"];
            apply.QzType = fc["vtype"].Replace("%20"," ");
            apply.Residence = fc["residence"];
            apply.SqrId = model.Id;
            apply.TimeRange = fc["timeName"];
            apply.VisitCountry = fc["visit"];
            apply.CheckStatus = 0;
            string yyxh = "";
            string vfee = fc["vfee"];
            ApplyService.AddTran(apply, vfee,out yyxh);
            string filepath = Server.MapPath("~/template/Reservationconfirm.html");
            string templateContent = System.IO.File.ReadAllText(filepath, Encoding.UTF8);
            templateContent = templateContent.Replace("{$yyxh}", yyxh);
            templateContent = templateContent.Replace("{$dateName}", fc["dateName"]);
            templateContent = templateContent.Replace("{$timeName}", fc["timeName"]);
            eMail mail = new eMail();
            mail.fromMail = "service@globalvisacenter.org";
            mail.mBody = templateContent;
            mail.mTitle = "预约确认";
            mail.toMail = model.Email;
            mail.uHost = "smtp.ym.163.com";
            mail.uPassword = "brcicservice2017";
            mail.userName = "service@globalvisacenter.org";
            Task.Run(() =>
            {
                bool isSend = SendService.SendMail(mail);
            });

            //CookiesHelper.RemoveAllCache(model.Cellphone + "_sqr");
            //CookiesHelper.RemoveAllCache(model.Cellphone + "_confirm");
            return Json(new { status = "1", message = "操作成功！" });
        }
        public ActionResult List()
        {
            // x<add+30
            DateTime date30 = DateTime.Now.AddDays(-30);
            List<Expression<Func<ApplyView, bool>>> listexp = new List<Expression<Func<ApplyView, bool>>>();
            listexp.Add(q => q.AddTime > date30);
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                return RedirectToAction("Login", "Account");
            }
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
            {
                return RedirectToAction("Login", "Account");
            }
            string userData = authTicket.UserData;
            Customer model = JsonConvert.DeserializeObject<Customer>(userData);
            listexp.Add(q => q.ApId == model.Id);
            List<ApplyView> lsitems = ApplyService.GetApplyViewList(listexp);
            return View(lsitems);
        }
        [HttpPost]
        public ActionResult Cfile(FormCollection fc)
        {
            string printid = fc["printid"].TrimEnd(',');
            string[] xprint = printid.Split(',');
            List<Applicant> lss = ApplicantService.GetApplicantList(xprint);
            int total = lss.Count;
            string filepath = Server.MapPath("~/template/");
            string filepath1 = Server.MapPath("/outfile/");
            string dcfile = DateTime.Now.ToString("yyyyMMddHHmmssff");
            using (DocX document = DocX.Load(filepath + "appointment.docx"))
            {
                document.ReplaceText("{$date}", DateTime.Now.ToString("yyyy-MM-dd"));
                document.ReplaceText("{$sl}", total.ToString());
                if (total > 0)
                    document.ReplaceText("{$yyxh}", lss[0].ApplyNumber);
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
                ApplyList model1 = null;
                if (total > 0)
                    model1 = ApplyService.GetModelBy(lss[0].ApplyNumber);
                for (int i = 0; i < total; i++)
                {
                    string surname = lss[i].Surname.ToUpper();
                    string geviname = lss[i].Givename.ToUpper();
                    string applyNumber = lss[i].ApplyNumber.ToUpper();
                    string passport = lss[i].Passport.ToUpper();
                    t.Rows[i + 1].Cells[0].Paragraphs[0].Append(geviname + " " + surname);
                    t.Rows[i + 1].Cells[1].Paragraphs[0].Append(applyNumber + "/" + (i + 1).ToString());
                    t.Rows[i + 1].Cells[2].Paragraphs[0].Append(passport);
                    if (model1 != null)
                    {
                        t.Rows[i + 1].Cells[3].Paragraphs[0].Append(model1.DateName + " " + model1.TimeRange.Split('-')[0]);
                        t.Rows[i + 1].Cells[4].Paragraphs[0].Append(model1.QzType);
                    }
                }

                p1.InsertTableAfterSelf(t);

                if (model1 != null)
                    dcfile = model1.ApplyNumber;
                document.SaveAs(filepath1 + dcfile + ".docx");

            }
            Wps2Pdf pdf = new Wps2Pdf();
            pdf.ToPdf(filepath1 + dcfile + ".docx", filepath1 + dcfile + ".pdf");
            pdf.Dispose();
            return Json(new { file = dcfile + ".pdf" });
        }
    }
}