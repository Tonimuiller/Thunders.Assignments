using Thunders.Assignments.Application.Abstractions.Mediator;
using Thunders.Assignments.Application.Models;

namespace Thunders.Assignments.Application.Features.Assignment.Update;

internal sealed class UpdateAssignmentHandler : IUseCaseHandler<UpdateAssignmentRequest, UpdateAssignmentResponse>
{
    public Task<Result<UpdateAssignmentResponse>> Handle(UpdateAssignmentRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
