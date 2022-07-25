namespace MS.Services.TaskCatalog.Contract.Workflows.Dtos
{
    public class RoleModelDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<RolesDto> Roles { get; set; }
    }
    public class RolesDto
    {
        public string Name { get; set; }
        public long RoleId { get; set; }
    }
}