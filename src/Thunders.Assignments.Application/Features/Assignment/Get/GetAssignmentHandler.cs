using Thunders.Assignments.Application.Abstractions.Mediator;
using Thunders.Assignments.Application.Models;

namespace Thunders.Assignments.Application.Features.Assignment.Get;

internal sealed class GetAssignmentHandler : IUseCaseHandler<GetAssignmentRequest, GetAssignmentResponse>
{
    public GetAssignmentHandler()
    {

    }

    public Task<Result<GetAssignmentResponse>> Handle(GetAssignmentRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
