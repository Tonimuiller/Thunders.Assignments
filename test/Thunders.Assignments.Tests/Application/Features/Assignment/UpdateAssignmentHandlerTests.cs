using FluentAssertions;
using NSubstitute;
using Thunders.Assignments.Application.Enums;
using Thunders.Assignments.Application.Features.Assignment;
using Thunders.Assignments.Application.Features.Assignment.Get;
using Thunders.Assignments.Application.Features.Assignment.Update;
using Thunders.Assignments.Application.Models;
using Thunders.Assignments.Domain.Repositories;
using Thunders.Assignments.Domain.Repositories.Assignment;

namespace Thunders.Assignments.Tests.Application.Features.Assignment;

public sealed class UpdateAssignmentHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAssignmentRepository _assignmentRepository;

    public UpdateAssignmentHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _assignmentRepository = Substitute.For<IAssignmentRepository>();
    }

    [Fact]
    public async Task NullRequestParameter_ThrowsArgumentNullException()
    {
        //Arrange
        var handler = BuildHandler();

        //Act
        Func<Task<Result<UpdateAssignmentResponse>>> func = () => handler.Handle(null!, CancellationToken.None);
        
        //Assert
        await func
            .Should()
            .ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task EmptyTitle_BadRequestFail()
    {
        //Arrange
        var request = new UpdateAssignmentRequest
        {
            Id = Guid.NewGuid(),
            Description = "Assignment",
            Done = false,
            Schedule = DateTime.Now.AddDays(5)
        };
        var handler = BuildHandler();

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
        _assignmentRepository
            .DidNotReceive()
            .Add(Arg.Any<Domain.Entities.Assignment>());
        await _unitOfWork.DidNotReceive().CommitAsync(CancellationToken.None);

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
            .Be(ResultType.BadRequest);
        result
            .Message
            .Should()
            .Be(AssignmentResources.TitleIsRequired);
    }

    [Fact]
    public async Task EmptySchedule_BadRequestFail()
    {
        //Arrange
        var request = new UpdateAssignmentRequest
        {
            Id = Guid.NewGuid(),
            Title = "Some title",
            Description = "Assignment",
            Done = false
        };
        var handler = BuildHandler();

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
        _assignmentRepository
            .DidNotReceive()
            .Add(Arg.Any<Domain.Entities.Assignment>());
        await _unitOfWork.DidNotReceive().CommitAsync(CancellationToken.None);

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
            .Be(ResultType.BadRequest);
        result
            .Message
            .Should()
            .Be(AssignmentResources.ScheduleIsRequired);
    }

    [Fact]
    public async Task NotExistentAssignmentId_NotFoundFail()
    {
        //Arrange
        var request = new UpdateAssignmentRequest
        {
            Id = Guid.NewGuid(),
            Description = "Some description",
            Done = true,
            Schedule = DateTime.Now.AddDays(-2),
            Title = "Some title"
        };

        _assignmentRepository.GetAsync(request.Id, CancellationToken.None)
            .Returns(Task.FromResult<Domain.Entities.Assignment>(null!));

        var handler = BuildHandler();

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
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

    [Fact] async Task CompleteRequest_ReturnsOkResult()
    {
        //Arrange
        var request = new UpdateAssignmentRequest
        {
            Id = Guid.NewGuid(),
            Title = "Changed title",
            Description = "Changed description",
            Done = true,
            Schedule = DateTime.Now.AddDays(-1)
        };

        var createdAt = DateTime.Now.AddDays(-10);
        var updatedAt = DateTime.Now.AddDays(-5);
        var assignment = new Domain.Entities.Assignment
        {
            Id = request.Id,
            CreatedAt = createdAt,
            UpdatedAt = updatedAt,
            Description = "Some description",
            Done = false,
            Schedule = DateTime.Now.AddDays(-2),
            Title = "Some title"
        };

        _assignmentRepository.GetAsync(request.Id, CancellationToken.None)
            .Returns(Task.FromResult<Domain.Entities.Assignment>(assignment));
        var handler = BuildHandler();

        //Act
        var result = await handler.Handle(request, CancellationToken.None);

        //Assert
        _assignmentRepository
            .Received()
            .Update(Arg.Is<Domain.Entities.Assignment>(a => 
                a.Title == request.Title
                && a.Description == request.Description
                && a.Done == request.Done
                && a.Schedule == request.Schedule
                && a.CreatedAt == createdAt
                && a.UpdatedAt != updatedAt
                && a.Id == request.Id));
        await _unitOfWork.Received().CommitAsync(CancellationToken.None);
        
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

    private UpdateAssignmentHandler BuildHandler() => 
        new UpdateAssignmentHandler(
            _assignmentRepository,
            _unitOfWork);
}