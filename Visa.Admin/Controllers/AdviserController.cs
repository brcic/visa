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
    public class AdviserController : Controller
    {
        // GET: Adviser
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return RedirectToAction("Login", "Account");
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
                return RedirectToAction("Login", "Account");
            SysAdmin _adviser = JsonConvert.DeserializeObject<SysAdmin>(authTicket.UserData);
            if (_adviser.Permission != "管理员")
            {
                return RedirectToAction("PermissionError", "Account");
            }
            List<SelectListItem> listperm = new List<SelectListItem>();
            listperm.Add(new SelectListItem { Text = "签证员", Value = "签证员" });
            listperm.Add(new SelectListItem { Text = "指纹采集", Value = "指纹采集" });
            listperm.Add(new SelectListItem { Text = "视频采集", Value = "视频采集" });
            listperm.Add(new SelectListItem { Text = "收银", Value = "收银" });
            listperm.Add(new SelectListItem { Text = "管理员", Value = "管理员" });
            ViewData["vperm"] = listperm;
            List<vDistrict> lsd = DistrictService.GetCountryItem();
            List<SelectListItem> listcountry = new List<SelectListItem>();
            foreach (var item in lsd)
            {
                listcountry.Add(new SelectListItem { Text = item.ChineseName, Value = item.CountryCode });
            }
            ViewData["vcountry"] = listcountry;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(SysAdmin model)
        {
            if (ModelState.IsValid)
            {
                if (AdminService.Exist(model.UserName.Trim()))
                {
                    List<SelectListItem> listperm = new List<SelectListItem>();
                    listperm.Add(new SelectListItem { Text = "签证员", Value = "签证员" });
                    listperm.Add(new SelectListItem { Text = "指纹采集", Value = "指纹采集" });
                    listperm.Add(new SelectListItem { Text = "视频采集", Value = "视频采集" });
                    listperm.Add(new SelectListItem { Text = "收银", Value = "收银" });
                    listperm.Add(new SelectListItem { Text = "管理员", Value = "管理员" });
                    ViewData["vperm"] = listperm;
                    ModelState.AddModelError("", "此账号已经存在！");
                    return View(model);
                }
                if (AdminService.Add(model) > 0)
                    return RedirectToAction("List", "Adviser");
                else
                    return View(model);
            }

            return View(model);
        }
        public ActionResult List()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return RedirectToAction("Login", "Account");
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
                return RedirectToAction("Login", "Account");
            SysAdmin _adviser = JsonConvert.DeserializeObject<SysAdmin>(authTicket.UserData);
            if (_adviser.Permission != "管理员")
            {
                return RedirectToAction("PermissionError", "Account");
            }

            List<Expression<Func<SysAdmin, bool>>> listexp = new List<Expression<Func<SysAdmin, bool>>>();
            int pageIndex = 1;
            // ReSharper disable once SuggestVarOrType_BuiltInTypes
            int PageSize = 10;
            if (Request.QueryString["page"] != null)
                int.TryParse(Request.QueryString["page"], out pageIndex);
            int totalRecord = 0;
            listexp.Add(q => q.Id > 1);
            List<SysAdmin> lsitems = AdminService.GetAdminList(pageIndex, ref totalRecord, PageSize, listexp);
            ViewData["PageHtml"] = PagingBar.BuildPage2(pageIndex, totalRecord, PageSize);
            return View(lsitems);
        }
        public ActionResult Edit(int id)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return RedirectToAction("Login", "Account");
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
                return RedirectToAction("Login", "Account");
            SysAdmin _adviser = JsonConvert.DeserializeObject<SysAdmin>(authTicket.UserData);
            if (_adviser.Permission != "管理员")
            {
                return RedirectToAction("PermissionError", "Account");
            }
            List<SelectListItem> listperm = new List<SelectListItem>();
            listperm.Add(new SelectListItem { Text = "签证员", Value = "签证员" });
            listperm.Add(new SelectListItem { Text = "指纹采集", Value = "指纹采集" });
            listperm.Add(new SelectListItem { Text = "视频采集", Value = "视频采集" });
            listperm.Add(new SelectListItem { Text = "收银", Value = "收银" });
            listperm.Add(new SelectListItem { Text = "管理员", Value = "管理员" });
            ViewData["vperm"] = listperm;
            List<vDistrict> lsd = DistrictService.GetCountryItem();
            List<SelectListItem> listcountry = new List<SelectListItem>();
            foreach (var item in lsd)
            {
                listcountry.Add(new SelectListItem { Text = item.ChineseName, Value = item.CountryCode });
            }
            ViewData["vcountry"] = listcountry;
            SysAdmin model = AdminService.GetModel(id);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SysAdmin model)
        {
            if (ModelState.IsValid)
            {
                if (AdminService.Edit(model))
                    return RedirectToAction("List", "Adviser");
                else
                    return View(model);
            }

            return View(model);
        }
    }
}