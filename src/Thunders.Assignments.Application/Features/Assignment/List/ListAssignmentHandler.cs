using Thunders.Assignments.Application.Abstractions.Mediator;
using Thunders.Assignments.Application.Models;
using Thunders.Assignments.Domain.Repositories.Assignment;

namespace Thunders.Assignments.Application.Features.Assignment.List;

internal sealed class ListAssignmentHandler : IUseCaseHandler<ListAssignmentRequest, IReadOnlyList<ListAssignmentResponse>>
{
    private IAssignmentRepository _assignmentRepository;

    public ListAssignmentHandler(IAssignmentRepository assignmentRepository)
    {
        _assignmentRepository = assignmentRepository;
    }

    public async Task<Result<IReadOnlyList<ListAssignmentResponse>>> Handle(ListAssignmentRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        if ((request.ScheduleBeginning.HasValue && !request.ScheduleEnd.HasValue)
            || (!request.ScheduleBeginning.HasValue && request.ScheduleEnd.HasValue))
        {
            return Result<IReadOnlyList<ListAssignmentResponse>>.BadRequest(AssignmentResources.CompleteScheduleDateRangeIsRequired);
        }

        if ((request.ScheduleBeginning.HasValue && request.ScheduleEnd.HasValue)
            && request.ScheduleBeginning > request.ScheduleEnd)
        {
            return Result<IReadOnlyList<ListAssignmentResponse>>.BadRequest(AssignmentResources.InvalidDateRange);
        }

        var assignments = await _assignmentRepository.GetAsync(new GetAssignmentsOptions
        {
            Description = request.Description,
            Done = request.Done,
            ScheduleBeginning = request.ScheduleBeginning,
            ScheduleEnd = request.ScheduleEnd,
            Title = request.Title
        }, cancellationToken);

        return Result<IReadOnlyList<ListAssignmentResponse>>.Ok(assignments
            .Select(a => new ListAssignmentResponse
            {
                CreatedAt = a.CreatedAt,
                Description = a.Description,
                Done = a.Done,
                Id = a.Id,
                Schedule = a.Schedule,
                Title = a.Title,
                UpdatedAt = a.UpdatedAt
            }).ToList());
    }
}
