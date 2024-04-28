namespace Thunders.Assignments.Application.Features.Assignment.Abstractions;

public abstract record SaveAssignmentResponse
{
    public Guid Id { get; set; }
}
