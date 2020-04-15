using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using UNO.DAL;
using UNO.MVCApp.Helpers;
using UNO.WebApi.Dto;
using UNO.WebApi.Models;
using UNO.WebApi.Services;

namespace UNO.WebApi.Controllers
{
    public class CategoryController : ApiController
    {
        private UnitOfWork _unitOfWork;
        private ICategoryService _categoryService;
        private int activeuser;

        private Utilities _ipaddressobj;
        public CategoryController(IUnitOfWork unitOfWork,
            ICategoryService categoryService, Utilities _ipaddress)
        {
            _ipaddressobj = _ipaddress;
            _unitOfWork = (UnitOfWork)unitOfWork;
            _categoryService = categoryService;
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
        }
        // GET api/category
        public IQueryable<CategoryDto> Get()
        {
            var x = _categoryService.Get(activeuser).AsQueryable();
            return x;
        }

        // GET api/category/5
        public async Task<IHttpActionResult> Get(int id)
        {
            var category = await _categoryService.GetSingle(id);

            return Ok(category);
        }

        // POST api/category
        public async Task<IHttpActionResult> Post([FromBody]Category category)
        {
            string ipadress = category.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var categoryId = await _categoryService.Create(category,ipadress,activeuser);



            return CreatedAtRoute("", new { id = categoryId }, category);
        }

        // PUT api/category/5
        public async Task<IHttpActionResult> Put(int id, [FromBody]Category category)
        {
            string ipadress = category.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            if (id != category.CATEGORY_ID)
            {
                return BadRequest(ModelState);
            }
            Category entCategory = await _categoryService.GetSingle(id);
            if (entCategory == null)
            {
                return NotFound();
            }
            _unitOfWork.BeginTransaction();
            var controllerId = await _categoryService.Edit(category, ipadress, activeuser);

            return Ok(entCategory);
        }

        // DELETE api/category/5
        [Route("api/category/{id}/{user}/{ipaddress}")]
        public async Task<IHttpActionResult> Delete(int id, string user,string ipaddress)
        {
            string ipadress = ipaddress;
            Category category = await _categoryService.GetSingle(id);
            if (category == null)
            {
                return NotFound();
            }
            await _categoryService.Delete(id, user, ipadress, activeuser);
            return Ok(category);
        }
    }
}
