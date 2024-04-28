using MediatR;
using Thunders.Assignments.Application.Models;

namespace Thunders.Assignments.Application.Abstractions.Mediator;

public abstract record UseCaseRequest : IRequest<Result>
{
}
