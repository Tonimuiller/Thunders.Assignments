using Thunders.Assignments.Application.Abstractions.Mediator;
using Thunders.Assignments.Application.Models;

namespace Thunders.Assignments.Application.Features.Assignment.Delete;

internal sealed class DeleteAssignmentHandler : UseCaseHandler<DeleteAssignmentRequest>
{
    public DeleteAssignmentHandler()
    {

    }

    public override Task<Result> Handle(DeleteAssignmentRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
