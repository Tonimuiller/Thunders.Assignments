namespace Thunders.Assignments.Domain.Repositories.Assignment;

public sealed record GetAssignmentsOptions
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? Done { get; set; }
    public DateOnly? ScheduleBeginning { get; set; }
    public DateOnly? ScheduleEnd { get; set; }
}
