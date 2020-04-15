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
    public class CategoryController : Controller
    {
        string _uri = ConfigurationManager.AppSettings["APIUrl"].ToString();
        string uri = string.Empty;
        //


        //
        // GET: /EntCategory/
        [AccessCheck(IdParamName = "Category/index")]
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

            return await GetCategoryList();
        }

        private async Task<ActionResult> GetCategoryList()
        {
            using (HttpClient client = new HttpClient())
            {
                client.AddTokenToHeader("");
                uri = string.Format("{0}category", _uri);

                var result = await client.GetAsync(uri);

                var category = await result.Content.ReadAsAsync<List<CategoryInfo>>();
                return View(category);
            }
        }
        //
        // GET: /EntCategory/Details/5

        public async Task<ActionResult> Details(int id)
        {
            Category category = await GetCategory(id);
            await BindDropDown();
            return View(category);
        }

        //
        // GET: /EntCategory/Create

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
                ViewBag.Companies = Companies;
                ViewBag.Categories = Categories;
            }

        }
        //
        // POST: /EntCategory/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Category collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                if (ModelState.IsValid)
                {
                    using (HttpClient client = new HttpClient())
                    {
                        uri = string.Format("{0}category", _uri);

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
        // GET: /EntCategory/Edit/5

        public async Task<ActionResult> Edit(int id)
        {
            Category category = await GetCategory(id);
            await BindDropDown();
            return View(category);
        }
        private async Task<Category> GetCategory(int id)
        {
            Category category;
            using (HttpClient client = new HttpClient())
            {
                uri = string.Format("{0}category/{1}", _uri, id);

                var result = await client.GetAsync(uri);

                category = await result.Content.ReadAsAsync<Category>();
            }
            return category;
        }

        //
        // POST: /EntCategory/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Category collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            Category category = await GetCategory(id);
            try
            {
                if (id == collection.CATEGORY_ID)
                {
                    // TODO: Add update logic here
                    if (ModelState.IsValid)
                    {
                        using (HttpClient client = new HttpClient())
                        {
                            uri = string.Format("{0}category/{1}", _uri, id);
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
                                await BindDropDown();
                                return View(category);
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

                    //return RedirectToAction("Index");
                }
                else
                {
                    throw new Exception("Invalid Model");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = MessageConfig.htmlErrorString;
                ViewBag.Status = "Failed";
                ViewBag.InnerMessage = ex.Message;

            }
            await BindDropDown();
            return View(category);
        }

        //
        // GET: /EntCategory/Delete/5

        public async Task<ActionResult> Delete(int id)
        {
            Category category = await GetCategory(id);
            await BindDropDown();
            return View(category);
        }

        //
        // POST: /EntCategory/Delete/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, Category collection)
        {
            Utilities _ipaddressobj = new Utilities();
            collection.ipaddress = _ipaddressobj.GetIpAddress();
            try
            {
                // TODO: Add delete logic here
                if (id == collection.CATEGORY_ID)
                {
                    using (HttpClient client = new HttpClient())
                    {

                        uri = string.Format("{0}category/{1}/{2}/{3}", _uri, id, Session["User"].ToString(), collection.ipaddress);

                        var result = await client.DeleteAsync(uri);
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
                            TempData["Message"] = MessageConfig.htmlErrorString;
                            TempData["Status"] = "Failed";
                            TempData["InnerMessage"] = contents;
                            return RedirectToAction("Index");
                        }
                    }


                }
                else
                {
                    TempData["Message"] = MessageConfig.htmlErrorString;
                    TempData["Status"] = "Failed";
                    TempData["InnerMessage"] = "Invalid";
                    return RedirectToAction("Index");

                }
                //return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Message"] = MessageConfig.htmlErrorString;
                TempData["InnerMessage"] = ex.Message;
                TempData["Status"] = "Failed";
                return RedirectToAction("Index");
            }
        }
    }
}
