using MS.Services.TaskCatalog.Domain.Workflows;
using MsftFramework.Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.TaskCatalog.Domain.workflows
{
    public class WorkflowRoleModel : Entity<long>
    {
        public long UnitId { get;private set; }
        public string Name { get; private set; }
        public List<Role> Roles { get; private set; } = new();

        public ICollection<WorkflowStepInstance> WorkflowStepInstances { get; set; }


        public static WorkflowRoleModel Create(long id,string name,long unitId)
        {
            return new WorkflowRoleModel
            {
                Id=id,
                Name = name,
                UnitId = unitId

            };
        }
    }
    public class Role : Entity<long>
    {
        public long RoleId{ get; private set; }
        public string Name { get; private set; }
        public long WorkflowRoleModelId { get; private set; }
        public WorkflowRoleModel WorkflowRoleModel { get; private set; }
        public ICollection<WorkflowRoleUser> WorkflowRoleUsers { get;private set; }
       

        public static Role Create(string name, long roleId,long id)
        {
            return new Role
            {
                Name = name,
                RoleId = roleId,
                Id=id,
                
            };
        }
    }
}
