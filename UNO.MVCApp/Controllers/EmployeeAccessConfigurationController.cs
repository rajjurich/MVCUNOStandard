using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UNO.AppModel;
using UNO.MVCApp.Common;
using UNO.MVCApp.Filters;
using UNO.WebApi.Helpers;

namespace UNO.MVCApp.Controllers
{
    [Authorize]
    [SessionCheck]
    public class EmployeeAccessConfigurationController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;
        //
        // GET: /EmployeeAccessConfiguration/
        [AccessCheck(IdParamName = "EmployeeAccessConfiguration/index")]
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
            await BindDropDown();
            return await GetEalConfigList("EMPLOYEE");
        }

        [HttpPost]
        public async Task<ActionResult> Index(FormCollection collection)
        {
            await BindDropDown();
            var selectedEntity = collection["ddlEntityType"];
            return await GetEalConfigList(selectedEntity);
        }

        private async Task<ActionResult> GetEalConfigList(string entityType)
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");

                uri = string.Format("{0}CommonMasterAPI/GetCommonIdByName/{1}", _uri, entityType);
                var result = await client.GetAsync(uri);
                var entityId = await result.Content.ReadAsAsync<int>();

                uri = string.Format("{0}employeeaccessconfiguration/GetByEntityId/{1}", _uri, entityId);

                result = await client.GetAsync(uri);

                var ealConfigView = await result.Content.ReadAsAsync<List<EalConfigView>>();
                ViewBag.SelectedEntity = entityType;
                return View(ealConfigView);
            }
        }
        //
        // GET: /EmployeeAccessConfiguration/Details/5

        public async Task<ActionResult> Details(int id)
        {
            EmployeeAccess employeeAccess = await GetEmployeeAccess(id);
            await BindDropDown("edit");
            return View(employeeAccess);
        }

        //
        // GET: /EmployeeAccessConfiguration/Create

        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();
        }

        private async Task BindDropDown(string action = "")
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");

                string actionMethod = "WithNoAccess";
                if (action == "edit")
                {
                    actionMethod = string.Empty;
                }

                uri = string.Format("{0}CommonAPI/GetByTypes{1}/Category", _uri, actionMethod);
                var result = await client.GetAsync(uri);
                var categoryContent = await result.Content.ReadAsAsync<List<CommonEntitiesModel>>();
                var Categories = categoryContent.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.OCE_DESCRIPTION
                });

                uri = string.Format("{0}CommonAPI/GetByTypes{1}/Location", _uri, actionMethod);
                result = await client.GetAsync(uri);
                var locationContent = await result.Content.ReadAsAsync<List<CommonEntitiesModel>>();
                var Locations = locationContent.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.OCE_DESCRIPTION
                });

                uri = string.Format("{0}CommonAPI/GetByTypes{1}/Division", _uri, actionMethod);
                result = await client.GetAsync(uri);
                var divisionContent = await result.Content.ReadAsAsync<List<CommonEntitiesModel>>();
                var Divisions = divisionContent.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.OCE_DESCRIPTION
                });

                uri = string.Format("{0}CommonAPI/GetByTypes{1}/Department", _uri, actionMethod);
                result = await client.GetAsync(uri);
                var departmentContent = await result.Content.ReadAsAsync<List<CommonEntitiesModel>>();
                var Departments = departmentContent.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.OCE_DESCRIPTION
                });

                uri = string.Format("{0}CommonAPI/GetByTypes{1}/Designation", _uri, actionMethod);
                result = await client.GetAsync(uri);
                var designationContent = await result.Content.ReadAsAsync<List<CommonEntitiesModel>>();
                var Designations = designationContent.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.OCE_DESCRIPTION
                });

                uri = string.Format("{0}CommonAPI/GetByTypes{1}/Group", _uri, actionMethod);
                result = await client.GetAsync(uri);
                var groupContent = await result.Content.ReadAsAsync<List<CommonEntitiesModel>>();
                var Groups = groupContent.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.OCE_DESCRIPTION
                });

                uri = string.Format("{0}CommonAPI/GetByTypes{1}/Grade", _uri, actionMethod);
                result = await client.GetAsync(uri);
                var gradeContent = await result.Content.ReadAsAsync<List<CommonEntitiesModel>>();
                var Grades = gradeContent.Select(c => new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.OCE_DESCRIPTION
                });




                uri = string.Format("{0}employee/{1}", _uri, actionMethod);
                result = await client.GetAsync(uri);
                var employees = await result.Content.ReadAsAsync<List<EmployeeInfo>>();
                var Employees = employees.Select(c => new SelectListItem
                {
                    Value = c.EMPLOYEE_ID.ToString(),
                    Text = string.Format("{0} {1}", c.FULL_NAME, c.EOD_EMPID)
                });











                uri = string.Format("{0}CommonMasterAPI/GetCommonWithEmployee", _uri);
                result = await client.GetAsync(uri);
                var commonMasterContent = await result.Content.ReadAsAsync<List<CommonMasterModel>>();
                var CommonMasters = commonMasterContent.Select(c => new SelectListItem
                {
                    Value = c.COMMON_TYPES_ID.ToString(),
                    Text = c.COMMON_NAME
                });

                uri = string.Format("{0}accesslevel", _uri);
                result = await client.GetAsync(uri);
                var accesslevelContent = await result.Content.ReadAsAsync<List<AccessLevelInfo>>();
                var accesslevels = accesslevelContent.Select(c => new SelectListItem
                {
                    Value = c.AL_ID.ToString(),
                    Text = c.AL_DESCRIPTION
                });

                ViewBag.AccessLevels = accesslevels;
                ViewBag.COMMON_TYPES = CommonMasters;


                ViewBag.Categories = Categories;
                ViewBag.Locations = Locations;
                ViewBag.Divisions = Divisions;
                ViewBag.Departments = Departments;
                ViewBag.Designations = Designations;
                ViewBag.Groups = Groups;
                ViewBag.Grades = Grades;



                ViewBag.Employees = Employees;
            }

        }
        //
        // POST: /EmployeeAccessConfiguration/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(EmployeeAccess collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.AddTokenToHeader("");
                        uri = string.Format("{0}employeeaccessconfiguration", _uri);

                        var result = await client.PostAsJsonAsync(uri, collection);
                        var contents = await result.Content.ReadAsStringAsync();
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
                    // TODO: Add insert logic here
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = "Model State Failed";
                    //return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;
            }
            await BindDropDown();
            return View();
        }

        //
        // GET: /EmployeeAccessConfiguration/Edit/5

        public async Task<ActionResult> Edit(int id)
        {
            EmployeeAccess employeeAccess = await GetEmployeeAccess(id);
            await BindDropDown("edit");
            return View(employeeAccess);
        }

        private async Task<EmployeeAccess> GetEmployeeAccess(int id)
        {
            EmployeeAccess employeeAccess;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}employeeaccessconfiguration/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                employeeAccess = await result.Content.ReadAsAsync<EmployeeAccess>();
            }
            return employeeAccess;
        }

        //
        // POST: /EmployeeAccessConfiguration/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, EmployeeAccess collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            EmployeeAccess employeeAccess = await GetEmployeeAccess(id);
            try
            {

                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        client.AddTokenToHeader("");
                        uri = string.Format("{0}employeeaccessconfiguration/{1}", _uri, id);

                        var result = await client.PutAsJsonAsync(uri, collection);
                        var contents = await result.Content.ReadAsStringAsync();
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
                    // TODO: Add insert logic here
                    ViewBag.Message = MessageConfig.htmlErrorString;
                    ViewBag.Status = "Failed";
                    ViewBag.InnerMessage = "Model State Failed";
                    //return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;

            }
            await BindDropDown();
            return View(employeeAccess);
        }

        //
        // GET: /EmployeeAccessConfiguration/Delete/5
        
        
        [HttpGet]
        public async Task<ActionResult> Delete()
        {
            await BindDropDown();
            return View();
        }

        //
        // POST: /EmployeeAccessConfiguration/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            Utilities _ipaddressobj = new Utilities();
            string ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
