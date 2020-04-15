using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using UNO.DAL;
using UNO.WebApi.Models;
namespace UNO.WebApi.Services
{
    public interface IVMSApproveRequestService
    {
        Task<int> ApproveReject(VisitorAppointmentRequest entity, string ipaddress, int activeuser);
        List<VisitorAppointmentRequest> GetVMSApproveRequest();
        VisitorAppointmentRequest GetVMSApproveRequest(int id);
    }
    public class VMSApproveRequestService : IVMSApproveRequestService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;
        public VMSApproveRequestService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }

        public async Task<int> ApproveReject(VisitorAppointmentRequest entity, string ipaddress, int activeuser)
        {
            string query = "Update Visitor_Appointment_Request set approval_status = '" + entity.APPROVAL_STATUS + "', Aprroval_datetime = '" + DateTime.Now + "' , Approver_Remarks = '" + entity.APPROVER_REMARKS + "'    where requestid =  " + entity.REQUEST_ID;
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }

        public List<VisitorAppointmentRequest> GetVMSApproveRequest()
        {
            string query = "select RequestID,Visitor_Name,VisitorCompany,nature_of_work,case when approval_status ='A' then 'Approved' when approval_status ='R' then 'Rejected' when approval_status ='N' then 'Pending' end as approval_status,convert(varchar(20),createdon,103) as RequestDate from Visitor_Appointment_Request where  isDeletedDate =0 and approval_status ='N'";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<VisitorAppointmentRequest> entParamsList = new List<VisitorAppointmentRequest>();

            foreach (DataRow dr in x.Rows)
            {
                VisitorAppointmentRequest entVMSApproveRequest = new VisitorAppointmentRequest();
                entVMSApproveRequest.REQUEST_ID = Convert.ToInt32(dr["RequestID"]);
                entVMSApproveRequest.VISITOR_NAME = Convert.ToString(dr["Visitor_Name"]);
                entVMSApproveRequest.VISITORCOMPANY = Convert.ToString(dr["VisitorCompany"]);
                entVMSApproveRequest.NATURE_OF_WORK = Convert.ToString(dr["nature_of_work"]);
                entVMSApproveRequest.APPROVAL_STATUS = Convert.ToString(dr["approval_status"]);
                entVMSApproveRequest.CREATEDON = Convert.ToDateTime(dr["RequestDate"]);

                entParamsList.Add(entVMSApproveRequest);
            }
            return entParamsList;
        }
        public VisitorAppointmentRequest GetVMSApproveRequest(int id)
        {
            string query = "select RequestID,Visitor_Name,VisitorCompany,mobileNo,nature_of_work,Designation,appointment_from_date,appointment_to_date,Visitor_Allowed_From_time,visitor_Allowed_To_Time,visitor_nationality,PurposeOfVisit,additional_Info from Visitor_Appointment_Request where  isDeletedDate =0 and RequestID =" + id;

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            VisitorAppointmentRequest entParamsList = new VisitorAppointmentRequest();

            foreach (DataRow dr in x.Rows)
            {
                entParamsList.REQUEST_ID = Convert.ToInt32(dr["RequestID"]);
                entParamsList.VISITOR_NAME = Convert.ToString(dr["Visitor_Name"]);
                entParamsList.VISITORCOMPANY = Convert.ToString(dr["VisitorCompany"]);
                entParamsList.MOBILENO = Convert.ToString(dr["mobileNo"]);
                entParamsList.NATURE_OF_WORK = Convert.ToString(dr["nature_of_work"]);
                entParamsList.DESIGNATION = Convert.ToString(dr["Designation"]);
                entParamsList.APPOINTMENT_FROM_DATE = Convert.ToDateTime(dr["appointment_from_date"]);
                entParamsList.APPOINTMENT_TO_DATE = Convert.ToDateTime(dr["appointment_to_date"]);
                entParamsList.VISITOR_ALLOWED_FROM_TIME = Convert.ToDateTime(dr["Visitor_Allowed_From_time"]);
                entParamsList.VISITOR_ALLOWED_TO_TIME = Convert.ToDateTime(dr["visitor_Allowed_To_Time"]);
                entParamsList.VISITOR_NATIONALITY = Convert.ToString(dr["visitor_nationality"]);
                entParamsList.PURPOSEOFVISIT = Convert.ToString(dr["PurposeOfVisit"]);
                entParamsList.ADDITIONAL_INFO = Convert.ToString(dr["additional_Info"]);                          
            }
            return entParamsList;
        }
    }
}