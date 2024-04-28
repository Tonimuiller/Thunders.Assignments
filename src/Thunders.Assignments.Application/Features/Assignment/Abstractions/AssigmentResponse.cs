namespace Thunders.Assignments.Application.Features.Assignment.Abstractions;

public abstract record AssigmentResponse
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string Title { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateTime Schedule { get; set; }
    public bool Done { get; set; }
}
