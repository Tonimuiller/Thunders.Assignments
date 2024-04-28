using Thunders.Assignments.Application.Abstractions.Mediator;
using Thunders.Assignments.Application.Models;
using Thunders.Assignments.Domain.Repositories;
using Thunders.Assignments.Domain.Repositories.Assignment;

namespace Thunders.Assignments.Application.Features.Assignment.Add;

internal sealed class AddAssignmentHandler : IUseCaseHandler<AddAssignmentRequest, AddAssignmentResponse>
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddAssignmentHandler(
        IAssignmentRepository assignmentRepository, 
        IUnitOfWork unitOfWork)
    {
        _assignmentRepository = assignmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<AddAssignmentResponse>> Handle(AddAssignmentRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (string.IsNullOrEmpty(request.Title))
        {
            return Result<AddAssignmentResponse>.BadRequest(AssignmentResources.TitleIsRequired);
        }

        if (request.Schedule == default)
        {
            return Result<AddAssignmentResponse>.BadRequest(AssignmentResources.ScheduleIsRequired);
        }

        var assignment = new Domain.Entities.Assignment
        {
            CreatedAt = DateTime.Now,
            Description = request.Description,
            Done = request.Done,
            Id = Guid.NewGuid(),
            Schedule = request.Schedule,
            Title = request.Title
        };

        _assignmentRepository.Add(assignment);
        await _unitOfWork.CommitAsync(CancellationToken.None);

        var response = new AddAssignmentResponse
        {
            Id = assignment.Id,
        };

        return Result<AddAssignmentResponse>.Created(response);
    }
}
