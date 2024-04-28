namespace Thunders.Assignments.Domain.Repositories.Assignment;

public interface IAssignmentRepository
{
    public void Add(Entities.Assignment assignment);
    public void Update(Entities.Assignment assignment);
    public void Delete(Guid id);
    public Task<Entities.Assignment> GetAsync(Guid id, CancellationToken cancellationToken);
    public Task<List<Entities.Assignment>> GetAsync(GetAssignmentsOptions options, CancellationToken cancellationToken);
}
