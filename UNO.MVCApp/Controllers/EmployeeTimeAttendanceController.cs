using System;
using System.Collections.Generic;
using System.Linq;
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
    public class EmployeeTimeAttendanceController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;
        //
        // GET: /EmployeeShift/
        [AccessCheck(IdParamName = "EmployeeTimeAttendance/index")]
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
            

            return await GetEmployeeShift();
        }

        private async Task<EmployeeTimeAttendanceCreateModel> GetEmployeeShift(int id)
        {
            EmployeeTimeAttendanceCreateModel employeeShifts;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}EmployeeTimeAttendanceAPI/{1}", _uri, id);
                var result = await client.GetAsync(uri);
                employeeShifts = await result.Content.ReadAsAsync<EmployeeTimeAttendanceCreateModel>();
            }
            return employeeShifts;
        }

        private async Task<ActionResult> GetEmployeeShift()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}EmployeeTimeAttendanceAPI", _uri);

                var result = await client.GetAsync(uri);

                var employeeShifts = await result.Content.ReadAsAsync<List<EmployeeTimeAttendanceCreateModel>>();
                return View(employeeShifts);
            }
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

                uri = string.Format("{0}employeeDtoattandance", _uri);
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


                uri = string.Format("{0}WeeklyOffAPI/getweekoffattandance", _uri);
                result = await client.GetAsync(uri);
                var weekoffattandance = await result.Content.ReadAsAsync<List<WeeklyOff>>();
                var weekoffattandance2 = weekoffattandance.Select(m => new SelectListItem
                {
                    Value = Convert.ToString(m.MWK_CD),
                    Text = m.MWK_DESC
                });


                uri = string.Format("{0}WeeklyOffAPI/getweekendattandance", _uri);
                result = await client.GetAsync(uri);
                var weekendattandance = await result.Content.ReadAsAsync<List<WeeklyOff>>();
                var weekendattandance2 = weekendattandance.Select(m => new SelectListItem
                {
                    Value = Convert.ToString(m.MWK_CD),
                    Text = m.MWK_DESC
                });


                uri = string.Format("{0}ShiftPattern/getpatterns", _uri);
                result = await client.GetAsync(uri);
                var shiftpattern = await result.Content.ReadAsAsync<List<ShiftPattern>>();
                var shiftpattern2 = shiftpattern.Select(m => new SelectListItem
                {
                    Value = Convert.ToString(m.SHIFT_PATTERN_ID),
                    Text = m.SHIFT_PATTERN_DESCRIPTION
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

                //added by DEVANG
                ViewBag.weekoffattandance2 = weekoffattandance2;
                ViewBag.weekendattandance2 = weekendattandance2;
                ViewBag.shiftpattern2 = shiftpattern2;
                //added by DEVANG
            }

        }

        [HttpGet]
        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(EmployeeTimeAttendanceCreateModel employeeattendance)
        {
            Utilities _ipaddressobj = new Utilities();
            employeeattendance.ipaddress = _ipaddressobj.GetIpAddress();

            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}EmployeeTimeAttendanceAPI", _uri);

                        var result = await client.PostAsJsonAsync(uri, employeeattendance);

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
                else
                {
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = "Model State Failed";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            EmployeeTimeAttendanceCreateModel employeeattend = await GetEmployeeShift(id);
            await BindDropDown();
            return View(employeeattend);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(int id, EmployeeTimeAttendanceCreateModel employeeattendance)
        {
            Utilities _ipaddressobj = new Utilities();
            employeeattendance.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}EmployeeTimeAttendanceAPI", _uri);
                        var result = await client.PutAsJsonAsync(uri, employeeattendance);
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
                else
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = "";
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<ActionResult> DeleteView(int id)
        {
            EmployeeTimeAttendanceCreateModel employeeattendance = await GetEmployeeShift(id);
            await BindDropDown();
            return View(employeeattendance);
        }


        [HttpPost]
        public async Task<ActionResult> DeleteView(EmployeeTimeAttendanceCreateModel employeeattendance)
        {
            Utilities _ipaddressobj = new Utilities();
            employeeattendance.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    uri = string.Format("{0}EmployeeTimeAttendanceAPI", _uri);
                    var result = await client.DeleteAsync(uri + "/" + employeeattendance.ETC_EMP_ID + "/" + employeeattendance.ipaddress);
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
            catch (Exception)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = "";
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public async Task<ActionResult> View(int id)
        {
            EmployeeTimeAttendanceCreateModel employeeattandancee = await GetEmployeeShift(id);
            await BindDropDown();
            return View(employeeattandancee);
        }
        

    }
}
