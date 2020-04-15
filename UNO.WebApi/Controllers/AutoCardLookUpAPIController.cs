using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using UNO.DAL;
using UNO.WebApi.Models;
using UNO.WebApi.Services;

namespace UNO.WebApi.Controllers
{
    public class AutoCardLookUpAPIController : ApiController
    {


        //private UnitOfWork _unitOfWork;
        //private DatabaseHelper _DatabaseHelper;

        private IAutoCardLookUpService _entAutoCardLookUpService;

        public AutoCardLookUpAPIController(IAutoCardLookUpService entAutoCardLookUpService)
        {
            _entAutoCardLookUpService = entAutoCardLookUpService;
        }
        // GET api/entparams


        
        //[Route("api/LocateCardHolderAPI/get/{FILTER_CRITERIA}/{FILTER}")]
        public List<AutoCardLookUp> get()
        {
            return _entAutoCardLookUpService.GetReaders();
        }
        public AutoCardLookUp getRocords(string CTLR_ID, string READER_ID)
        {
            return _entAutoCardLookUpService.GetRecords(CTLR_ID, READER_ID);
        }
    }
}
