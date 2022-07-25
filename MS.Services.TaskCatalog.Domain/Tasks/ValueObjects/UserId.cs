using Ardalis.GuardClauses;
using MsftFramework.Abstractions.Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Domain.Tasks.ValueObjects
{
    public record UserId : AggregateId
    {
        public UserId(long value) : base(value)
        {
            Guard.Against.NegativeOrZero(value, nameof(value));
        }

        public static implicit operator long(UserId id) => id.Value;

        public static implicit operator UserId(long id) => new(id);
    }
}
