using FluentAssertions;
using NSubstitute;
using Thunders.Assignments.Application.Enums;
using Thunders.Assignments.Application.Features.Assignment;
using Thunders.Assignments.Application.Features.Assignment.Delete;
using Thunders.Assignments.Application.Models;
using Thunders.Assignments.Domain.Repositories;
using Thunders.Assignments.Domain.Repositories.Assignment;

namespace Thunders.Assignments.Tests.Application.Features.Assignment;

public sealed class DeleteAssignmentHandlerTests
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAssignmentHandlerTests()
    {
        _assignmentRepository = Substitute.For<IAssignmentRepository>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
    }

    [Fact]
    public async Task NullRequestParameter_ThrowsArgumentNullException()
    {
        //Arrange
        var handler = BuildHandler();

        //Act
        Func<Task<Result>> func = () => handler.Handle(null!, CancellationToken.None);

        //Assert
        await func
            .Should()
            .ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task NotExistentAssignmentId_NotFoundFail()
    {
        //Arrange
        var request = new DeleteAssignmentRequest
        {
            Id = Guid.NewGuid(),
        };
        _assignmentRepository.ExistsAsync(request.Id, CancellationToken.None)
            .Returns(Task.FromResult(false));
        var handler = BuildHandler();

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
        await _assignmentRepository
            .DidNotReceive()
            .DeleteAsync(Arg.Any<Guid>(), CancellationToken.None);
        
        await _unitOfWork
            .DidNotReceive()
            .CommitAsync(CancellationToken.None);

        result
            .Should()
            .NotBeNull();

        result
            .IsSuccess
            .Should()
            .BeFalse();

        result
            .Type
            .Should()
            .Be(ResultType.NotFound);

        result
            .Message
            .Should()
            .Be(AssignmentResources.AssignmentIdNotFound);
    }

    [Fact]
    public async Task ExistentAssignmentId_ReturnsOkResult()
    {
        //Arrange
        var request = new DeleteAssignmentRequest
        {
            Id = Guid.NewGuid(),
        };
        _assignmentRepository.ExistsAsync(request.Id, CancellationToken.None)
            .Returns(Task.FromResult(true));
        var handler = BuildHandler();

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
        await _assignmentRepository
            .Received()
            .DeleteAsync(Arg.Any<Guid>(), CancellationToken.None);

        await _unitOfWork
            .Received()
            .CommitAsync(CancellationToken.None);

        result
            .Should()
            .NotBeNull();

        result
            .IsSuccess
            .Should()
            .BeTrue();

        result
            .Type
            .Should()
            .Be(ResultType.Ok);
    }

    private DeleteAssignmentHandler BuildHandler() => 
        new DeleteAssignmentHandler(
            _assignmentRepository,
            _unitOfWork);
}
