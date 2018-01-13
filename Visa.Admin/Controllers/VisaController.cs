using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Visa.Admin.Models;
using Visa.Domain;
using Visa.Service;

namespace Visa.Admin.Controllers
{
    public class VisaController : Controller
    {
        // GET: Visa
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
            List<vDistrict> lsd = DistrictService.GetCountryItem();
            List<SelectListItem> listcountry = new List<SelectListItem>();
            listcountry.Add(new SelectListItem { Text = "--全部--", Value = "" });
          
            foreach (var item in lsd)
            {
                listcountry.Add(new SelectListItem { Text = item.ChineseName, Value = item.CountryCode });
            }
            ViewData["vcountry"] = listcountry;
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Add(VisaType model)
        {
            string overview = Request.Form["containerOverview"];
            string visaFee = Request.Form["containerVisaFee"];
            string documentRequire = Request.Form["containerDocumentRequire"];
            string photo = Request.Form["containerPhoto"];
            string processing = Request.Form["containerProcessing"];
            string download = Request.Form["containerDownload"];
            if (ModelState.IsValid)
            {
                model.DocumentRequired = documentRequire;
                model.DownloadForm = download;
                model.Overview = overview;
                model.PhotoSpecification = photo;
                model.ProcessingTime = processing;
                model.VisaFee = visaFee;
                int insertid = TypeService.Add(model);
                if (insertid>0)
                {
                    return RedirectToAction("List", "Visa", new { country = model.RegionCode});
                }
                else
                    ModelState.AddModelError("", "操作失败");
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
            List<Expression<Func<VisaType, bool>>> listexp = new List<Expression<Func<VisaType, bool>>>();
            int pageIndex = 1;
            // ReSharper disable once SuggestVarOrType_BuiltInTypes
            int PageSize = 20;
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
                listexp.Add(q => q.RegionCode == country);
            }
            foreach (var item in lsd) 
            {
                if (item.CountryCode == country)
                    listcountry.Add(new SelectListItem { Text = item.ChineseName, Value = item.CountryCode,Selected=true });
                else
                    listcountry.Add(new SelectListItem { Text = item.ChineseName, Value = item.CountryCode });
            }
            ViewData["vcountry"] = listcountry;
            List<VisaType> lsitems = TypeService.GetTypeList(pageIndex, PageSize, ref totalRecord, listexp);
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
            List<vDistrict> lsd = DistrictService.GetCountryItem();
            List<SelectListItem> listcountry = new List<SelectListItem>();
            listcountry.Add(new SelectListItem { Text = "--全部--", Value = "" });

            foreach (var item in lsd)
            {
                listcountry.Add(new SelectListItem { Text = item.CountryName, Value = item.CountryCode });
            }
            ViewData["vcountry"] = listcountry;
            VisaType model = TypeService.GetModel(id);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(VisaType model)
        {
            string overview = Request.Form["containerOverview"];
            string visaFee = Request.Form["containerVisaFee"];
            string documentRequire = Request.Form["containerDocumentRequire"];
            string photo = Request.Form["containerPhoto"];
            string processing = Request.Form["containerProcessing"];
            string download = Request.Form["containerDownload"];
            if (ModelState.IsValid) 
            {
                model.DocumentRequired = documentRequire;
                model.DownloadForm = download;
                model.Overview = overview;
                model.PhotoSpecification = photo;
                model.ProcessingTime = processing;
                model.VisaFee = visaFee;
                if (TypeService.Edit(model))
                {
                    return RedirectToAction("List", "Visa", new  {country=model.RegionCode });
                }
                else
                    ModelState.AddModelError("", "操作失败");
            }
            return View(model);
        }
       
        public ActionResult FormAdd(int vid,string country)
        {
            ViewData["vid"] = vid;
            ViewData["vcountry"] = country;
            return View();
        }
        [HttpPost]
        public ActionResult FormAdd(DownloadForm model)
        {
            if (ModelState.IsValid) 
            {
                HttpPostedFileBase ufile = Request.Files["fileform"];
                if (ufile.ContentLength > 0)
                {
                    string filepath = Server.MapPath("~/Material/");
                    string exname = System.IO.Path.GetExtension(ufile.FileName);
                    string nowfile = DateTime.Now.ToString("yyyyMMddHHmmss") + exname;
                    model.FileValue = nowfile;
                    ufile.SaveAs(filepath + nowfile);
                   
                }
                model.AddTime = DateTime.Now;
                model.QzId = int.Parse(Request.QueryString["vid"]);
                int insertid= DownloadServce.Add(model);
                if (insertid > 0)
                {
                    return RedirectToAction("FormList", "Visa", new { vid = Request.QueryString["vid"], country = Request.QueryString["country"] });
                }
                else
                    ModelState.AddModelError("", "操作失败！");
            }
            return View(model);
        }
        public ActionResult FormList(string vid)
        {
           
            ViewData["vid"] = vid;
            SingleParameter single = SingleParameter.CreateInstance();
            if (Request.QueryString["country"]!=null) 
            {
                string country = Request.QueryString["country"];
                ViewData["vcountry"] = country;
                ViewData["vregion"] = single.GetRegionName(country);
            }
        
            List<DownloadForm> lsv = DownloadServce.GetFormList(vid);
            return View(lsv);
        }
        public ActionResult FormEdit(string vid, int pid, string country)
        {
            ViewData["vid"] = vid;
            ViewData["vpid"] = pid;
            ViewData["vcountry"] = country;
            DownloadForm model = DownloadServce.GetModel(pid);
            if (model == null)
                return HttpNotFound();
            ViewData["vfile"] = "";
            if (model.FileValue!=null)
                ViewData["vfile"] = model.FileValue;
            return View(model);
        }
        [HttpPost]
        public ActionResult FormEdit(DownloadForm model)
        {
            if (ModelState.IsValid)
            {
                model.FileValue = Request.Form["hiddenfile"];
                HttpPostedFileBase ufile = Request.Files["fileform"];
                if (ufile.ContentLength > 0)
                {
                    string filepath = Server.MapPath("~/Material/");
                    if (System.IO.File.Exists(filepath + model.FileValue))
                        System.IO.File.Delete(filepath + model.FileValue);
                    string exname = System.IO.Path.GetExtension(ufile.FileName);
                    string nowfile = DateTime.Now.ToString("yyyyMMddHHmmss") + exname;
                    model.FileValue = nowfile;
                    ufile.SaveAs(filepath + nowfile);

                }
                model.Id = int.Parse(Request.QueryString["pid"]);
                if (DownloadServce.Edit(model))
                {
                    return RedirectToAction("FormList", "Visa", new { vid = Request.QueryString["vid"], country = Request.QueryString["country"] });
                }
                else
                    ModelState.AddModelError("", "操作失败！");
            }
            return View(model);
        }


        public ActionResult EntryAdd(int vid, string country)
        {
            ViewData["vid"] = vid;
            ViewData["vcountry"] = country;
            return View();
        }
        [HttpPost]
        public ActionResult EntryAdd(vEntry model)
        {
            if (ModelState.IsValid)
            {
               
                model.Vid = int.Parse(Request.QueryString["vid"]);
                int insertid = EntryService.Add(model);
                if (insertid > 0)
                {
                    return RedirectToAction("EntryList", "Visa", new { vid = Request.QueryString["vid"], country = Request.QueryString["country"] });
                }
                else
                    ModelState.AddModelError("", "操作失败！");
            }
            return View(model);
        }
        public ActionResult EntryList(string vid)
        {

            ViewData["vid"] = vid;
            SingleParameter single = SingleParameter.CreateInstance();
            if (Request.QueryString["country"] != null)
            {
                string country = Request.QueryString["country"];
                ViewData["vcountry"] = country;
                ViewData["vregion"] = single.GetRegionName(country);
            }

            List<vEntry> lsv = EntryService.GetEntryList(vid);
            return View(lsv);
        }
        public ActionResult EntryEdit(string vid, int pid, string country)
        {
            ViewData["vid"] = vid;
            ViewData["vpid"] = pid;
            ViewData["vcountry"] = country;
            vEntry model = EntryService.GetModel(pid);
            if (model == null)
                return HttpNotFound();
            return View(model);
        }
        [HttpPost]
        public ActionResult EntryEdit(vEntry model)
        {
            if (ModelState.IsValid)
            {
               
                model.Id = int.Parse(Request.QueryString["pid"]);
                if (EntryService.Edit(model))
                {
                    return RedirectToAction("EntryList", "Visa", new { vid = Request.QueryString["vid"], country = Request.QueryString["country"] });
                }
                else
                    ModelState.AddModelError("", "操作失败！");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(FormCollection fc)
        {
            string id = fc["id"];
            TypeService.Delete(Convert.ToInt32(id));
            return Json(new { status = "1" });
        }
        [HttpPost]
        public ActionResult Delete2(FormCollection fc)
        {
            string id = fc["id"];
            DownloadServce.Delete(Convert.ToInt32(id));
            return Json(new { status="1"});
        }
        [HttpPost]
        public ActionResult Delete3(FormCollection fc)
        {
            string id = fc["id"];
            EntryService.Delete(Convert.ToInt32(id));
            return Json(new { status = "1" });
        }
    }
}