using Lab10.Domain2.Models;

namespace Lab10.Domain2.Interfaces;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllActiveCoursesAsync();
    
    // ➕ Agrega estos dos métodos obligatorios para tus casos de uso:
    Task<Course> AddAsync(Course course);
    Task<Course?> GetByIdAsync(int id);
}