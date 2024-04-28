using Thunders.Assignments.Application.Abstractions.Mediator;
using Thunders.Assignments.Application.Models;

namespace Thunders.Assignments.Application.Features.Assignment.List;

internal sealed class ListAssignmentHandler : IUseCaseHandler<ListAssignmentRequest, IReadOnlyList<ListAssignmentResponse>>
{
    public ListAssignmentHandler()
    {

    }

    public Task<Result<IReadOnlyList<ListAssignmentResponse>>> Handle(ListAssignmentRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
