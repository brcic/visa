using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Visa.Domain;
using Visa.Service;

namespace Visa.Bespeak.Controllers
{
    [Authorize]
    public class CancelController : Controller
    {
        // GET: Cancel
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            List<Expression<Func<ApplyView, bool>>> listexp = new List<Expression<Func<ApplyView, bool>>>();
            listexp.Add(q => q.ApStatus == 0);
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
        public ActionResult Delete(FormCollection fc)
        {
            string qxid = fc["qxid"];
            string[] qxids = qxid.Split(',');
            string filepath = Server.MapPath("~/template/cancel.html");
            string templateContent = System.IO.File.ReadAllText(filepath, Encoding.UTF8);
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
            string yyxhstr = "";
            for (int i = 0; i < qxids.Length; i++) 
            {
                yyxhstr += qxids[i] + "/" + (i + 1).ToString() + ",";
            }
            templateContent = templateContent.Replace("{$yyxh}", yyxhstr.TrimEnd(','));
            eMail mail = new eMail();
            mail.fromMail = "service@globalvisacenter.org";
            mail.mBody = templateContent;
            mail.mTitle = "预约取消确认";
            mail.toMail = model.Email;
            mail.uHost = "smtp.ym.163.com";
            mail.uPassword = "brcicservice2017";
            mail.userName = "service@globalvisacenter.org";
            Task.Run(() =>
            {
                bool isSend = SendService.SendMail(mail);
            });
            ApplicantService.Delete(qxids);
            ApplyService.Clear();
            return Json(new { status = "1", message = "ok" });
        }
    }
}