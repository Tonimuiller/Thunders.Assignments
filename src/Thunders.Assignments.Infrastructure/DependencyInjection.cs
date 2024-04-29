using Microsoft.Extensions.DependencyInjection;
using Thunders.Assignments.Domain.Repositories;
using Thunders.Assignments.Domain.Repositories.Assignment;
using Thunders.Assignments.Infrastructure.Persistence.Contexts;
using Thunders.Assignments.Infrastructure.Persistence.Repositories;
using Thunders.Assignments.Infrastructure.Persistence.UnitOfWork;

namespace Thunders.Assignments.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IAssignmentRepository, AssignmentRepository>();
        services.AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork>();
        services.AddDbContext<AssignmentsContext>();
        return services;
    }
}
