using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Domain.Tasks.Exceptions.Domain
{
    public static class GuardCustomExpectionExtentions
    {
        public static IGuardClause CheckLength(this IGuardClause guardClause,string input, int maxlength)
        {
            if (input.Length > maxlength)
                throw new ArgumentException($"مقدار نباید بیشتر از {maxlength} کاراکتر باشد");
            return guardClause;
        }
    }
}
