using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Infrastructure.Shared.Extensions.EnumBuilderExtensions
{
    public static partial class EnumExtension
    {
        public static string? ToDisplay(this Enum enumVal)
        {
            return enumVal.GetType()
                                    .GetMember(enumVal.ToString())
                                    .First()
                                    .GetCustomAttribute<DisplayAttribute>()!.Name??null;
        }
        public static IEnumerable<T> CastToList<T>(Type val)where T : Enum
        {
            return Enum.GetValues(val).Cast<T>();
        }
    }
}
