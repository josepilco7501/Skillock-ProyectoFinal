using Lab10.Domain2.Interfaces;
using Lab10.Domain2.Models;
using Lab10.Infrastructure2.Context;
using Microsoft.EntityFrameworkCore;

namespace Lab10.Infrastructure2.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly Lab10Context _context;

    public CourseRepository(Lab10Context context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Course>> GetAllActiveCoursesAsync()
    {
        return await _context.Courses.Where(c => c.IsActive == true).ToListAsync();
    }

    // ➕ Implementación del método para agregar a la base de datos
    public async Task<Course> AddAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
        await _context.SaveChangesAsync(); // Guarda los cambios de forma asíncrona en SQL
        return course;
    }

    // ➕ Implementación del método para buscar por ID
    public async Task<Course?> GetByIdAsync(int id)
    {
        return await _context.Courses.FirstOrDefaultAsync(c => c.Id == id);
    }
    
    
}