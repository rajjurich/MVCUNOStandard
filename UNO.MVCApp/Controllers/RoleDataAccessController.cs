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
    public class RoleDataAccessController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;
        //
        // GET: /RoleDataAccess/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /RoleDataAccess/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /RoleDataAccess/Create

        public async Task<ActionResult> Create()
        {
            await BindDropDown();
            return View();
        }
        private async Task BindDropDown()
        {
            IEnumerable<SelectListItem> Users;
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}UserApi", _uri);
                var result = await client.GetAsync(uri);
                var entparams = await result.Content.ReadAsAsync<List<UserModel>>();
                Users = entparams.Select(m => new SelectListItem
                {
                    Value = m.USER_ID.ToString(),
                    Text = m.USER_CODE.ToString() + " - " + m.ROLE_Name
                });

                uri = string.Format("{0}CompanyAPI", _uri);
                result = await client.GetAsync(uri);
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

                

                uri = string.Format("{0}employee", _uri);
                result = await client.GetAsync(uri);
                var employees = await result.Content.ReadAsAsync<List<EmployeeInfo>>();
                var Employees = employees.Select(c => new SelectListItem
                {
                    Value = c.EMPLOYEE_ID.ToString(),
                    Text = string.Format("{0} - {1}", c.FULL_NAME, c.EOD_EMPID)
                });

                uri = string.Format("{0}CommonMasterAPI", _uri);
                result = await client.GetAsync(uri);
                var commonMasterContent = await result.Content.ReadAsAsync<List<CommonMasterModel>>();
                var CommonMasters = commonMasterContent.Select(c => new SelectListItem
                {
                    Value = c.COMMON_TYPES_ID.ToString(),
                    Text = c.COMMON_NAME
                });

                ViewBag.COMMON_TYPES = commonMasterContent;
                
              


                ViewBag.Users = Users;
                ViewBag.Companies = Companies;
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
        // POST: /RoleDataAccess/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleDataAccess collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}roledataaccess", _uri);

                        var result = await client.PostAsJsonAsync(uri, collection);
                        var contents = await result.Content.ReadAsStringAsync();
                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.Message = MessageConfig.htmlSuccessString;
                            ViewBag.Status = "Success";
                            ViewBag.InnerMessage = "";
                            
                        }
                        else
                        {
                            ViewBag.Message = MessageConfig.htmlErrorString;
                            ViewBag.Status = "Failed";
                            ViewBag.InnerMessage = contents;
                            
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
        // GET: /RoleDataAccess/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /RoleDataAccess/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /RoleDataAccess/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /RoleDataAccess/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
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
