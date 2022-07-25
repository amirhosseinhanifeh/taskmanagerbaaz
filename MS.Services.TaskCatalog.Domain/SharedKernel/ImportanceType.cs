using System.ComponentModel.DataAnnotations;

namespace MS.Services.TaskCatalog.Domain.SharedKernel
{
    public enum ImportanceType:short
    {
        
        [Display(Name = "ضروری مهم فوری")]
        ZMF,    
        [Display(Name = "ضروری مهم")]
        ZM,
        [Display(Name = "ضروری غیر مهم")]
        ZGM,
        [Display(Name = "مهم فوری")]
        MF,
        [Display(Name = "مهم غیر فوری")]
        MGF

    }
}
