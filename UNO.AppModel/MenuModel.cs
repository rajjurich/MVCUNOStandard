using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNO.AppModel
{
    public class MenuModel
    {
        public long MENU_ID { get; set; }

        [Display(Name = "Menu Name")]
        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "Please Enter Menu Name")]
        public string MENU_NAME { get; set; }

        [Display(Name = "Menu Link")]
        [MaxLength(200)]
        [StringLength(200)]
        [Required(ErrorMessage = "Please Enter Menu Link")]
        public string MENU_URL { get; set; }

        [Display(Name = "Module Name")]
        [Required(ErrorMessage = "Please Select Module Name")]
        public long MODULE_ID { get; set; }

        public string MODULE_NAME { get; set; }

        [Display(Name = "Sub Module Name")]
        [Required(ErrorMessage = "Please Select Sub Module Name")]
        public long SMODULE_ID { get; set; }

        [Display(Name = "Menu Item Position")]
        [Required(ErrorMessage = "Please Select Menu Item Position")]
        public float MENU_ITEMPOSITION { get; set; }
        public string SMODULE_NAME { get; set; }

        public string ipaddress { get; set; }

    }

}
