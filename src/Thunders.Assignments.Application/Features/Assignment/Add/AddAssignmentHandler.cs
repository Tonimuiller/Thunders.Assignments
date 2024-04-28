using Thunders.Assignments.Application.Abstractions.Mediator;
using Thunders.Assignments.Application.Models;

namespace Thunders.Assignments.Application.Features.Assignment.Add;

internal sealed class AddAssignmentHandler : UseCaseHandler<AddAssignmentRequest, AddAssignmentResponse>
{
    public override Task<Result<AddAssignmentResponse>> Handle(AddAssignmentRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
