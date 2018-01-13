using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Visa.Admin.Models;
using Visa.Domain;
using Visa.Service;

namespace Visa.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult RightError()
        {
            return View();
        }
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                if (authTicket != null)
                {
                    if (authTicket.IsPersistent) 
                    {
                        if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("List", "Apply");
                        }
                    }
                }
            }
          
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.textUser == null)
            {
                ModelState.AddModelError("", "请输入账号.");
                return View(model);
            }
            if (model.textPassword == null)
            {
                ModelState.AddModelError("", "请输入密码.");
                return View(model);
            }
            List<SysAdmin> lsstu = AdminService.CheckLogin(model.textUser.Trim(), model.textPassword);
            if (lsstu.Count > 0)
            {
                // 两种登录代码都可以，如果要涉及到角色，必须要在在Global.asax.cs文件中需要添加AuthorizeRequest事件处理代码  
                // 1.这种适用于一般的情况，如果在用这种登录代码，则在Global.asax.cs的AuthorizeRequest必须要为用户去数据库中查询角色，然后添加进去  
                //FormsAuthentication.SetAuthCookie(model.UserName, true);  
                // 2. 这种适用于带角色的登录，角色可以放在UserData里面，在AuthorizeRequest事件中可以直接拿出来使用  
                //string stuStr = ProtobufHelper.Serialize(lsstu[0]);
                string RememberMe = Request.Form["RememberMe"];
                string userData = JsonConvert.SerializeObject(lsstu[0]);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, model.textUser.Trim(), DateTime.Now, DateTime.Now.AddDays(1), true, userData, "/");
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
                cookie.HttpOnly = false;
                HttpContext.Response.Cookies.Add(cookie);

                if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("List", "Apply");
                }
            }
            else
            {
                ModelState.AddModelError("", "账号或密码不正确.");
            }
            return View(model);
        }
        [Authorize]
        public ActionResult ChangePwd()
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return RedirectToAction("Login", "Account");
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
                return RedirectToAction("Login", "Account");


            SysAdmin _adviser = JsonConvert.DeserializeObject<SysAdmin>(authTicket.UserData);
            if (_adviser != null)
                ViewData["vadviser"] = _adviser.UserName;
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult ChangePwd(ChangePasswordViewModel model)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
                return RedirectToAction("Login", "Account");
            FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null)
                return RedirectToAction("Login", "Account");
            string adviserId = authTicket.UserData;
            SysAdmin _adviser = JsonConvert.DeserializeObject<SysAdmin>(authTicket.UserData);
            if (_adviser != null)
                ViewData["vadviser"] = _adviser.UserName;

            List<SysAdmin> lsstu = AdminService.CheckLogin(_adviser.UserName, model.CurrentPassword);
            if (lsstu.Count == 0)
            {
                ModelState.AddModelError("", "旧密码不正确！");
                return View(model);
            }
            if (AdminService.ChangePwd(_adviser.UserName, model.NewPassword))
                ModelState.AddModelError("", "修改成功！");
            else
                ModelState.AddModelError("", "修改失败！");
            return View(model);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        public ActionResult PermissionError()
        {
            FormsAuthentication.SignOut();
            return View();
        }
    }
}