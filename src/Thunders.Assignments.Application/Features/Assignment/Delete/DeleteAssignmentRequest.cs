using Thunders.Assignments.Application.Abstractions.Mediator;

namespace Thunders.Assignments.Application.Features.Assignment.Delete;

public sealed record DeleteAssignmentRequest : UseCaseRequest
{
    public Guid Id { get; set; }
}
