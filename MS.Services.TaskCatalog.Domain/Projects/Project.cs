using Ardalis.GuardClauses;
using MsftFramework.Core.Domain.Model;
using MsftFramework.Core.Exception;
using MsftFramework.Core.Domain.Events.Internal;
using MS.Services.TaskCatalog.Domain.Projects.Features.CreatingProject.Events.Domain;
using MS.Services.TaskCatalog.Domain.Projects.ValueObjects;
using MS.Services.TaskCatalog.Domain.Projects.Exceptions.Domain;

namespace MS.Services.TaskCatalog.Domain.Projects
{
    public class Project : Entity<ProjectId>
    {
        public ProjectName Name { get; private set; } = default!;
        public string? Description { get; private set; }
        public string Attachment { get; private set; } = default!;
        public ICollection<Tasks.Task> Tasks { get; set; } = default!;
        //public ICollection<Workflows.Workflow> Workflows { get; set; } = default!;
        public ICollection<Tasks.User> Users { get; set; } = default!;
        public static Project Create(
            ProjectId id,
            ProjectName name,
            string description,
            string attachment)
        {
            DomainEventsHandler.RaiseDomainEvent(new CreatingProjectEvent(id, name, description));

            var Project = new Project
            {
                Id = Guard.Against.Null(id, new ProjectDomainException("Project id can not be null")),
                Name = name,
                Description=description,
                Attachment=attachment

            };

            Project.ChangeName(name);


            return Project;
        }
        /// <summary>
        /// Sets Project item name.
        /// </summary>
        /// <param name="name">The name to be changed.</param>
        public void ChangeName(ProjectName name)
        {
            Guard.Against.Null(name, new ProjectDomainException("Project name cannot be null."));

            Name = name;
        }
    }
}