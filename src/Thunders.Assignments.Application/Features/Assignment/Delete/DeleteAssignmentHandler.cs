using Thunders.Assignments.Application.Abstractions.Mediator;
using Thunders.Assignments.Application.Models;
using Thunders.Assignments.Domain.Repositories;
using Thunders.Assignments.Domain.Repositories.Assignment;

namespace Thunders.Assignments.Application.Features.Assignment.Delete;

internal sealed class DeleteAssignmentHandler : IUseCaseHandler<DeleteAssignmentRequest>
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IUnitOfWork _unitOfWork;
    public DeleteAssignmentHandler(
        IAssignmentRepository assignmentRepository,
        IUnitOfWork unitOfWork)
    {
        _assignmentRepository = assignmentRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(DeleteAssignmentRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        if (!await _assignmentRepository.ExistsAsync(request.Id, cancellationToken))
        {
            return Result.NotFound(AssignmentResources.AssignmentIdNotFound);
        }

        await _assignmentRepository.DeleteAsync(request.Id, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);
        return Result.Ok();
    }
}
