using Thunders.Assignments.Application.Abstractions.Mediator;
using Thunders.Assignments.Application.Models;
using Thunders.Assignments.Domain.Repositories;
using Thunders.Assignments.Domain.Repositories.Assignment;

namespace Thunders.Assignments.Application.Features.Assignment.Update;

internal sealed class UpdateAssignmentHandler : IUseCaseHandler<UpdateAssignmentRequest, UpdateAssignmentResponse>
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateAssignmentHandler(
        IAssignmentRepository assignmentRepository, 
        IUnitOfWork unitOfWork)
    {
        _assignmentRepository = assignmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<UpdateAssignmentResponse>> Handle(UpdateAssignmentRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (string.IsNullOrEmpty(request.Title))
        {
            return Result<UpdateAssignmentResponse>.BadRequest(AssignmentResources.TitleIsRequired);
        }

        if (request.Schedule == default)
        {
            return Result<UpdateAssignmentResponse>.BadRequest(AssignmentResources.ScheduleIsRequired);
        }

        var assignment = await _assignmentRepository.GetAsync(request.Id, cancellationToken);
        if (assignment == null)
        {
            return Result<UpdateAssignmentResponse>.NotFound(AssignmentResources.AssignmentIdNotFound);
        }

        assignment.Schedule = request.Schedule;
        assignment.Title = request.Title;
        assignment.Description = request.Description;
        assignment.UpdatedAt = DateTime.Now;
        assignment.Done = request.Done;
        _assignmentRepository.Update(assignment);
        await _unitOfWork.CommitAsync(cancellationToken);
        var updatedAssignmentResponse = new UpdateAssignmentResponse
        {
            Id = assignment.Id,
        };

        return Result<UpdateAssignmentResponse>.Ok(updatedAssignmentResponse);
    }
}
