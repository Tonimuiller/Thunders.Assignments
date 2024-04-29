namespace Thunders.Assignments.Domain.Repositories.Assignment;

public interface IAssignmentRepository
{
    public void Add(Entities.Assignment assignment);
    public void Update(Entities.Assignment assignment);
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
    public Task<Entities.Assignment?> GetAsync(Guid id, CancellationToken cancellationToken);
    public Task<List<Entities.Assignment>> GetAsync(GetAssignmentsOptions options, CancellationToken cancellationToken);
}
