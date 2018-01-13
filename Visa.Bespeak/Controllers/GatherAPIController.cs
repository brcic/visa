using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Visa.Bespeak.Models;
using Visa.Domain;
using Visa.Service;

namespace Visa.Bespeak.Controllers
{
    public class GatherAPIController : Controller
    {
        // GET: GatherAPI
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Applicant()
        {
            List<Expression<Func<ApplicantView, bool>>> listexp = new List<Expression<Func<ApplicantView, bool>>>();
            if (Request.QueryString["passport"] != null)
            {
                string passport = Request.QueryString["passport"];
                listexp.Add(q => q.Passport.Contains(passport));
            }
            if (Request.QueryString["give"] != null)
            {
                string give = Request.QueryString["give"];
                listexp.Add(q => q.Givename.Contains(give));
            }
            if (Request.QueryString["sur"] != null)
            {
                string sur = Request.QueryString["sur"];
                listexp.Add(q => q.Surname.Contains(sur));
            }
            if (Request.QueryString["phone"] != null)
            {
                string phone = Request.QueryString["phone"];
                listexp.Add(q => q.Cellphone.Contains(phone));
            }
            if (Request.QueryString["number"] != null)
            {
                string number = Request.QueryString["number"];
                listexp.Add(q => q.ApplyNumber.Contains(number));
            }
            List<ApplicantView> lsap = ApplicantService.GetApplicantView(listexp);
            return Json(lsap, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult FingerAdd(Fingerprint model)
        {
            model.AddTime = DateTime.Now;
            int insertid = FingerService.Add(model);
            ApplicantService.UpdateFingerStstus(model.SqrId);
            return Json(new { status = "1" });
        }
        [HttpPost]
        public ActionResult VideoAdd(VideoMaterial model)
        {
            model.AddTime = DateTime.Now;
            int insertid = VideoService.Add(model);

            ApplicantService.UpdateVideoStstus(model.SqrId);
            return Json(new { status = "1" });
        }
        [HttpPost]
        public ActionResult UploadF()
        {
            HttpFileCollectionBase files= Request.Files;
            string filepath = Server.MapPath("/fingerfile/");
            for (int i = 0; i < files.Count;i++ ) 
            {
                HttpPostedFileBase fileblur = files[i];
                string filename = System.IO.Path.GetFileName(fileblur.FileName);
                fileblur.SaveAs(filepath + filename);
               
            }
            return Json(new { status = "1" });
        }
        [HttpPost]
        public ActionResult UploadV()
        {
            HttpFileCollectionBase files = Request.Files;
            string filepath = Server.MapPath("/videofile/");
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase fileblur = files[i];
                string filename = System.IO.Path.GetFileName(fileblur.FileName);
                fileblur.SaveAs(filepath + filename);

            }
            return Json(new { status = "1" });
        }
        [HttpGet]
        public ActionResult getVideoFile(string sqrid)
        {
            try
            {
                string vfile = VideoService.GetFileBy(sqrid);
                return Content(vfile, "text/plain");
            }
            catch (Exception ex) 
            {
                return Content(ex.Message, "text/plain");
            }
        }
        [HttpGet]
        public ActionResult checkLogin(string userName, string userPwd)
        {
            List<SysAdmin> lss = AdminService.CheckLogin(userName, userPwd);
            return Json(lss, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UploadTemplate()
        {
            HttpFileCollectionBase files = Request.Files;
            string filepath = Server.MapPath("/template/");
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase fileblur = files[i];
                string filename = System.IO.Path.GetFileName(fileblur.FileName);
                fileblur.SaveAs(filepath + filename);
                Wps2Pdf pdf = new Wps2Pdf();
                pdf.ToPdf(filepath + filename, filepath + filename.Replace(".docx", ".pdf"));
                pdf.Dispose();
            }
            return Json(new { status = "1" });
        }
    }
}