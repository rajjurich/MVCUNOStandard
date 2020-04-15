using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Models;

namespace UNO.WebApi.Services
{
    public interface IControlService
    {
        List<Control> Get();

    }
    public class ControlService : IControlService
    {
         private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public ControlService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }


        public List<Control> Get()
        {

            string query = "Select ID as ControllerID,CTLR_DESCRIPTION as ControllerName from ACS_CONTROLLER where CTLR_ISDELETED =0 order by CTLR_DESCRIPTION";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<Control> entControlList = new List<Control>();

            foreach (DataRow dr in x.Rows)
            {
                Control entControl = new Control();
                entControl.ControllerID = Convert.ToString(dr["ControllerID"]);
                entControl.ControllerName = Convert.ToString(dr["ControllerName"]);

                entControlList.Add(entControl);
            }
            return entControlList;
        }
    }
}