using Thunders.Assignments.Application.Abstractions.Mediator;
using Thunders.Assignments.Application.Models;

namespace Thunders.Assignments.Application.Features.Assignment.Update;

internal sealed class UpdateAssignmentHandler : UseCaseHandler<UpdateAssignmentRequest, UpdateAssigmentResponse>
{
    public override Task<Result<UpdateAssigmentResponse>> Handle(UpdateAssignmentRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
