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
    public interface IVisitorAppointmentRequestService
    {
        Task<int> Create(VisitorAppointmentRequest entity, string ipaddress, int activeuser);
        Task<int> Edit(VisitorAppointmentRequest entity, string ipaddress, int activeuser);
        Task<int> Delete(int Requestid, string ipaddress, int activeuser);
        IQueryable<VisitorAppointmentRequest> GetVisitorAppointmentRequest();
        VisitorAppointmentRequest GetVisitorAppointmentRequest(int id);
        IQueryable<ApprovalAuth> Get();
    }
    public class VisitorAppointmentRequestService : IVisitorAppointmentRequestService
    {
        private UnitOfWork _unitOfWork;
        private DatabaseHelper _DatabaseHelper;

        public VisitorAppointmentRequestService(IUnitOfWork unitOfWork, DatabaseHelper databaseHelper)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
            _DatabaseHelper = databaseHelper;
        }
        public async Task<int> Create(VisitorAppointmentRequest entity, string ipaddress, int activeuser)
        {
            string query = " insert into Visitor_Appointment_Request(Visitor_id,Approval_Authority_Code,approval_status,Visitor_Allowed_From_time,visitor_Allowed_To_Time,appointment_from_date,appointment_to_date,nature_of_work,additional_Info,isDeleted,isDeletedDate,Visitor_Name,VisitorCompany,mobileNo,CreatedOn,CreatedBy,Designation,CheckedOutTime,PurposeOfVisit,Visitor_MiddleName,Visitor_LastName,visitor_nationality,visitor_location,Total_Count,IS_SYNC,COMPANY_ID)  ";
            query += " values ('0', '" + entity.APPROVAL_AUTHORITY_CODE + "', 'N','" + entity.VISITOR_ALLOWED_FROM_TIME.ToString("dd/MMM/yyyy HH:mm:ss") + "','" + entity.VISITOR_ALLOWED_TO_TIME.ToString("dd/MMM/yyyy HH:mm:ss") + "','" + entity.APPOINTMENT_FROM_DATE.ToString("dd/MMM/yyyy HH:mm:ss") + "','" + entity.APPOINTMENT_TO_DATE.ToString("dd/MMM/yyyy HH:mm:ss") + "','" + entity.NATURE_OF_WORK + "','" + entity.ADDITIONAL_INFO + "','0','','" + entity.VISITOR_NAME + "','" + entity.VISITORCOMPANY + "','" + entity.MOBILENO + "',";
            query += "'" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss") + "','admin','" + entity.DESIGNATION + "','" + entity.CHECKEDOUTTIME.ToString("dd/MMM/yyyy HH:mm:ss") + "','" + entity.PURPOSEOFVISIT + "','" + entity.VISITOR_MIDDLENAME + "','" + entity.VISITOR_LASTNAME + "','" + entity.VISITOR_NATIONALITY + "','" + entity.VISITOR_LOCATION + "','0',0," + entity.COMPANY_ID + ") ";
            query += " select @@Identity ;";
            return await _DatabaseHelper.Insert(query, CommandType.Text,ipaddress,activeuser);
        }
        public async Task<int> Edit(VisitorAppointmentRequest entity, string ipaddress, int activeuser)
        {
            string query = "Update Visitor_Appointment_Request set visitor_Allowed_To_Time = '" + entity.VISITOR_ALLOWED_TO_TIME.ToString("dd/MMM/yyyy HH:mm:ss") + "', Visitor_Allowed_From_time = '" + entity.VISITOR_ALLOWED_FROM_TIME.ToString("dd/MMM/yyyy HH:mm:ss") + "' , appointment_from_date = '" + entity.APPOINTMENT_FROM_DATE.ToString("dd/MMM/yyyy HH:mm:ss") + "' , appointment_to_date ='" + entity.APPOINTMENT_TO_DATE.ToString("dd/MMM/yyyy HH:mm:ss") + "' , nature_of_work ='" + entity.NATURE_OF_WORK + "', additional_Info ='" + entity.ADDITIONAL_INFO + "', Visitor_Name ='" + entity.VISITOR_NAME + "', VisitorCompany ='" + entity.VISITORCOMPANY + "', mobileNo ='" + entity.MOBILENO + "', Designation ='" + entity.DESIGNATION + "', PurposeOfVisit ='" + entity.PURPOSEOFVISIT + "', Visitor_MiddleName ='" + entity.VISITOR_MIDDLENAME + "', Visitor_LastName ='" + entity.VISITOR_LASTNAME + "', visitor_nationality ='" + entity.VISITOR_NATIONALITY + "', visitor_location ='" + entity.VISITOR_LOCATION + "'   where requestid =  " + entity.REQUEST_ID;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public async Task<int> Delete(int Requestid, string ipaddress, int activeuser)
        {
            string query = "Update Visitor_Appointment_Request set isDeleted =1 , isDeletedDate ='" + DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss") + "' where Requestid = " + Requestid;
            return await _DatabaseHelper.Insert(query, CommandType.Text, ipaddress, activeuser);
        }

        public IQueryable<VisitorAppointmentRequest> GetVisitorAppointmentRequest()
        {
            string query = "select RequestID,Visitor_Name,VisitorCompany,nature_of_work,mobileNo,appointment_from_date,appointment_to_date,Visitor_Allowed_From_time,visitor_Allowed_To_Time,approval_status,additional_Info,Designation,PurposeOfVisit,Visitor_MiddleName,Visitor_LastName from Visitor_Appointment_Request where  isDeletedDate =0";

            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<VisitorAppointmentRequest> entParamsList = new List<VisitorAppointmentRequest>();

            foreach (DataRow dr in x.Rows)
            {
                VisitorAppointmentRequest entVisitorAppointmentRequest = new VisitorAppointmentRequest();
                entVisitorAppointmentRequest.REQUEST_ID = Convert.ToInt32(dr["RequestID"]);
                entVisitorAppointmentRequest.VISITOR_NAME = Convert.ToString(dr["Visitor_Name"]);
                entVisitorAppointmentRequest.VISITORCOMPANY = Convert.ToString(dr["VisitorCompany"]);
                entVisitorAppointmentRequest.NATURE_OF_WORK = Convert.ToString(dr["nature_of_work"]);
                entVisitorAppointmentRequest.MOBILENO = Convert.ToString(dr["mobileNo"]);
                entVisitorAppointmentRequest.APPOINTMENT_FROM_DATE = Convert.ToDateTime(dr["appointment_from_date"]);
                entVisitorAppointmentRequest.APPOINTMENT_TO_DATE = Convert.ToDateTime(dr["appointment_to_date"]);
                entVisitorAppointmentRequest.VISITOR_ALLOWED_FROM_TIME = Convert.ToDateTime(dr["Visitor_Allowed_From_time"]);
                entVisitorAppointmentRequest.VISITOR_ALLOWED_TO_TIME = Convert.ToDateTime(dr["visitor_Allowed_To_Time"]);
                entVisitorAppointmentRequest.APPROVAL_STATUS = Convert.ToString(dr["approval_status"]);
                entVisitorAppointmentRequest.ADDITIONAL_INFO = Convert.ToString(dr["additional_Info"]);
                entVisitorAppointmentRequest.DESIGNATION = Convert.ToString(dr["Designation"]);
                entVisitorAppointmentRequest.PURPOSEOFVISIT = Convert.ToString(dr["PurposeOfVisit"]);
                entVisitorAppointmentRequest.VISITOR_MIDDLENAME = Convert.ToString(dr["Visitor_MiddleName"]);
                entVisitorAppointmentRequest.VISITOR_LASTNAME = Convert.ToString(dr["Visitor_LastName"]); 
                entParamsList.Add(entVisitorAppointmentRequest);
            }
            return entParamsList.AsQueryable(); 
        }
        
        public VisitorAppointmentRequest GetVisitorAppointmentRequest(int id)
        {
            string query = "select RequestID,Visitor_Name,VisitorCompany,nature_of_work,mobileNo,appointment_from_date,appointment_to_date,Visitor_Allowed_From_time,visitor_Allowed_To_Time,approval_status,additional_Info,Designation,PurposeOfVisit,Visitor_MiddleName,Visitor_LastName from Visitor_Appointment_Request where  isDeletedDate =0 and RequestID =" + id;
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            VisitorAppointmentRequest entParamsList = new VisitorAppointmentRequest();

            foreach (DataRow dr in x.Rows)
            {
                entParamsList.REQUEST_ID = Convert.ToInt32(dr["RequestID"]);
                entParamsList.VISITOR_NAME = Convert.ToString(dr["Visitor_Name"]);
                entParamsList.VISITORCOMPANY = Convert.ToString(dr["VisitorCompany"]);
                entParamsList.NATURE_OF_WORK = Convert.ToString(dr["nature_of_work"]);
                entParamsList.MOBILENO = Convert.ToString(dr["mobileNo"]);
                entParamsList.APPOINTMENT_FROM_DATE = Convert.ToDateTime(dr["appointment_from_date"]);
                entParamsList.APPOINTMENT_TO_DATE = Convert.ToDateTime(dr["appointment_to_date"]);
                entParamsList.VISITOR_ALLOWED_FROM_TIME = Convert.ToDateTime(dr["Visitor_Allowed_From_time"]);
                entParamsList.VISITOR_ALLOWED_TO_TIME = Convert.ToDateTime(dr["visitor_Allowed_To_Time"]);
                entParamsList.APPROVAL_STATUS = Convert.ToString(dr["approval_status"]);
                entParamsList.ADDITIONAL_INFO = Convert.ToString(dr["additional_Info"]);
                entParamsList.DESIGNATION = Convert.ToString(dr["Designation"]);
                entParamsList.PURPOSEOFVISIT = Convert.ToString(dr["PurposeOfVisit"]);
                entParamsList.VISITOR_MIDDLENAME = Convert.ToString(dr["Visitor_MiddleName"]);
                entParamsList.VISITOR_LASTNAME = Convert.ToString(dr["Visitor_LastName"]);         
            }
            return entParamsList;
        }
        public IQueryable<ApprovalAuth> Get()
        {
            string query = "select EOD_EMPID,EPD_FIRST_NAME from ENT_EMPLOYEE_DTLS";
            var x = _DatabaseHelper.GetDataTable(query, CommandType.Text);
            List<ApprovalAuth> entApprovalList = new List<ApprovalAuth>();

            foreach (DataRow dr in x.Rows)
            {
                ApprovalAuth entApproval = new ApprovalAuth();
                entApproval.APPROVAL_ID = Convert.ToString(dr["EOD_EMPID"]);
                entApproval.APPROVAL_NAME = Convert.ToString(dr["EPD_FIRST_NAME"]);
                entApprovalList.Add(entApproval);
            }
            return entApprovalList.AsQueryable();
        }
    }
}