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

    public GetCourseByIdQueryHandler(ICourseRepository courseRepository)
    {
        _courseRepository = courseRepository;
    }

    public async Task<Course?> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
    {
        var course = await _courseRepository.GetByIdAsync(request.CourseId);
        
        // Si no existe, manejamos la regla devolviendo null (o lanzando excepción según prefieras)
        if (course == null)
        {
            return null;
        }
        
        return course;
    }
}