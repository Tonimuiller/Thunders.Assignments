using Thunders.Assignments.Application.Abstractions.Mediator;
using Thunders.Assignments.Application.Models;

namespace Thunders.Assignments.Application.Features.Assignment.Get;

internal sealed class GetAssignmentHandler : UseCaseHandler<GetAssignmentRequest, GetAssignmentResponse>
{
    public GetAssignmentHandler()
    {

    }

    public override Task<Result<GetAssignmentResponse>> Handle(GetAssignmentRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
