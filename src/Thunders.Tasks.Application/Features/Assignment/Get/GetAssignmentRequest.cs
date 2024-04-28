using Thunders.Assignments.Application.Abstractions.Mediator;

namespace Thunders.Assignments.Application.Features.Assignment.Get;

public sealed record GetAssignmentRequest : UseCaseRequest<GetAssignmentResponse>
{
    public GetAssignmentRequest(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
