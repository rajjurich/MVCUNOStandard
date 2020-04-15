using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using UNO.AppModel;
using UNO.MVCApp.Common;
using UNO.MVCApp.Filters;












namespace UNO.MVCApp.Controllers
{

    public class HomeController : Controller
    {
        string User_code_public = "";
        string url = WebConfigurationManager.AppSettings["APIUrl"];
       
        public ActionResult Index()
        {

            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string versions = version.Substring(0, 3);
            return View();
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Login(UserLogin login)
        {
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            string versiontrimed = version.Substring(0, 4);
            try
            {
                UserModel _user = new UserModel();
                User_code_public = login.USER_CODE;
               login.Password = CryptoHelper.EncryptTripleDES(login.Password);
               login.verionsofwebapp = versiontrimed;
               HttpContext.Session["Version_display"] ="Version "+ login.verionsofwebapp;
               //login.Password = CryptoHelper.DecryptTripleDES("dd0sYz92BCI=");
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        var uri = url + "Userapi/ValidateLogin";
                        var result = await client.PostAsJsonAsync(uri, login);
                        var contents = result.Content.ReadAsStringAsync().Result;
                        if (result.IsSuccessStatusCode)
                        {
                            //Storing the response details recieved from web api  
                            //Deserializing the response recieved from web api and storing into the Employee list  
                            _user = JsonConvert.DeserializeObject<UserModel>(contents);
                        }
                        else
                        {   // need to handled exception 
                            ViewBag.Message = MessageConfig.htmlErrorString;
                            ViewBag.InnerMessage = contents;
                            ViewBag.Status = "Failed";
                            return View("Index");
                        }
                    }

                    if (_user.USER_ID != 0)
                    {
                        FormsAuthentication.SetAuthCookie(_user.USER_CODE.ToString(), false);
                        HttpContext.Session["ACTIVE_USER"] = _user.USER_ID;
                        HttpContext.Session["Ess"] = _user.EssEnabled;
                        HttpContext.Session["User"] = _user.USER_CODE;
                        HttpContext.Session["Role"] = _user.ROLE_ID;                        

                        if (_user.PASSWORD_RESET)
                            return RedirectToAction("ChangePassword", "Home");

                        if (_user.EssEnabled)
                        {
                            return Redirect(Url.Action("index", "EssDashboard"));
                            /// return Redirect(Url.Action("index", "ManagerDashboar"));  provision for manager dashboard
                        }
                        else
                            return Redirect(Url.Action("index", "Dashboard"));
                    }
                    else
                    {
                        ViewBag.Message = MessageConfig.htmlErrorString;
                        ViewBag.InnerMessage = "The user name or password provided is incorrect";
                        ViewBag.Status = "Failed";
                        //return View("Index");


                        ModelState.AddModelError("CustomError", "The user name or password provided is incorrect");
                        //return RedirectToAction("Index", "Home");
                        return View("Index");
                    }
                }

                return View("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View("Index");
            }
        }

        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }

        
        [Authorize]
        [SessionCheck]
        [HttpPost]
        
        public async Task<ActionResult> ChangePassword(UserModel User1)
        {
            //ViewBag.userid = Convert.ToInt64(HttpContext.Session["ACTIVE_USER"]);
            //ViewBag.usercode = Convert.ToString(HttpContext.Session["User"]);
            User1.Password=CryptoHelper.EncryptTripleDES(User1.Password);
            User1.isReset = 0;
            if (ModelState.IsValid)
            {
                using (HttpClient client = new HttpClient())
                {
                    
                    var uri = url + "UserApi/ChangePassword";
                    var result = await client.PostAsJsonAsync(uri, User1);
                    if (result.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api  
                        var contents = result.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the Employee list  
                     //   _user = JsonConvert.DeserializeObject<UserModel>(contents);

                        if (Convert.ToBoolean(HttpContext.Session["Ess"]))
                            return Redirect(Url.Action("Index", "EssDashboard"));
                        else
                            return Redirect(Url.Action("index", "Dashboard"));
                    }
                    else
                    {   // need to handled exception 
                        ViewBag.Message = MessageConfig.htmlErrorString;
                        ViewBag.InnerMessage = result.Content;
                        ViewBag.Status = "Failed";
                        return View("Index");
                    }
                }

            }
            else
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.InnerMessage = "The user name or password provided is incorrect.";
                ViewBag.Status = "Failed";
                //return View("Index");


                ModelState.AddModelError("CustomError", "The user name or password provided is incorrect.");

                return View("ChangePassword");
            }


        }

        //[Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            //Session.Abandon();
            HttpContext.Session["ACTIVE_USER"] = null;
            HttpContext.Session["Ess"] = null;
            HttpContext.Session["User"] = null;
            HttpContext.Session["Role"] = null;
            return RedirectToAction("Index", "Home");
        }

        
        public ActionResult GetDateTimeNow()
        {
            var datetimesend = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss tt");
            return Json(datetimesend);
        }

      

    }
}

