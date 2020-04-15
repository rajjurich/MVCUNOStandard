using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{

  public class EssTaOdReqModel
    {
      public int ESS_OD_ID { get; set; }

      public int ESS_OD_EMPID { get; set; }

      public string ESS_OD_FROLOC { get; set; }

      public string ESS_OD_TOLOC { get; set; }

      public string ESS_OD_ODCD { get; set; }

      public DateTime ESS_OD_REQUESTDT { get; set; }

      public DateTime ESS_OD_FROMDT { get; set; }

      public DateTime ESS_OD_FROMTM { get; set; }

      public DateTime ESS_OD_TODT { get; set; }

      public DateTime ESS_OD_TOTM { get; set; }

      public int ESS_OD_REASON_ID { get; set; }

      public string ESS_OD_REODRK { get; set; }

      public int ESS_SANCTION_ID { get; set; }

      public DateTime ESS_OD_SANCDT { get; set; }

      public string ESS_OD_SANC_REODRK { get; set; }

      public double ESS_OD_ORDER { get; set; }

      public string ESS_OD_STATUS { get; set; }

      public string ESS_OD_OLDSTATUS { get; set; }

      public int ESS_OD_ISDELETED { get; set; }

      public DateTime ESS_OD_DELETEDDATE { get; set; }

      public double ESS_OD_LVDAYS { get; set; }

      public string ESS_REQUEST_TYPE { get; set; }

      public string NAME { get; set; }

      public string ipaddress { get; set; }

    }
}
