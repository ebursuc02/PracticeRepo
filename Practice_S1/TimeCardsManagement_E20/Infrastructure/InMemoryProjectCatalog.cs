using TimeCardsManagement_E20.Application.Abstractions;
using TimeCardsManagement_E20.Domain.Entities;

namespace TimeCardsManagement_E20.Infrastructure;

public sealed class InMemoryProjectCatalog : IProjectCatalog
{
    private readonly Dictionary<string, Project> _projects = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, List<ProjectTask>> _tasksByProject = new(StringComparer.OrdinalIgnoreCase);

    public InMemoryProjectCatalog AddProject(string id, string name, params (string Code, string DisplayName)[] tasks)
    {
        var project = new Project(id, name);
        _projects[id] = project;
        _tasksByProject[id] = tasks.Select(t => new ProjectTask(id, t.Code, t.DisplayName)).ToList();
        return this;
    }

    public IEnumerable<Project> GetProjects() => _projects.Values;

    public Project? GetById(string projectId)
        => _projects.TryGetValue(projectId, out var p) ? p : null;

    public IEnumerable<ProjectTask> GetTasksFor(string projectId)
        => _tasksByProject.TryGetValue(projectId, out var tasks) ? tasks : Enumerable.Empty<ProjectTask>();

    public bool TaskExists(string projectId, string taskCode)
        => _tasksByProject.TryGetValue(projectId, out var tasks)
           && tasks.Any(t => string.Equals(t.Code, taskCode, StringComparison.OrdinalIgnoreCase));
}
