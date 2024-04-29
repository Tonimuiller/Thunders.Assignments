using Thunders.Assignments.Application.Abstractions.Mediator;
using Thunders.Assignments.Application.Models;
using Thunders.Assignments.Domain.Repositories.Assignment;

namespace Thunders.Assignments.Application.Features.Assignment.Get;

internal sealed class GetAssignmentHandler : IUseCaseHandler<GetAssignmentRequest, GetAssignmentResponse>
{
    private readonly IAssignmentRepository _assignmentRepository;

    public GetAssignmentHandler(IAssignmentRepository assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async Task<Result<GetAssignmentResponse>> Handle(GetAssignmentRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        var assignment = await _assignmentRepository.GetAsync(request.Id, cancellationToken);
        if (assignment == null)
        {
            return Result<GetAssignmentResponse>.NotFound(AssignmentResources.AssignmentIdNotFound);
        }

        return Result<GetAssignmentResponse>.Ok(new GetAssignmentResponse
        {
            CreatedAt = assignment.CreatedAt,
            Description = assignment.Description,
            Done = assignment.Done,
            Id = assignment.Id,
            Schedule = assignment.Schedule,
            Title = assignment.Title,
            UpdatedAt = assignment.UpdatedAt
        });
    }
}
