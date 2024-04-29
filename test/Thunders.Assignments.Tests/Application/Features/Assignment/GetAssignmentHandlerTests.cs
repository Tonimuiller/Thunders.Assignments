using FluentAssertions;
using NSubstitute;
using Thunders.Assignments.Application.Enums;
using Thunders.Assignments.Application.Features.Assignment;
using Thunders.Assignments.Application.Features.Assignment.Get;
using Thunders.Assignments.Application.Models;
using Thunders.Assignments.Domain.Repositories.Assignment;

namespace Thunders.Assignments.Tests.Application.Features.Assignment;

public sealed class GetAssignmentHandlerTests
{
    private readonly IAssignmentRepository _assignmentRepository;

    public GetAssignmentHandlerTests()
    {
        _assignmentRepository = Substitute.For<IAssignmentRepository>();       
    }

    [Fact]
    public async Task NullRequestParameter_ThrowsArgumentNullException()
    {
        //Arrange
        var handler = BuildHandler();

        //Act
        Func<Task<Result<GetAssignmentResponse>>> func = () => handler.Handle(null!, CancellationToken.None);

        //Assert
        await func
            .Should()
            .ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task NotExistentAssignmentId_NotFoundFail()
    {
        //Arrange
        var request = new GetAssignmentRequest(Guid.NewGuid());

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

    [Fact]
    public async Task ExistentAssignmentId_ReturnsOk()
    {
        //Arrange
        var request = new GetAssignmentRequest(Guid.NewGuid());

        var assignment = new Domain.Entities.Assignment
        {
            Id = request.Id,
            CreatedAt = DateTime.Now.AddDays(-10),
            UpdatedAt = DateTime.Now.AddDays(-5),
            Description = "Some description",
            Done = true,
            Schedule = DateTime.Now.AddDays(-2),
            Title = "Some title"
        };

        _assignmentRepository.GetAsync(request.Id, CancellationToken.None)
            .Returns(Task.FromResult(assignment));

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
            .BeTrue();

        result
            .Type
            .Should()
            .Be(ResultType.Ok);

        result
            .Value!
            .Should()
            .Match<GetAssignmentResponse>(response => 
                response.Id == assignment.Id
                && response.CreatedAt == assignment.CreatedAt
                && response.Description == assignment.Description
                && response.Schedule == assignment.Schedule
                && response.Title == assignment.Title
                && response.UpdatedAt == assignment.UpdatedAt
                && response.Done == assignment.Done);
    }

    private GetAssignmentHandler BuildHandler() => new GetAssignmentHandler(_assignmentRepository);
}
