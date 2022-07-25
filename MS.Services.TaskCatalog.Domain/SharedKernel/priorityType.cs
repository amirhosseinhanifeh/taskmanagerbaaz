using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Domain.SharedKernel
{
    public  enum priorityType
    {
        [Display(Name ="A+")]
       VeryHigh,
        [Display(Name = "A")]
        High = 1 ,
        [Display(Name = "B")]
        Medium,
        [Display(Name = "C")]
        Low

    }
}
