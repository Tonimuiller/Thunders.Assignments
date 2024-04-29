using Microsoft.EntityFrameworkCore;
using Thunders.Assignments.Domain.Entities;
using Thunders.Assignments.Domain.Repositories.Assignment;
using Thunders.Assignments.Infrastructure.Persistence.Contexts;

namespace Thunders.Assignments.Infrastructure.Persistence.Repositories;

internal sealed class AssignmentRepository : IAssignmentRepository
{
    private readonly AssignmentsContext _assignmentsContext;

    public AssignmentRepository(AssignmentsContext assignmentsContext)
    {
        _assignmentsContext = assignmentsContext;
    }

    public void Add(Assignment assignment)
    {
        _assignmentsContext.Assignments.Add(assignment);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var assignment = await _assignmentsContext.Assignments.FindAsync(id, cancellationToken);
        if (assignment != null)
        {
            _assignmentsContext.Assignments.Remove(assignment);
        }
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _assignmentsContext.Assignments.AnyAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<Assignment?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _assignmentsContext.Assignments.FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<List<Assignment>> GetAsync(GetAssignmentsOptions options, CancellationToken cancellationToken)
    {
        return await _assignmentsContext
            .Assignments
            .Where(a => (string.IsNullOrEmpty(options.Title) || a.Title.ToLower().Contains(options.Title.ToLower()))
                && (string.IsNullOrEmpty(options.Description) || a.Description.ToLower().Contains(options.Description.ToLower()))
                && (options.Done == null || a.Done == options.Done)
                && ((options.ScheduleBeginning == null || options.ScheduleEnd == null) 
                || (a.Schedule >= options.ScheduleBeginning.Value.ToDateTime(TimeOnly.MinValue)
                && (a.Schedule <= options.ScheduleEnd.Value.ToDateTime(TimeOnly.MaxValue)))))
            .ToListAsync(cancellationToken);
    }

    public void Update(Assignment assignment)
    {
        _assignmentsContext.Attach(assignment);
        _assignmentsContext.Entry(assignment).State = EntityState.Modified;
    }
}
