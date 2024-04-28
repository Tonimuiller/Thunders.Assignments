using Thunders.Assignments.Application.Abstractions.Mediator;

namespace Thunders.Assignments.Application.Features.Assignment.List;

public sealed record ListAssignmentRequest : UseCaseRequest<IReadOnlyList<ListAssignmentResponse>> { }
