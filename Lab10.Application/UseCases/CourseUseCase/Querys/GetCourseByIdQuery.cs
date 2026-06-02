using Lab10.Application2.Interfaces;
using MediatR;
using Lab10.Domain2.Interfaces;
using Lab10.Domain2.Models;
namespace Lab10.Application2.UseCases.CourseUseCase.Querys;

public class GetCourseByIdQuery : IRequest<Course?>
{
    public int CourseId { get; set; }
}
internal sealed class GetCourseByIdQueryHandler : IRequestHandler<GetCourseByIdQuery, Course?>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IBackgroundJobService _backgroundJobService;

    public GetCourseByIdQueryHandler(ICourseRepository courseRepository, IBackgroundJobService backgroundJobService)
    {
        _courseRepository = courseRepository;
        _backgroundJobService = backgroundJobService;
    }
    public async Task<Course?> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.CourseId);
        
        // Si no existe, manejamos la regla devolviendo null (o lanzando excepción según prefieras)
        if (course == null)
        {
            return null;
        }
        
        if (course != null)
        {
            // Programado para ejecutarse en 10 minutos de forma asíncrona
            _backgroundJobService.ScheduleNotification(
                $"Sistema: Se consultó el curso '{course.Title}'. Verificación programada ejecutada.", 
                TimeSpan.FromMinutes(10));
        }
        return course;
    }
}