using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UNO.WebApi.Models
{
    public class AcsReader
    {
        public int RowID { get; set; }
        public int READER_ID { get; set; }        
        public string READER_DESCRIPTION { get; set; }        
        public int CTLR_ID { get; set; }
        public int? COMPANY_ID { get; set; }
        public string READER_MODE { get; set; }        
        public string READER_TYPE { get; set; }       
        public string READER_PASSES_FROM { get; set; }
        public string READER_PASSES_TO { get; set; }
        public bool READER_ISDELETED { get; set; }
        public Nullable<System.DateTime> READER_DELETEDDATE { get; set; }
        public bool IsActive { get; set; }
        public Nullable<long> EntryReaderMode { get; set; }
    }
    public class AcsReaderInfo
    {
        public int RowId { get; set; }      
        public string READER_DESCRIPTION { get; set; }       
    }
}