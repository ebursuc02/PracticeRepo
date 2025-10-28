using TimeCardsManagement_E20.Domain.Entities;

namespace TimeCardsManagement_E20.Application.Abstractions;

public interface IProjectCatalog
{
    IEnumerable<Project> GetProjects();
    Project? GetById(string projectId);
    IEnumerable<ProjectTask> GetTasksFor(string projectId);
    bool TaskExists(string projectId, string taskCode);
}
