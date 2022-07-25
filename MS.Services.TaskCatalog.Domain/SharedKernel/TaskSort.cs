using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Domain.SharedKernel
{
    public enum TaskStatus
    {
        /// <summary>
        /// تکمیل شده
        /// </summary>
        Done = 0,
        /// <summary>
        /// تکمیل نشده
        /// </summary>
        UnCompleted = 1,
        /// <summary>
        /// انجام نشده
        /// </summary>
        UnDone = 2
    }
    public enum TaskSort
    {

        /// <summary>
        /// نزدیک ترین ها
        /// </summary>
        New = 0,
        /// <summary>
        /// 
        /// </summary>
        Old = 1,
        /// <summary>
        /// تاخیر
        /// </summary>
        Delay = 2,
        /// <summary>
        /// ضرروت
        /// </summary>
        Priority = 3,
        /// <summary>
        /// اهمیت
        /// </summary>
        Importance = 4,

        /// <summary>
        /// برگشتی
        /// </summary>
        ReturnCount = 5

    }
    /// <summary>
    /// 
    /// </summary>
    public enum TodayOrderPriority
    {
        Admin = 0,
        User = 1
    }
}
