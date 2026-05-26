using MediatR;
using Lab10.Domain2.Interfaces;
using Lab10.Domain2.Models;

namespace Lab10.Application2.UseCases.CourseUseCase.Querys;

// La consulta no necesita parámetros de entrada y devuelve una lista de cursos
public record GetAllActiveCoursesQuery() : IRequest<IEnumerable<Course>>;

internal sealed class GetAllActiveCoursesQueryHandler : IRequestHandler<GetAllActiveCoursesQuery, IEnumerable<Course>>
{
    private readonly ICourseRepository _courseRepository;

    public GetAllActiveCoursesQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<IEnumerable<Course>> Handle(GetAllActiveCoursesQuery request, CancellationToken cancellationToken)
    {
        // Invoca al método original que ya tenías en tu repositorio de Dominio
        return await _courseRepository.GetAllActiveCoursesAsync();
    }
}