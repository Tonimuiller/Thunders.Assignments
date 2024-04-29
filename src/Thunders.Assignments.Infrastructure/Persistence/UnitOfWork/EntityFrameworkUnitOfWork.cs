using Thunders.Assignments.Domain.Repositories;
using Thunders.Assignments.Infrastructure.Persistence.Contexts;

namespace Thunders.Assignments.Infrastructure.Persistence.UnitOfWork;

internal class EntityFrameworkUnitOfWork : IUnitOfWork
{
    private readonly AssignmentsContext _assignmentsContext;

    public EntityFrameworkUnitOfWork(AssignmentsContext assignmentsContext)
    {
        _assignmentsContext = assignmentsContext;
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _assignmentsContext.SaveChangesAsync(cancellationToken);
    }
}
