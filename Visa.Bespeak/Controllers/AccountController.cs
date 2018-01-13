using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Visa.Bespeak.Models;
using Visa.Domain;
using Visa.Service;

namespace Visa.Bespeak.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            ViewData["atip"] = "";
            if (Request.QueryString["status"] != null) 
            {
                ViewData["atip"] = "你的账号已经成功过激活,请登录系统处理你的签证预约！";
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string email = fc["email"];
            string pwd = fc["pwd"];
            string verifycode = fc["verifycode"];
            if (Session["vcode"] == null)
            {
                return Json(new { status = "2", message = "验证码不正确！" });
            }
            if (Session["vcode"].ToString().ToUpper() != verifycode.ToUpper())
            {
                return Json(new { status = "2", message = "验证码不正确！" });
            }
            List<Customer> lsstu = CustomerService.CheckLogin(email.Trim(), GetSwcMD5(pwd.Trim()));
            if (lsstu.Count > 0)
            {
                if (lsstu[0].UserStatus == 0) 
                {
                    return Json(new { status = "3", message = "你的账号还未激活." });
                }
                string RememberMe = Request.Form["RememberMe"];
                string userData = JsonConvert.SerializeObject(lsstu[0]);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, "applicant", DateTime.Now, DateTime.Now.AddDays(1), false, userData, "/");
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                cookie.HttpOnly = false;
                Response.Cookies.Add(cookie);
                return Json(new { status = "1", message = "验证成功." });
            }
            else
            {
                return Json(new { status = "2", message = "账号或密码不正确." });
            }
        }
        [HttpPost]
        public ActionResult Register(FormCollection fc)
        {
            string givname = fc["givname"];
            string surname = fc["surname"];
            string uEmail = fc["uEmail"];
            string cellphone = fc["cellphone"];
            string pwd = fc["pwd"];

            string verifycode = fc["verifycode"];
            if (Session["vcode"] == null)
            {
                return Json(new { status = "2", message = "验证码不正确！" });
            }
            if (Session["vcode"].ToString().ToUpper() != verifycode.ToUpper())
            {
                return Json(new { status = "2", message = "验证码不正确！" });
            }
            if (CustomerService.Exist(uEmail.Trim()))
            {
                return Json(new { status = "4", message = "此邮箱已经被注册了！" });
            }
            if (CustomerService.Exist2(cellphone.Trim()))
            {
                return Json(new { status = "4", message = "此手机号已经被注册了！" });
            }
            Customer model = new Customer();
            model.AddTime = DateTime.Now;
            model.Cellphone = cellphone;
            model.Email = uEmail.Trim();
            model.Givename = givname;
            model.Surname = surname;
            model.UserPwd = GetSwcMD5(pwd);
            model.UserStatus = 0;
            int insertid = CustomerService.Add(model);
            if (insertid > 0)
            {
                //发送激活邮件
                string filepath = Server.MapPath("~/template/regsuccess.html");
                string ukey = TripleDESEncryptService.Encrypt3DES(model.Email + "_" + insertid.ToString()+ "_" + model.Cellphone);
                ukey = HttpUtility.UrlEncode(ukey, Encoding.UTF8);
                string url = "http://apply.globalvisacenter.org/Account/Activate?ukey=" + ukey;
                string templateContent = System.IO.File.ReadAllText(filepath, Encoding.UTF8);
                templateContent = templateContent.Replace("{$url}", url);
                eMail mail = new eMail();
                mail.fromMail = "service@globalvisacenter.org";
                mail.mBody = templateContent;
                mail.mTitle = "签证预约注册成功";
                mail.toMail = model.Email;
                mail.uHost = "smtp.ym.163.com";
                mail.uPassword = "brcicservice2017";
                mail.userName = "service@globalvisacenter.org";
                Task.Run(() =>
                {
                    bool isSend = SendService.SendMail(mail);
                });
                return Json(new { status = "1", message = "注册成功,请的到你注册邮箱中激活此账号！" });
            }
            return Json(new { status = "3", message = "注册失败！" });
        }
        public string GetSwcMD5(string value)
        {
            MD5 algorithm = MD5.Create();
            byte[] data = algorithm.ComputeHash(Encoding.UTF8.GetBytes(value));
            string sh1 = "";
            for (int i = 0; i < data.Length; i++)
            {
                sh1 += data[i].ToString("x2").ToUpperInvariant();
            }
            return sh1;
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult ImgValidCode()
        {
            string code = "";
            byte[] bytes = ValidateCode.CreateValidateGraphic(out code, 4, 108, 38, 20);
            Session["vcode"] = code;
            return File(bytes, @"image/jpeg");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult Activate()
        {
            if (Request.QueryString["ukey"] == null)
                return HttpNotFound();
            string ukey = TripleDESEncryptService.Decrypt3DES(Request.QueryString["ukey"]);
            string[] _zcs = ukey.Split('_');
            //model.Email + "_" + insertid.ToString()+ "_" + model.Cellphone
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("UserStatus", "1");
            CustomerService.EditOnly(dic, _zcs[0]);
            return RedirectToAction("Login", "Account", new  {status="ok" });
        }
    }
}