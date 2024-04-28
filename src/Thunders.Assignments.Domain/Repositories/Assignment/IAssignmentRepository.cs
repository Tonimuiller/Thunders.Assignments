namespace Thunders.Assignments.Domain.Repositories.Assignment;

public interface IAssignmentRepository
{
    public Task AddAsync(Entities.Assignment assignment, CancellationToken cancellationToken);
    public Task UpdateAsync(Entities.Assignment assignment, CancellationToken cancellationToken);
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    public Task<Entities.Assignment> GetAsync(Guid id, CancellationToken cancellationToken);
    public Task<List<Entities.Assignment>> GetAsync(GetAssignmentsOptions options, CancellationToken cancellationToken);
}
