using MsftFramework.Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Domain.Users
{
    public class UserSelection:Entity<long>
    {
        public long UserId { get; set; }
        public Domain.Tasks.User User { get; set; } = default!;

        public long? SelectUserId { get; set; }
        public Domain.Tasks.User SelectUser { get; set; }=default!;
        public static UserSelection Create(long id,long userId,long? selectUserId)
        {
            return new UserSelection
            {
                Id=id,
                UserId = userId,
                SelectUserId = selectUserId
            };
        }
    }
}
