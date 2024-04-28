using Thunders.Assignments.Application.Features.Assignment.Abstractions;

namespace Thunders.Assignments.Application.Features.Assignment.Update;

public sealed record UpdateAssignmentRequest : SaveAssignmentRequest<UpdateAssignmentResponse>
{
    public Guid Id { get; set; }
}
