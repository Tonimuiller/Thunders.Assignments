using Thunders.Assignments.Application.Abstractions.Mediator;
using Thunders.Assignments.Application.Models;

namespace Thunders.Assignments.Application.Features.Assignment.Delete;

internal sealed class DeleteAssignmentHandler : IUseCaseHandler<DeleteAssignmentRequest>
{
    public DeleteAssignmentHandler()
    {

    }

    public Task<Result> Handle(DeleteAssignmentRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
