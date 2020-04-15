using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class EmployeeCardConfigController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;

        // GET: /Menu/
        [AccessCheck(IdParamName = "EmployeeCardConfig/index")]
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
            
            //using (WebClient client = new WebClient())
            //{
            //    string s = client.DownloadString(_uri + "EmployeeCardConfigAPI/get");
            //    EmployeeCardConfigList = JsonConvert.DeserializeObject<List<EmployeeCardConfig>>(s);
            //    EmployeeCardConfigList.RemoveAll(item => item == null);
            //}

            return await GetEmployeeCardList();
        }

        private async Task<ActionResult> GetEmployeeCardList()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}EmployeeCardConfigAPI/get", _uri);

                var result = await client.GetAsync(uri);

                var employeeCardConfig = await result.Content.ReadAsAsync<List<EmployeeCardConfig>>();
                return View(employeeCardConfig);
            }
        }




        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();
        }
        

        private async Task BindDropDown()
        {
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

                uri = string.Format("{0}CommonAPI/GetByTypes/Category", _uri);
                result = await client.GetAsync(uri);
                var categoryContent = await result.Content.ReadAsAsync<List<CommonEntitiesModel>>();
                var Categories = categoryContent.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.OCE_DESCRIPTION
                });

                uri = string.Format("{0}CommonAPI/GetByTypes/Location", _uri);
                result = await client.GetAsync(uri);
                var locationContent = await result.Content.ReadAsAsync<List<CommonEntitiesModel>>();
                var Locations = locationContent.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.OCE_DESCRIPTION
                });

                uri = string.Format("{0}CommonAPI/GetByTypes/Division", _uri);
                result = await client.GetAsync(uri);
                var divisionContent = await result.Content.ReadAsAsync<List<CommonEntitiesModel>>();
                var Divisions = divisionContent.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.OCE_DESCRIPTION
                });

                uri = string.Format("{0}CommonAPI/GetByTypes/Department", _uri);
                result = await client.GetAsync(uri);
                var departmentContent = await result.Content.ReadAsAsync<List<CommonEntitiesModel>>();
                var Departments = departmentContent.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.OCE_DESCRIPTION
                });

                uri = string.Format("{0}CommonAPI/GetByTypes/Designation", _uri);
                result = await client.GetAsync(uri);
                var designationContent = await result.Content.ReadAsAsync<List<CommonEntitiesModel>>();
                var Designations = designationContent.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.OCE_DESCRIPTION
                });

                uri = string.Format("{0}CommonAPI/GetByTypes/Group", _uri);
                result = await client.GetAsync(uri);
                var groupContent = await result.Content.ReadAsAsync<List<CommonEntitiesModel>>();
                var Groups = groupContent.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.OCE_DESCRIPTION
                });

                uri = string.Format("{0}CommonAPI/GetByTypes/Grade", _uri);
                result = await client.GetAsync(uri);
                var gradeContent = await result.Content.ReadAsAsync<List<CommonEntitiesModel>>();
                var Grades = gradeContent.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.OCE_DESCRIPTION
                });

                uri = string.Format("{0}entparams/EMPSTATUS", _uri);
                result = await client.GetAsync(uri);
                var employeeTypes = await result.Content.ReadAsAsync<List<dynamic>>();
                var EmployeeTypes = employeeTypes.Select(m => new SelectListItem
                {
                    Value = m.CODE,
                    Text = m.VALUE
                });

                uri = string.Format("{0}ReasonMasterAPI/get/EL", _uri);
                result = await client.GetAsync(uri);
                var leftReasons = await result.Content.ReadAsAsync<List<Reason>>();
                var LeftReasons = leftReasons.Select(c => new SelectListItem
                {
                    Value = c.REASON_ID.ToString(),
                    Text = c.REASON_DESC
                });

                uri = string.Format("{0}employeeDtoCardConfig", _uri);
                result = await client.GetAsync(uri);
                var employees = await result.Content.ReadAsAsync<List<EmployeeInfo>>();
                var Employees = employees.Select(c => new SelectListItem
                {
                    Value = c.EMPLOYEE_ID.ToString(),
                    Text = string.Format("{0} - {1}", c.FULL_NAME, c.EOD_EMPID)
                });

                uri = string.Format("{0}entparams/MARITALSTATUS", _uri);
                result = await client.GetAsync(uri);
                var maritalStatus = await result.Content.ReadAsAsync<List<dynamic>>();
                var MaritalStatus = maritalStatus.Select(m => new SelectListItem
                {
                    Value = m.PARAM_ID,
                    Text = m.VALUE
                });

                uri = string.Format("{0}entparams/RELIGION", _uri);
                result = await client.GetAsync(uri);
                var religion = await result.Content.ReadAsAsync<List<dynamic>>();
                var Religion = religion.Select(m => new SelectListItem
                {
                    Value = m.PARAM_ID,
                    Text = m.VALUE
                });

                uri = string.Format("{0}entparams/states", _uri);
                result = await client.GetAsync(uri);
                var states = await result.Content.ReadAsAsync<List<dynamic>>();
                var States = states.Select(m => new SelectListItem
                {
                    Value = m.PARAM_ID,
                    Text = m.VALUE
                });


                uri = string.Format("{0}entparams/gender", _uri);
                result = await client.GetAsync(uri);
                var gender = await result.Content.ReadAsAsync<List<dynamic>>();
                var Gender = gender.Select(m => new SelectListItem
                {
                    Value = m.PARAM_ID,
                    Text = m.VALUE
                });

                uri = string.Format("{0}entparams/bloodgroup", _uri);
                result = await client.GetAsync(uri);
                var bloodgroup = await result.Content.ReadAsAsync<List<dynamic>>();
                var Bloodgroup = bloodgroup.Select(m => new SelectListItem
                {
                    Value = m.PARAM_ID,
                    Text = m.VALUE
                });

                ViewBag.Companies = Companies;
                ViewBag.Categories = Categories;
                ViewBag.Locations = Locations;
                ViewBag.Divisions = Divisions;
                ViewBag.Departments = Departments;
                ViewBag.Designations = Designations;
                ViewBag.Groups = Groups;
                ViewBag.Grades = Grades;
                ViewBag.EmployeeTypes = EmployeeTypes;
                ViewBag.LeftReasons = LeftReasons;

                ViewBag.Employees = Employees;
                ViewBag.MaritalStatus = MaritalStatus;
                ViewBag.Religion = Religion;
                ViewBag.States = States;

                ViewBag.Gender = Gender;
                ViewBag.Bloodgroup = Bloodgroup;
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeCardConfigCreateModel collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}EmployeeCardConfigAPI", _uri);
                        var result = await client.PostAsJsonAsync(uri, collection);
                        var contents = result.Content.ReadAsStringAsync();

                        if (result.IsSuccessStatusCode)
                        {
                            TempData["Message"] = MessageConfig.htmlSuccessString;
                            TempData["Status"] = "Success";
                            TempData["InnerMessage"] = "";
                            //BindDropDown();
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ViewBag.Message = MessageConfig.htmlErrorString;
                            ViewBag.Status = "Failed";
                            ViewBag.InnerMessage = contents;
                            await BindDropDown();
                            return View();
                        }
                    }
                }
                else
                {
                    var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .FirstOrDefault();
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = errors[0].ErrorMessage;
                    //return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = e.Message;
            }
            await BindDropDown();
            return View();
        }

        [HttpGet]    
        public async Task<ActionResult> Edit(int id)
        {
            EmployeeCardConfig cardconfig = await GetEmployeeList(id);
            await BindDropDown();
            return View(cardconfig);

        }
        [HttpGet]
        public async Task<ActionResult> View(int id)
        {
            EmployeeCardConfig cardconfig = await GetEmployeeList(id);
            await BindDropDown();
            return View(cardconfig);
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            EmployeeCardConfig cardconfig = await GetEmployeeList(id);
            await BindDropDown();
            return View(cardconfig);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(EmployeeCardConfig collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {

                using (HttpClient client = new HttpClient())
                {
                    uri = string.Format("{0}EmployeeCardConfigAPI", _uri);
                    var result = await client.DeleteAsync(uri + "/" + collection.CC_EMP_ID + "/" + collection.ipaddress);
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
                        return RedirectToAction("Index");
                    }
                }


            }
            catch (Exception e)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = "";
                return RedirectToAction("Index");
            }
        }


        [HttpPost]
        public async Task<ActionResult> Edit(EmployeeCardConfig collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                
                    using(HttpClient client=new HttpClient())
                    {
                        uri = string.Format("{0}EmployeeCardConfigAPI", _uri);
                        var result = await client.PutAsJsonAsync(uri, collection);
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
                            return RedirectToAction("Index");
                        }
                    }
                
                
            }
            catch(Exception e)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = "";
                return RedirectToAction("Index");
            }
        }


        private async Task<EmployeeCardConfig> GetEmployeeList(int id)
        {
            EmployeeCardConfig employee;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}EmployeeCardConfigAPI/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                employee = await result.Content.ReadAsAsync<EmployeeCardConfig>();
            }
            return employee;
        }


    }
}


