using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using UNO.AppModel;
using UNO.MVCApp.Common;
using UNO.MVCApp.Filters;
using UNO.WebApi.Helpers;

namespace UNO.MVCApp.Controllers
{
    [Authorize]
    [SessionCheck]
    public class UserController : Controller
    {
        string _uri = WebConfigurationManager.AppSettings["APIUrl"];
        string uri = string.Empty;
        //
        // GET: /User/
        [AccessCheck(IdParamName = "User/index")]
        public async Task<ActionResult> Index()
        {
            var data = ViewData.Model as RoleMenuAccess;
            if (data.CreateAccess == 0)
                ViewBag.CreateAccess = "False";
            if (data.ViewAccess == 0)
                ViewBag.ViewAccess = "False";
            if (data.UpdateAccess == 0)
                ViewBag.EditAccess = "False";
            if (data.DeleteAccess == 0)
                ViewBag.DeleteAccess = "False";
            ViewBag.Message = TempData["Message"];
            ViewBag.Status = TempData["Status"];
            ViewBag.InnerMessage = TempData["InnerMessage"];
            List<UserModel> UserList = new List<UserModel>();
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}UserApi", _uri);
                var result = await client.GetAsync(uri);
                UserList = await result.Content.ReadAsAsync<List<UserModel>>();

            }

            //using (WebClient client = new WebClient())
            //{

            //    string s = client.DownloadString(_uri + "UserApi");
            //    UserList = JsonConvert.DeserializeObject<List<UserModel>>(s);
            //    UserList.RemoveAll(item => item == null);
            //}
            return View(UserList);
        }
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();
        }

        private async Task BindDropDown()
        {
            List<RoleModule> rolemodule = new List<RoleModule>();
            List<CompanyModel> companymodel = new List<CompanyModel>();

            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}CompanyAPI", _uri);
                var result = await client.GetAsync(uri);
                var companyContent = await result.Content.ReadAsAsync<List<CompanyModel>>();
                var Companies = companyContent.Select(c => new SelectListItem
                {
                    Value = c.COMPANY_ID.ToString(),
                    Text = c.COMPANY_NAME
                });

                uri = string.Format("{0}RoleAPI", _uri);
                result = await client.GetAsync(uri);
                var roleContent = await result.Content.ReadAsAsync<List<RoleModule>>();
                var Roles = roleContent.Select(c => new SelectListItem
                {
                    Value = c.ROLE_ID.ToString(),
                    Text = c.ROLE_Name
                });

                ViewBag.CompanyId = Companies;
                ViewBag.RoleId = Roles;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(UserModel userModule)
        {
            Utilities _ipaddressobj = new Utilities();
            userModule.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.AddTokenToHeader("");
                        userModule.ACTIVE_USER = Convert.ToString(Session["ACTIVE_USER"]);
                        userModule.Password = CryptoHelper.EncryptTripleDES(userModule.Password);
                        var result = await client.PostAsJsonAsync(_uri + "UserApi", userModule);
                        var contents = await result.Content.ReadAsStringAsync();
                        if (result.IsSuccessStatusCode)
                        {
                            TempData["Message"] = MessageConfig.htmlSuccessString;
                            TempData["Status"] = "Success";
                            TempData["InnerMessage"] = "";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Message = MessageConfig.htmlErrorString;
                            ViewBag.Status = "Failed";
                            ViewBag.InnerMessage = contents;
                            FillData(userModule.USER_ID);
                            return View();
                        }
                    }
                }
                else
                {
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = "Model State Failed";
                    FillData(userModule.USER_ID);
                    return View();
                }
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;
                FillData(userModule.USER_ID);
                return View();
            }
        }
        [HttpGet]
        [ActionName("View")]
        public async Task<ActionResult> ReadView(int id)
        {
            //MenuModel mm = new MenuModel();
            //if (mm.ViewFlag =="")
            //{

            //}
            //return FillData(id);
            UserModel user = await GetUser(id);
            await BindDropDown();
            return View(user);
        }
        public async Task<ActionResult> Edit(int id)
        {
            UserModel user = await GetUser(id);
            await BindDropDown();
            return View(user);
            //return FillData(id);
        }
        private async Task<UserModel> GetUser(int id)
        {
            UserModel user;
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}UserApi/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                user = await result.Content.ReadAsAsync<UserModel>();
            }
            return user;
        }

        //
        // POST: /MeterType/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(UserModel usermodel)
        {
            Utilities _ipaddressobj = new Utilities();
            usermodel.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {

                        var result = await client.PutAsJsonAsync(_uri + "userapi", usermodel);
                        var contents = await result.Content.ReadAsStringAsync();
                        if (result.IsSuccessStatusCode)
                        {
                            TempData["Message"] = MessageConfig.htmlSuccessString;
                            TempData["Status"] = "Success";
                            TempData["InnerMessage"] = "";
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Message = MessageConfig.htmlErrorString;
                            ViewBag.Status = "Failed";
                            ViewBag.InnerMessage = contents;
                            FillData(usermodel.USER_ID);
                            return View();
                        }

                    }
                }
                else
                {
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = "Model State Failed";
                    FillData(usermodel.USER_ID);
                    return View();
                }
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;
                FillData(usermodel.USER_ID);
                return View();
            }
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteView(int id)
        {
            //MenuModel mm = new MenuModel();
            //if (mm.ViewFlag =="")
            //{

            //}
            //return FillData(id);
            UserModel user = await GetUser(id);
            await BindDropDown();
            return View(user);
        }

        //
        // POST: /MeterType/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            Utilities _ipaddressobj = new Utilities();
            string ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.AddTokenToHeader("");
                    var result = await client.DeleteAsync(_uri + "userapi/" + id + "/" + ipaddress);
                    var contents = await result.Content.ReadAsStringAsync();
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["Message"] = MessageConfig.htmlSuccessString;
                        TempData["Status"] = "Success";
                        TempData["InnerMessage"] = "";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = MessageConfig.htmlErrorString;
                        ViewBag.Status = "Failed";
                        ViewBag.InnerMessage = contents;
                        FillData(id);
                        return View();
                    }

                }
                // return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;
                FillData(id);
                return View();
            }
        }
        private ActionResult FillData(long id)
        {
            UserModel usermodel = new UserModel();
            List<RoleModule> rolemodule = new List<RoleModule>();
            List<CompanyModel> companymodel = new List<CompanyModel>();

            using (WebClient client = new WebClient())
            {
                string mCompany = client.DownloadString(_uri + "CompanyAPI");
                companymodel = JsonConvert.DeserializeObject<List<CompanyModel>>(mCompany);
                string mRole = client.DownloadString(_uri + "RoleAPI");
                rolemodule = JsonConvert.DeserializeObject<List<RoleModule>>(mRole);
                string user = client.DownloadString(_uri + "UserApi/" + id);
                usermodel = JsonConvert.DeserializeObject<UserModel>(user);

            }
            ViewBag.CompanyId = new SelectList(companymodel, "COMPANY_ID", "COMPANY_NAME");
            ViewBag.RoleId = new SelectList(rolemodule, "ROLE_ID", "ROLE_NAME");

            return View(usermodel);
        }

    }
}
