using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class FilterModel
    {
        public int Companyid { get; set; }

        public string CompanyName { get; set; }

        public int Categoryid { get; set; }

        public string CategoryName { get; set; }

        public int Locationid { get; set; }

        public string LocationName { get; set; }

        public int Gradeid { get; set; }

        public string GradeName { get; set; }

        public int Groupid { get; set; }

        public string GroupName { get; set; }

        public int Divid { get; set; }

        public string DivName { get; set; }

        public int Departmentid { get; set; }

        public string DepartmentName { get; set; }

        public int Designationid { get; set; }

        public string DesignationName { get; set; }
    }
}