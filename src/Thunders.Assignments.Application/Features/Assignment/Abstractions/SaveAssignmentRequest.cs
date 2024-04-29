using Thunders.Assignments.Application.Abstractions.Mediator;

namespace Thunders.Assignments.Application.Features.Assignment.Abstractions;

public abstract record SaveAssignmentRequest<TSaveAssignmentResponse>
    : UseCaseRequest<TSaveAssignmentResponse>
    where TSaveAssignmentResponse : SaveAssignmentResponse
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime Schedule { get; set; }
    public bool Done { get; set; }
}
