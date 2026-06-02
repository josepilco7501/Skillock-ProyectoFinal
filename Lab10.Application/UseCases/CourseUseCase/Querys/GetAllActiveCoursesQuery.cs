using Lab10.Application2.Interfaces;
using MediatR;
using Lab10.Domain2.Interfaces;
using Lab10.Domain2.Models;

namespace Lab10.Application2.UseCases.CourseUseCase.Querys;

// La consulta no necesita parámetros de entrada y devuelve una lista de cursos
public record GetAllActiveCoursesQuery() : IRequest<IEnumerable<Course>>;

internal sealed class GetAllActiveCoursesQueryHandler : IRequestHandler<GetAllActiveCoursesQuery, IEnumerable<Course>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IBackgroundJobService _backgroundJobService;

    public GetAllActiveCoursesQueryHandler(ICourseRepository courseRepository, IBackgroundJobService backgroundJobService)
    {
        _courseRepository = courseRepository;
        _backgroundJobService = backgroundJobService;
    }

    public async Task<IEnumerable<Course>> Handle(GetAllActiveCoursesQuery request, CancellationToken cancellationToken)
    {
        var courses = await _courseRepository.GetAllActiveCoursesAsync();

        // Registra o actualiza una tarea que se ejecutará todos los días a la medianoche
        _backgroundJobService.AddOrUpdateRecurringNotification(
            "reporte-diario-cursos",
            "Sistema: Ejecutando verificación de inventario recurrente diaria de cursos activos.",
            "0 0 * * *"); 


        return courses;
    }
}