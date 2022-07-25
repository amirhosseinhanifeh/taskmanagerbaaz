using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Domain.SharedKernel
{
    public enum UserRoleType:short
    {
        [Display(Name="مجری")]
        User = 0,
        [Display(Name = "کنترلر")]
        Controller = 1,
        [Display(Name = "تستر")]
        Tester = 2,

    }
}
