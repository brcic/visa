using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Visa.Domain;
using Visa.Service;

namespace Visa.Admin.Controllers
{
    public class CountryController : Controller
    {
        // GET: Country
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            List<Expression<Func<vDistrict, bool>>> listexp=new List<Expression<Func<vDistrict,bool>>>();
            if (Request.QueryString["chinese"] != null) 
            {
                string key = Request.QueryString["chinese"];
                listexp.Add(q => q.ChineseName.Contains(key));
                ViewData["vchi"] = key;
            }
            if (Request.QueryString["eng"] != null)
            {
                string key = Request.QueryString["eng"];
                listexp.Add(q => q.CountryName.Contains(key));
                ViewData["veng"] = key;
            }
            List<vDistrict> lsitems = DistrictService.GetCountryList(listexp);
            return View(lsitems);
        }
        public ActionResult Edit(int id)
        {
            List<SelectListItem> listshow = new List<SelectListItem>();
            listshow.Add(new SelectListItem { Text = "显示", Value = "1" });
            listshow.Add(new SelectListItem { Text = "隐藏", Value = "0" });
            ViewData["vshow"] = listshow;
            List<SelectListItem> listfill= new List<SelectListItem>();
            listfill.Add(new SelectListItem { Text = "机打", Value = "机打" });
            listfill.Add(new SelectListItem { Text = "手写", Value = "手写" });
            ViewData["vfill"] = listfill;

            vDistrict model = DistrictService.GetModel(id);
            if (model == null)
                return HttpNotFound();
            ViewData["vfile"] = model.TemplateFile;
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(vDistrict model)
        {
            if (ModelState.IsValid)
            {
                string filepath = Server.MapPath("~/template/");
                HttpPostedFileBase filev = Request.Files["FileVisa"];
                string filetime = DateTime.Now.ToString("yyyyMMddHHmmss");
                model.TemplateFile = Request.Form["hiddenFile"];
                if (filev.ContentLength > 0)
                {
                    if (System.IO.File.Exists(filepath + model.TemplateFile))
                        System.IO.File.Delete(filepath + model.TemplateFile);
                    string nowfile = model.CountryCode +".docx";
                    model.TemplateFile = nowfile;
                    filev.SaveAs(filepath + nowfile);
                    using (HttpClient client = new HttpClient())
                    {
                        #region
                        MultipartFormDataContent form = new MultipartFormDataContent();
                        StreamContent fileContent = new StreamContent(System.IO.File.OpenRead(filepath + nowfile));
                        fileContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                        fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                        fileContent.Headers.ContentDisposition.FileName = nowfile;
                        form.Add(fileContent);
                        #endregion
                        var response = await client.PostAsync("http://apply.globalvisacenter.org/GatherAPI/UploadTemplate", form);
                        //HttpResponseMessage hrm = response.EnsureSuccessStatusCode();
                       // string returnid = response.Content.ReadAsStringAsync().Result;
                    }
                }
                if (DistrictService.Edit(model))
                {
                    return RedirectToAction("List", "Country");
                }
                else
                    ModelState.AddModelError("", "操作失败！");
            }

            return View(model);
        }
    }
}