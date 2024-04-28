using Thunders.Assignments.Application.Abstractions.Mediator;
using Thunders.Assignments.Application.Models;

namespace Thunders.Assignments.Application.Features.Assignment.List;

internal sealed class ListAssignmentHandler : UseCaseHandler<ListAssignmentRequest, IReadOnlyList<ListAssignmentResponse>>
{
    public ListAssignmentHandler()
    {

    }

    public override Task<Result<IReadOnlyList<ListAssignmentResponse>>> Handle(ListAssignmentRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
