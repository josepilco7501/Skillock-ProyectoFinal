using MediatR;
using Lab10.Domain2.Interfaces;
using Lab10.Domain2.Models;

namespace Lab10.Application2.UseCases.CourseUseCase.Commands;

// El record define los datos de entrada requeridos por tu base de datos
public record CreateCourseCommand(string Title, string? Description, decimal Price) : IRequest<int>;

internal sealed class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, int>
{
    private readonly ICourseRepository _courseRepository;

    public CreateCourseCommandHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var newCourse = new Course
        {
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            IsActive = true 
        };

        var createdCourse = await _courseRepository.AddAsync(newCourse);
        
        return createdCourse.Id; 
    }
}