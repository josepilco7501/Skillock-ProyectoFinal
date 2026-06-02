using MediatR;
using Lab10.Domain2.Interfaces;
using Lab10.Domain2.Models;
using Lab10.Application2.Interfaces; // Para usar tu interfaz

namespace Lab10.Application2.UseCases.CourseUseCase.Commands;

// El record define los datos de entrada requeridos por tu base de datos
public record CreateCourseCommand(string Title, string? Description, decimal Price) : IRequest<int>;
internal sealed class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, int>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IBackgroundJobService _backgroundJobService;

    public CreateCourseCommandHandler(ICourseRepository courseRepository, IBackgroundJobService backgroundJobService)
    {
        _courseRepository = courseRepository;
        _backgroundJobService = backgroundJobService;
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
        
        // 2. Llamamos al servicio a través de nuestra abstracción limpia
        _backgroundJobService.EnqueueNotification($"Administrador: El curso '{createdCourse.Title}' ha sido creado con éxito (ID: {createdCourse.Id}).");
        
        return createdCourse.Id;    
    }
}