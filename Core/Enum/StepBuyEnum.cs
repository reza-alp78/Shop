using System.ComponentModel.DataAnnotations;

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
