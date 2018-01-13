using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Visa.Domain;
using Visa.Service;

namespace Visa.Admin.Controllers
{
    public class ApplyController : Controller
    {
        // GET: Apply
        public ActionResult List()
        {
            List<Expression<Func<AppointView, bool>>> listexp = new List<Expression<Func<AppointView, bool>>>();
            int pageIndex = 1;
            // ReSharper disable once SuggestVarOrType_BuiltInTypes
            int PageSize = 10;
            if (Request.QueryString["page"] != null)
                int.TryParse(Request.QueryString["page"], out pageIndex);
            int totalRecord = 0;
            List<vDistrict> lsd = DistrictService.GetCountryItem();
            List<SelectListItem> listcountry = new List<SelectListItem>();
            listcountry.Add(new SelectListItem { Text = "--全部--", Value = "" });
            string country = "";
            if (Request.QueryString["country"] != null)
            {
                country = Request.QueryString["country"];
                listexp.Add(q => q.VisitCountry == country);
            }
            foreach (var item in lsd)
            {
                if (item.CountryCode == country)
                    listcountry.Add(new SelectListItem { Text = item.ChineseName, Value = item.CountryCode, Selected = true });
                else
                    listcountry.Add(new SelectListItem { Text = item.ChineseName, Value = item.CountryCode });
            }
            ViewData["vcountry"] = listcountry;
            if (Request.QueryString["number"] != null)
            {
                string key = Request.QueryString["number"];
                ViewData["vnumber"] = key;
                listexp.Add(q => q.ApplyNumber.Contains(key));
            }
            if (Request.QueryString["phone"] != null)
            {
                string key = Request.QueryString["phone"];
                ViewData["vphone"] = key;
                listexp.Add(q => q.Cellphone.Contains(key));
            }
            if (Request.QueryString["mail"] != null)
            {
                string key = Request.QueryString["mail"];
                ViewData["vmail"] = key;
                listexp.Add(q => q.Email.Contains(key));
            }
            if (Request.QueryString["stime"] != null)
            {
                string stime = Request.QueryString["stime"];
                ViewData["vstime"] = stime;
                listexp.Add(q => q.DateName == stime);
            }
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return RedirectToAction("Login", "Account");
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
                return RedirectToAction("Login", "Account");


            SysAdmin _adviser = JsonConvert.DeserializeObject<SysAdmin>(authTicket.UserData);
            if (_adviser.Permission != "管理员")
            {
                listexp.Add(q => q.VisitCountry == _adviser.UserCountry);
            }
            List<AppointView> lsitems = ApplyService.GetAppointList(pageIndex, PageSize, ref totalRecord, listexp);
            ViewData["PageHtml"] = PagingBar.BuildPage2(pageIndex, totalRecord, PageSize);
            ViewData["vburl"] = Eip.EncodeBase64(Request.Url.PathAndQuery);
            return View(lsitems);
        }

        public ActionResult Applicant(string applynumber, string burl)
        {
            List<Expression<Func<ApplicantView, bool>>> listexp = new List<Expression<Func<ApplicantView, bool>>>();
            listexp.Add(q => q.ApplyNumber == applynumber);
            ViewData["vback"] = Eip.DecodeBase64(burl);
            List<ApplicantView> lsitem = ApplicantService.GetApplicantView(listexp);
            return View(lsitem);
        }
        [HttpPost]
        public ActionResult Delete(FormCollection fc)
        {
            string id = fc["id"];
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return Json(new { status = "2" });
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
                return Json(new { status = "2" });


            SysAdmin _adviser = JsonConvert.DeserializeObject<SysAdmin>(authTicket.UserData);
            if (_adviser.Permission == "管理员")
            {
                ApplyService.Delete(Convert.ToInt32(id));
                return Json(new { status = "1" });
            }
            else
            {
                return Json(new { status = "2" });
            }
        }
        [HttpPost]
        public ActionResult Adopt(FormCollection fc)
        {
            string id = fc["id"];
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return Json(new { status = "2" });
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
                return Json(new { status = "2" });


            SysAdmin _adviser = JsonConvert.DeserializeObject<SysAdmin>(authTicket.UserData);
            string transactionId = "GZ" + DateTime.Now.ToString("yyyyMMddHHmmss") + _adviser.Id;
            ApplicantService.Approval(_adviser.UserName, transactionId, id);
            return Json(new { status = "1" });
        }
        public ActionResult Receipt()
        {

            List<Expression<Func<AppointView, bool>>> listexp = new List<Expression<Func<AppointView, bool>>>();
            int pageIndex = 1;
            // ReSharper disable once SuggestVarOrType_BuiltInTypes
            int PageSize = 10;
            if (Request.QueryString["page"] != null)
                int.TryParse(Request.QueryString["page"], out pageIndex);
            int totalRecord = 0;
            List<vDistrict> lsd = DistrictService.GetCountryItem();
            List<SelectListItem> listcountry = new List<SelectListItem>();
            listcountry.Add(new SelectListItem { Text = "--全部--", Value = "" });
            string country = "";
            if (Request.QueryString["country"] != null)
            {
                country = Request.QueryString["country"];
                listexp.Add(q => q.VisitCountry == country);
            }
            foreach (var item in lsd)
            {
                if (item.CountryCode == country)
                    listcountry.Add(new SelectListItem { Text = item.ChineseName, Value = item.CountryCode, Selected = true });
                else
                    listcountry.Add(new SelectListItem { Text = item.ChineseName, Value = item.CountryCode });
            }
            ViewData["vcountry"] = listcountry;
            if (Request.QueryString["number"] != null)
            {
                string key = Request.QueryString["number"];
                ViewData["vnumber"] = key;
                listexp.Add(q => q.ApplyNumber.Contains(key));
            }
            if (Request.QueryString["phone"] != null)
            {
                string key = Request.QueryString["phone"];
                ViewData["vphone"] = key;
                listexp.Add(q => q.Cellphone.Contains(key));
            }
            if (Request.QueryString["mail"] != null)
            {
                string key = Request.QueryString["mail"];
                ViewData["vmail"] = key;
                listexp.Add(q => q.Email.Contains(key));
            }
            if (Request.QueryString["stime"] != null)
            {
                string stime = Request.QueryString["stime"];
                ViewData["vstime"] = stime;
                listexp.Add(q => q.DateName == stime);
            }
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return RedirectToAction("Login", "Account");
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
                return RedirectToAction("Login", "Account");


            SysAdmin _adviser = JsonConvert.DeserializeObject<SysAdmin>(authTicket.UserData);
            if (_adviser.Permission == "管理员" || _adviser.Permission == "收银")
            {

            }
            else
            {
                return RedirectToAction("PermissionError", "Account");
            }
            List<AppointView> lsitems = ApplyService.GetAppointList(pageIndex, PageSize, ref totalRecord, listexp);
            ViewData["PageHtml"] = PagingBar.BuildPage2(pageIndex, totalRecord, PageSize);

            return View(lsitems);
        }
        public ActionResult Print(string number)
        {
            ApplyList lsf = ApplyService.GetModelBy(number);
            if (lsf!=null)
            {
                ViewData["vVisaFee"] = lsf.totalVisaFee;
                ViewData["vServiceFee"] = lsf.totalServiceFee;
                ViewData["vSupportFee"] = lsf.totalSupportFee;
                ViewData["vVIPFee"] = lsf.totalVIPFee;
                ViewData["vReceiptNo"] = lsf.ReceiptNo;
            }
            return View();
        }
        [HttpPost]
        public ActionResult checkPay(FormCollection fc)
        {
            string id = fc["id"];
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return Json(new { status = "2" });
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
                return Json(new { status = "2" });
            ApplyList model = ApplyService.GetModel(Convert.ToInt32(id));
            string receiptNo = id.PadLeft(6, '0') + "/" + DateTime.Now.Year + "-VF-";
            if (model != null)
                receiptNo += model.VisitCountry;
            SysAdmin _adviser = JsonConvert.DeserializeObject<SysAdmin>(authTicket.UserData);

            ApplyService.Payconfirm(_adviser.UserName, receiptNo,id);
            return Json(new { status = "1" });
        }
    }
}