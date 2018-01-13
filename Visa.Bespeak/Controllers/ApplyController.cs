using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Visa.Bespeak.Models;
using Visa.Domain;
using Visa.Service;
using Xceed.Words.NET;

namespace Visa.Bespeak.Controllers
{
    public class ApplyController : Controller
    {
        // GET: Apply
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add(int id, string visit)
        {
            Applicant apmo = ApplicantService.GetModel(id);
            string xb = "";
            if (apmo != null)
            {
                ViewData["vgiven"] = apmo.Givename;
                ViewData["vsur"] = apmo.Surname;
                ViewData["vnation"] = apmo.Nationality;
                ViewData["vPassport"] = apmo.Passport;
                xb = apmo.Gender;
            }

            List<SelectListItem> listgender = new List<SelectListItem>();
            listgender.Add(new SelectListItem { Text = "-性别-", Value = "" });
            listgender.Add(new SelectListItem { Text = "男", Value = "男", Selected = xb == "男" ? true : false });
            listgender.Add(new SelectListItem { Text = "女", Value = "女", Selected = xb == "女" ? true : false });
            ViewData["vgender"] = listgender;

            List<SelectListItem> listmarital = new List<SelectListItem>();
            listmarital.Add(new SelectListItem { Text = "-婚姻状况-", Value = "" });
            listmarital.Add(new SelectListItem { Text = "已婚", Value = "Married" });
            listmarital.Add(new SelectListItem { Text = "未婚", Value = "Single" });
            listmarital.Add(new SelectListItem { Text = "离婚", Value = "Divorced" });
            listmarital.Add(new SelectListItem { Text = "丧偶", Value = "Widowed" });
            listmarital.Add(new SelectListItem { Text = "其他", Value = "Other" });
            ViewData["vmarital"] = listmarital;

            List<SelectListItem> listpassport = new List<SelectListItem>();
            listpassport.Add(new SelectListItem { Text = "-护照种类-", Value = "" });
            listpassport.Add(new SelectListItem { Text = "外交", Value = "Diplomatic" });
            listpassport.Add(new SelectListItem { Text = "公务、官员", Value = "Service or official" });
            listpassport.Add(new SelectListItem { Text = "普通", Value = "Ordinary" });
            listpassport.Add(new SelectListItem { Text = "因公", Value = "Public Affairs" });
            listpassport.Add(new SelectListItem { Text = "其他", Value = "Other" });
            ViewData["vpasstype"] = listpassport;


            List<SelectListItem> listrush = new List<SelectListItem>();
            listrush.Add(new SelectListItem { Text = "-请选择-", Value = "0" });
            if (visit == "GH")
            {
                listrush.Add(new SelectListItem { Text = "不加急", Value = "不加急" });
                listrush.Add(new SelectListItem { Text = "加急(72小时)", Value = "加急(72小时)" });
                listrush.Add(new SelectListItem { Text = "加急(24小时)", Value = "加急(24小时)" });
            }
            else
            {
                listrush.Add(new SelectListItem { Text = "不加急", Value = "720" });
                listrush.Add(new SelectListItem { Text = "加急(72小时)", Value = "1080" });
                listrush.Add(new SelectListItem { Text = "加急(24小时)", Value = "1440" });
            }
            //listrush.Add(new SelectListItem { Text = "加急(72小时)", Value = "Express for 72 hours" });
            //listrush.Add(new SelectListItem { Text = "加急(24小时)", Value = "Express for 24 hours" });
            ViewData["vrush"] = listrush;

            List<SelectListItem> listentry = new List<SelectListItem>();
            listentry.Add(new SelectListItem { Text = "-计划入境次数-", Value = "" });
            listentry.Add(new SelectListItem { Text = "一次入境", Value = "One entry valid for 3 months from application" });
            listentry.Add(new SelectListItem { Text = "二次入境", Value = "Two entries valid for 3 to 6 months from application" });
            listentry.Add(new SelectListItem { Text = "半年多次入境", Value = "Multiple entries valid for 6 months from application" });
            listentry.Add(new SelectListItem { Text = "一年多次入境", Value = "Multiple entries valid for 12 months from application" });
            listentry.Add(new SelectListItem { Text = "其他", Value = "Other " });
            ViewData["ventry"] = listentry;

            List<SelectListItem> listexpense = new List<SelectListItem>();
            listexpense.Add(new SelectListItem { Text = "你本人", Value = "Yourself" });
            listexpense.Add(new SelectListItem { Text = "邀请单位或个人", Value = "Inviter" });
            listexpense.Add(new SelectListItem { Text = "父母或法定监护人", Value = "Parent(s) or legal guardian(s)" });
            listexpense.Add(new SelectListItem { Text = "其他", Value = "Other" });
            ViewData["vexpense"] = listexpense;

            List<SelectListItem> overstayed = new List<SelectListItem>();
            overstayed.Add(new SelectListItem { Text = "否", Value = "No" });
            overstayed.Add(new SelectListItem { Text = "是", Value = "Yes" });

            ViewData["voverstay"] = overstayed;

            List<SelectListItem> haverefused = new List<SelectListItem>();
            haverefused.Add(new SelectListItem { Text = "否", Value = "No" });
            haverefused.Add(new SelectListItem { Text = "是", Value = "Yes" });

            ViewData["vhaverefused"] = haverefused;

            List<SelectListItem> havecriminal = new List<SelectListItem>();
            havecriminal.Add(new SelectListItem { Text = "否", Value = "No" });
            havecriminal.Add(new SelectListItem { Text = "是", Value = "Yes" });
            ViewData["vhavecriminal"] = havecriminal;

            List<SelectListItem> havedisease = new List<SelectListItem>();

            havedisease.Add(new SelectListItem { Text = "否", Value = "No" });
            havedisease.Add(new SelectListItem { Text = "是", Value = "Yes" });
            ViewData["vhavedisease"] = havedisease;

            List<SelectListItem> visitInfected = new List<SelectListItem>();

            visitInfected.Add(new SelectListItem { Text = "否", Value = "No" });
            visitInfected.Add(new SelectListItem { Text = "是", Value = "Yes" });
            ViewData["vvisitInfected"] = visitInfected;
            ViewData["vsqid"] = id;

            return View();
        }
        [HttpPost]
        public ActionResult Add(ApplyForm model)
        {
            if (ModelState.IsValid)
            {
                model.InviterName = Request.Form["InviterName"].ToUpper();
                model.InviterAddress = Request.Form["InviterAddress"].ToUpper();
                model.InviterPhone = Request.Form["InviterPhone"].ToUpper();
                model.ResidenceName = Request.Form["ResidencePlace"].ToUpper();
                model.ResidenceAddress = Request.Form["ResidenceAddress"].ToUpper();
                model.ResidencePhone = Request.Form["ResidencePhone"].ToUpper();
                model.VisitedPlace = Request.Form["VisitedPlace"].ToUpper();
                model.VisitedPurpose = Request.Form["VisitedPurpose"].ToUpper();
                model.VisitedDate = Request.Form["VisitedDate"].ToUpper();
                model.Occupation = Request.Form["checkboxOccu"];
                model.VisitPurpose = Request.Form["checkboxPurpose"];
                int insertid = ApplyformService.Add(model);
                if (insertid > 0)
                {
                    string id = Request.QueryString["sqid"];
                    model.SqId = Convert.ToInt32(id);
                    ApplicantService.UpdateStstus(model.SqId);
                    //ApplicantService.UpdateVisaFee(model.RushService,model.SqId);
                    CreateApply(model, id);
                    return RedirectToAction("List", "Order");
                }
                else
                    ModelState.AddModelError("", "操作失败");
            }
            return View(model);
        }
        public ActionResult Edit(int id, string visit)
        {

            ApplyForm model = ApplyformService.GetModelBy(id);
            if (model == null)
                return HttpNotFound();
            string xb = model.Gender;
            string hyqk = model.MaritalStatus;
            string hzzl = model.PassportType;
            string sfjj = model.RushService;
            string rjcs = model.EntryNumber;
            string fycd = model.WhoCostYourExpense;
            string stayed = model.HaveOverstayed;
            string refused = model.HaveRefused;
            string criminal = model.HaveCriminal;
            string disease = model.HaveDisease;
            string infected = model.VisitInfected;
            ViewData["vOccupation"] = model.Occupation;
            ViewData["vPurpose"] = model.VisitPurpose;
            ViewData["vInviter1"] = model.InviterName;
            ViewData["vInviter2"] = model.InviterAddress;
            ViewData["vInviter3"] = model.InviterPhone;

            ViewData["vResidence1"] = model.ResidenceName;
            ViewData["vResidence2"] = model.ResidenceAddress;
            ViewData["vResidence3"] = model.ResidencePhone;

            ViewData["vVisited1"] = model.VisitedPlace;
            ViewData["vVisited2"] = model.VisitedPurpose;
            ViewData["vVisited3"] = model.VisitedDate;
            ViewData["vgxid"] = model.Id;
            List<SelectListItem> listgender = new List<SelectListItem>();
            listgender.Add(new SelectListItem { Text = "-性别-", Value = "" });
            listgender.Add(new SelectListItem { Text = "男", Value = "男", Selected = xb == "男" ? true : false });
            listgender.Add(new SelectListItem { Text = "女", Value = "女", Selected = xb == "女" ? true : false });
            ViewData["vgender"] = listgender;

            List<SelectListItem> listmarital = new List<SelectListItem>();
            listmarital.Add(new SelectListItem { Text = "-婚姻状况-", Value = "" });
            listmarital.Add(new SelectListItem { Text = "已婚", Value = "Married", Selected = hyqk == "Married" ? true : false });
            listmarital.Add(new SelectListItem { Text = "未婚", Value = "Never Married", Selected = hyqk == "Never Married" ? true : false });
            listmarital.Add(new SelectListItem { Text = "离婚", Value = "Divorced", Selected = hyqk == "Divorced" ? true : false });
            listmarital.Add(new SelectListItem { Text = "丧偶", Value = "Widowed", Selected = hyqk == "Widowed" ? true : false });
            listmarital.Add(new SelectListItem { Text = "其他", Value = "Other", Selected = hyqk == "Other" ? true : false });
            ViewData["vmarital"] = listmarital;

            List<SelectListItem> listpassport = new List<SelectListItem>();
            listpassport.Add(new SelectListItem { Text = "-护照种类-", Value = "" });
            listpassport.Add(new SelectListItem { Text = "外交", Value = "Diplomatic", Selected = hzzl == "Diplomatic" ? true : false });
            listpassport.Add(new SelectListItem { Text = "公务、官员", Value = "Service or official", Selected = hzzl == "Service or official" ? true : false });
            listpassport.Add(new SelectListItem { Text = "普通", Value = "Ordinary", Selected = hzzl == "Ordinary" ? true : false });
            listpassport.Add(new SelectListItem { Text = "因公", Value = "Public Affairs", Selected = hzzl == "Public Affairs" ? true : false });
            listpassport.Add(new SelectListItem { Text = "其他", Value = "Other", Selected = hzzl == "Other" ? true : false });
            ViewData["vpasstype"] = listpassport;


            List<SelectListItem> listrush = new List<SelectListItem>();
            listrush.Add(new SelectListItem { Text = "-请选择-", Value = "0" });
            if (visit == "GH")
            {
                listrush.Add(new SelectListItem { Text = "不加急", Value = "720", Selected = sfjj == "720" ? true : false });
                listrush.Add(new SelectListItem { Text = "加急(72小时)", Value = "1080", Selected = sfjj == "1080" ? true : false });
                listrush.Add(new SelectListItem { Text = "加急(24小时)", Value = "1440", Selected = sfjj == "1440" ? true : false });
            }
            else
            {
                listrush.Add(new SelectListItem { Text = "不加急", Value = "720", Selected = sfjj == "720" ? true : false });
                listrush.Add(new SelectListItem { Text = "加急(72小时)", Value = "1080", Selected = sfjj == "1080" ? true : false });
                listrush.Add(new SelectListItem { Text = "加急(24小时)", Value = "1440", Selected = sfjj == "1440" ? true : false });
            }
            ViewData["vrush"] = listrush;

            List<SelectListItem> listentry = new List<SelectListItem>();
            listentry.Add(new SelectListItem { Text = "-计划入境次数-", Value = "" });
            listentry.Add(new SelectListItem { Text = "一次入境", Value = "One entry valid for 3 months from application", Selected = rjcs == "One entry valid for 3 months from application" ? true : false });
            listentry.Add(new SelectListItem { Text = "二次入境", Value = "Two entries valid for 3 to 6 months from application", Selected = rjcs == "Two entries valid for 3 to 6 months from application" ? true : false });
            listentry.Add(new SelectListItem { Text = "半年多次入境", Value = "Multiple entries valid for 6 months from application", Selected = rjcs == "Multiple entries valid for 6 months from application" ? true : false });
            listentry.Add(new SelectListItem { Text = "一年多次入境", Value = "Multiple entries valid for 12 months from application", Selected = rjcs == "Multiple entries valid for 12 months from application" ? true : false });
            listentry.Add(new SelectListItem { Text = "其他", Value = "Other " });
            ViewData["ventry"] = listentry;

            List<SelectListItem> listexpense = new List<SelectListItem>();
            listexpense.Add(new SelectListItem { Text = "你本人", Value = "Yourself", Selected = fycd == "Yourself" ? true : false });
            listexpense.Add(new SelectListItem { Text = "邀请单位或个人", Value = "Inviter", Selected = fycd == "Inviter" ? true : false });
            listexpense.Add(new SelectListItem { Text = "父母或法定监护人", Value = "Parent(s) or legal guardian(s)", Selected = fycd == "Parent(s) or legal guardian(s)" ? true : false });
            listexpense.Add(new SelectListItem { Text = "其他", Value = "Other" });
            ViewData["vexpense"] = listexpense;

            List<SelectListItem> overstayed = new List<SelectListItem>();
            overstayed.Add(new SelectListItem { Text = "否", Value = "No", Selected = stayed == "No" ? true : false });
            overstayed.Add(new SelectListItem { Text = "是", Value = "Yes", Selected = stayed == "Yes" ? true : false });

            ViewData["voverstay"] = overstayed;

            List<SelectListItem> haverefused = new List<SelectListItem>();
            haverefused.Add(new SelectListItem { Text = "否", Value = "No", Selected = refused == "No" ? true : false });
            haverefused.Add(new SelectListItem { Text = "是", Value = "Yes", Selected = refused == "Yes" ? true : false });

            ViewData["vhaverefused"] = haverefused;

            List<SelectListItem> havecriminal = new List<SelectListItem>();
            havecriminal.Add(new SelectListItem { Text = "否", Value = "No", Selected = criminal == "No" ? true : false });
            havecriminal.Add(new SelectListItem { Text = "是", Value = "Yes", Selected = criminal == "Yes" ? true : false });
            ViewData["vhavecriminal"] = havecriminal;

            List<SelectListItem> havedisease = new List<SelectListItem>();

            havedisease.Add(new SelectListItem { Text = "否", Value = "No", Selected = disease == "No" ? true : false });
            havedisease.Add(new SelectListItem { Text = "是", Value = "Yes", Selected = disease == "Yes" ? true : false });
            ViewData["vhavedisease"] = havedisease;

            List<SelectListItem> visitInfected = new List<SelectListItem>();

            visitInfected.Add(new SelectListItem { Text = "否", Value = "No", Selected = infected == "No" ? true : false });
            visitInfected.Add(new SelectListItem { Text = "是", Value = "Yes", Selected = infected == "Yes" ? true : false });
            ViewData["vvisitInfected"] = visitInfected;
            ViewData["vsqrid"] = model.SqId;

            return View(model);
        }
        [HttpPost]
        public ActionResult Edit(ApplyForm model)
        {
            if (ModelState.IsValid)
            {
                model.InviterName = Request.Form["InviterName"].ToUpper();
                model.InviterAddress = Request.Form["InviterAddress"].ToUpper();
                model.InviterPhone = Request.Form["InviterPhone"].ToUpper();
                model.ResidenceName = Request.Form["ResidencePlace"].ToUpper();
                model.ResidenceAddress = Request.Form["ResidenceAddress"].ToUpper();
                model.ResidencePhone = Request.Form["ResidencePhone"].ToUpper();
                model.VisitedPlace = Request.Form["VisitedPlace"].ToUpper();
                model.VisitedPurpose = Request.Form["VisitedPurpose"].ToUpper();
                model.VisitedDate = Request.Form["VisitedDate"].ToUpper();
                model.Occupation = Request.Form["checkboxOccu"];
                model.VisitPurpose = Request.Form["checkboxPurpose"];
                model.Id = Convert.ToInt32(Request.QueryString["gxid"]);
                string sqrid = Request.Form["hiddensqrid"];
                if (ApplyformService.Edit(model))
                {
                    CreateApply(model, sqrid);
                    ApplicantService.UpdateVisaFee(model.RushService, int.Parse(sqrid));
                    return RedirectToAction("List", "Order");
                }
                else
                    ModelState.AddModelError("", "操作失败");
            }
            return View(model);
        }
        public void CreateApply(ApplyForm model, string id)
        {
            string vfile = DistrictService.GetTemplateFile(id);
            if (vfile != "" && vfile != "0")
            {
                string filepath = Server.MapPath("/template/");
                using (DocX document = DocX.Load(filepath + vfile))
                {
                    #region 替换
                    document.ReplaceText("{Surname}", model.Surname.ToUpper());
                    document.ReplaceText("{Givname}", model.Givname.ToUpper());
                    if (model.Prename != null)
                        document.ReplaceText("{Prename}", model.Prename.ToUpper());
                    if (model.Chiname != null)
                        document.ReplaceText("{Chiname}", model.Chiname.ToUpper());
                    if (model.CNationality != null)
                        document.ReplaceText("{CNationality}", model.CNationality.ToUpper());
                    if (model.FNationality != null)
                        document.ReplaceText("{FNationality}", model.FNationality.ToUpper());
                    string[] csrq = model.Dateofbirth.Split('/');

                    document.ReplaceText("{csd}", csrq[0]);
                    document.ReplaceText("{csm}", csrq[1]);
                    document.ReplaceText("{csy}", csrq[2]);
                    if (model.Placeofbirth != null)
                        document.ReplaceText("{Placeofbirth}", model.Placeofbirth.ToUpper());
                    document.ReplaceText("{IDNumber}", model.IDNumber.ToUpper());
                    if (model.ContactAddress != null)
                        document.ReplaceText("{ContactAddress}", model.ContactAddress.ToUpper());
                    if (model.Telephone != null)
                        document.ReplaceText("{Telephone}", model.Telephone.ToUpper());

                    document.ReplaceText("{PassportNumber}", model.PassportNumber.ToUpper());
                    document.ReplaceText("{DateIssue}", model.DateIssue.Replace("/", "-"));
                    if (model.PlaceIssue != null)
                        document.ReplaceText("{PlaceIssue}", model.PlaceIssue.ToUpper());
                    document.ReplaceText("{ExpiryDate}", model.ExpiryDate.Replace("/", "-"));

                    string[] inviters1 = model.InviterName.ToUpper().Split(',');
                    document.ReplaceText("{InviterName1}", inviters1[0]);
                    document.ReplaceText("{InviterName2}", inviters1[1]);
                    string[] inviters2 = model.InviterAddress.Split(',');
                    document.ReplaceText("{InviterAddress1}", inviters2[0]);
                    document.ReplaceText("{InviterAddress2}", inviters2[1]);
                    string[] inviters3 = model.InviterPhone.Split(',');
                    document.ReplaceText("{InviterPhone1}", inviters3[0]);
                    document.ReplaceText("{InviterPhone2}", inviters3[1]);
                    if (model.ResidenceName != null)
                    {
                        string[] residence1 = model.ResidenceName.ToUpper().Split(',');
                        document.ReplaceText("{ResidenceName1}", residence1[0]);
                        document.ReplaceText("{ResidenceName2}", residence1[1]);
                        document.ReplaceText("{ResidenceName3}", residence1[2]);
                    }
                    if (model.ResidenceAddress != null)
                    {
                        string[] residence2 = model.ResidenceAddress.ToUpper().Split(',');
                        document.ReplaceText("{ResidenceAddress1}", residence2[0]);
                        document.ReplaceText("{ResidenceAddress2}", residence2[1]);
                        document.ReplaceText("{ResidenceAddress3}", residence2[2]);
                    }
                    if (model.ResidencePhone != null)
                    {
                        string[] residence3 = model.ResidencePhone.Split(',');
                        document.ReplaceText("{ResidencePhone1}", residence3[0]);
                        document.ReplaceText("{ResidencePhone2}", residence3[1]);
                        document.ReplaceText("{ResidencePhone3}", residence3[2]);
                    }

                    if (model.InsuranceCompanyAccount != null)
                        document.ReplaceText("{InsuranceCompanyAccount}", model.InsuranceCompanyAccount.ToUpper());
                    if (model.HomeAddress != null)
                        document.ReplaceText("{HomeAddress}", model.HomeAddress.ToUpper());
                    if (model.HomeTelephone != null)
                        document.ReplaceText("{HomeTelephone}", model.HomeTelephone.ToUpper());
                    if (model.MobilePhone != null)
                        document.ReplaceText("{MobilePhone}", model.MobilePhone.ToUpper());
                    if (model.EmailAddress != null)
                        document.ReplaceText("{EmailAddress}", model.EmailAddress.ToUpper());
                    if (model.EmployerName != null)
                        document.ReplaceText("{EmployerName}", model.EmployerName.ToUpper());
                    if (model.EmployerMail != null)
                        document.ReplaceText("{EmployerMail}", model.EmployerMail.ToUpper());
                    if (model.EmployerPhone != null)
                        document.ReplaceText("{EmployerPhone}", model.EmployerPhone.ToUpper());
                    if (model.EmergencyContact != null)
                        document.ReplaceText("{EmergencyContact}", model.EmergencyContact.ToUpper());
                    if (model.EmergencyPhone != null)
                        document.ReplaceText("{EmergencyPhone}", model.EmergencyPhone.ToUpper());

                    string[] visited1 = model.VisitedPlace.ToUpper().Split(',');
                    document.ReplaceText("{VisitedPlace1}", visited1[0]);
                    document.ReplaceText("{VisitedPlace2}", visited1[1]);
                    string[] visited2 = model.VisitedPurpose.ToUpper().Split(',');
                    document.ReplaceText("{VisitedPurpose1}", visited2[0]);
                    document.ReplaceText("{VisitedPurpose2}", visited2[1]);

                    string[] visited3 = model.VisitedDate.ToUpper().Split(',');
                    document.ReplaceText("{VisitedDate1}", visited3[0]);
                    document.ReplaceText("{VisitedDate2}", visited3[1]);
                    if (model.Givedetail != null)
                        document.ReplaceText("{Givedetail}", model.Givedetail.ToUpper());
                    if (model.DeclaredAbove != null)
                        document.ReplaceText("{DeclaredAbove}", model.DeclaredAbove.ToUpper());
                    List<string> lss = document.FindUniqueByPattern(@"{\w+\d?}", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                    foreach (var item in lss)
                    {
                        document.ReplaceText(item, "");
                    }
                    #endregion
                    string sfile = model.Surname.Replace(" ", "_") + "_" + model.Givname.Replace(" ", "_") + "_" + id + ".docx";
                    string filepath2 = Server.MapPath("/visafile/");
                    document.SaveAs(filepath2 + sfile);
                    Wps2Pdf pdf = new Wps2Pdf();
                    pdf.ToPdf(filepath2 + sfile, filepath2 + sfile.Replace(".docx", ".pdf"));
                    pdf.Dispose();
                }

            }
        }
        public ActionResult Download(string f)
        {
            string filepath = Server.MapPath("/outfile/" + f);
            return File(filepath, "application/octet-stream", f);
        }
    }
}