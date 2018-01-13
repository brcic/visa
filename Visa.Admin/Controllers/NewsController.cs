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
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            //
            return View();
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

            List<Expression<Func<vArticle, bool>>> listexp = new List<Expression<Func<vArticle, bool>>>();
            int pageIndex = 1;
            // ReSharper disable once SuggestVarOrType_BuiltInTypes
            int PageSize = 10;
            if (Request.QueryString["page"] != null)
                int.TryParse(Request.QueryString["page"], out pageIndex);
            int totalRecord = 0;
            List<vDistrict> lsd = DistrictService.GetCountryItem();
            List<SelectListItem> listcountry = new List<SelectListItem>();
            listcountry.Add(new SelectListItem { Text = "--全部--", Value = "" });
            string country = "";//hdas
            if (Request.QueryString["country"] != null)
            {
                country = Request.QueryString["country"];
                listexp.Add(q => q.RegionCode == country);
            }
            foreach (var item in lsd)
            {
                if (item.CountryCode == country)
                    listcountry.Add(new SelectListItem { Text = item.ChineseName, Value = item.CountryCode, Selected = true });
                else
                    listcountry.Add(new SelectListItem { Text = item.ChineseName, Value = item.CountryCode });
            }
            ViewData["vcountry"] = listcountry;
            List<vArticle> lsv = ArticleService.GetArticleList(pageIndex, PageSize, ref totalRecord, listexp);
            ViewData["PageHtml"] = PagingBar.BuildPage2(pageIndex, totalRecord, PageSize);
            return View(lsv);
        }
         public ActionResult Edit(int id)
         {
             ViewData["vid"] = id;
             vArticle model = ArticleService.GetModel(id);
             if (model == null)
                 return HttpNotFound();
             return View(model);
         }
         [HttpPost]
         [ValidateInput(false)]
         public ActionResult Edit(vArticle model)
         {
             string articleContent = Request.Form["ArticleContent"];
             if (ModelState.IsValid)
             {
                 model.ArticleContent = articleContent;
                 if (ArticleService.Edit(model))
                 {
                     return RedirectToAction("List", "News");
                 }
                 else
                     ModelState.AddModelError("", "操作失败");
             }
             return View(model);
         }
    }
}