using System;
using System.Collections.Generic;
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
    public class BioMetricTemplateConfigurationAPIController : ApiController
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        private int activeuser;
        private Utilities _ipaddressobj;

        private IBioMetricTemplateConfigurationService _entbiometricservice;

        public BioMetricTemplateConfigurationAPIController(IBioMetricTemplateConfigurationService entbiometricservice,
            IUnitOfWork unitOfWork, DatabaseHelper databaseHelper, Utilities _ipaddress)
        {
            var request = HttpContext.Current.Request;
            activeuser = Convert.ToInt32(request.Headers["activeuser"]);
            _entbiometricservice = entbiometricservice;
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
            _ipaddressobj = _ipaddress;
        }


        [HttpGet]

        public List<BioMetricTemplateConfigurationapimodel> get()
        {
            return _entbiometricservice.GetFingureList();
        }

        public BioMetricTemplateConfigurationapimodel get(int id)
        {
            return _entbiometricservice.GetFingureList(id);
        }

        public async Task<IHttpActionResult> Post(BioMetricTemplateConfigurationapimodel BioMetricTemplateConfiguration1)
        {
            string ipadress = BioMetricTemplateConfiguration1.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }
            _unitOfWork.BeginTransaction();

            BioMetricTemplateConfigurationapimodel biometricmodel = new BioMetricTemplateConfigurationapimodel();
            biometricmodel.COMPANY_ID = BioMetricTemplateConfiguration1.COMPANY_ID;
            biometricmodel.FingureForAttandance = BioMetricTemplateConfiguration1.FingureForAttandance;
            biometricmodel.FingureForEnroll = BioMetricTemplateConfiguration1.FingureForEnroll;
            biometricmodel.Mycount = BioMetricTemplateConfiguration1.Mycount;
            biometricmodel.Time_Attandance = BioMetricTemplateConfiguration1.Time_Attandance;
            biometricmodel.Enroll = BioMetricTemplateConfiguration1.Enroll;

            await _entbiometricservice.Create(biometricmodel, ipadress, activeuser);



            return Ok(BioMetricTemplateConfiguration1);



        }

        public async Task<IHttpActionResult> Put(BioMetricTemplateConfigurationapimodel BioMetricTemplateConfiguration1)
        {
            string ipadress = BioMetricTemplateConfiguration1.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var biometricc = await _entbiometricservice.Edit(BioMetricTemplateConfiguration1, ipadress, activeuser);

            return CreatedAtRoute("", new { id = biometricc }, BioMetricTemplateConfiguration1);
        }


        public async Task<IHttpActionResult> DELETE(int id, BioMetricTemplateConfigurationapimodel BioMetricTemplateConfiguration1)
        {
            string ipadress = BioMetricTemplateConfiguration1.ipaddress;
            if (!(ModelState.IsValid))
            {
                return BadRequest(ModelState);
            }

            _unitOfWork.BeginTransaction();

            var biometrics = await _entbiometricservice.Delete(id, ipadress, activeuser);

            return CreatedAtRoute("", new { id = biometrics }, id);
        }

    }
}
