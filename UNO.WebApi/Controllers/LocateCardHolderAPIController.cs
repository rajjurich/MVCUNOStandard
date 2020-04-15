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
    public class LocateCardHolderAPIController : ApiController
    {


        //private UnitOfWork _unitOfWork;
        //private DatabaseHelper _DatabaseHelper;

        private ILocateCardHolderService _entLocateCardHolderService;

        public LocateCardHolderAPIController(ILocateCardHolderService entLocateCardHolderService)
        {
            _entLocateCardHolderService = entLocateCardHolderService;
        }
        // GET api/entparams


        
        [Route("api/LocateCardHolderAPI/get/{FILTER_CRITERIA}/{FILTER}")]
        public LocateCardHolder get(int FILTER_CRITERIA, string FILTER)
        {
            return _entLocateCardHolderService.GetCardHolder(FILTER_CRITERIA, FILTER);
        }
    }
}
