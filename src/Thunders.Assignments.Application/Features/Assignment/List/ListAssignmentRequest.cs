using Thunders.Assignments.Application.Abstractions.Mediator;

namespace Thunders.Assignments.Application.Features.Assignment.List;

public sealed record ListAssignmentRequest : UseCaseRequest<IReadOnlyList<ListAssignmentResponse>> 
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? Done { get; set; }
    public DateOnly? ScheduleBeginning { get; set; }
    public DateOnly? ScheduleEnd { get; set; }
}
