using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Enum
{
    public enum StepBuyEnum
    {
        [Display(Name = "بسته بندی")]
        Packing = 1,
        [Display(Name = "راننده")]
        Driver = 2,
        [Display(Name = "اداره پست")]
        PostOffice = 3
    }
}
