using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Template.Core.Interfaces.Repositories;
using Template.Core.Models;
using Template.Infrastructure.Data;

namespace Template.Infrastructure.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly AppDbContext _db;
    private readonly IMapper _mapper;

    public StudentRepository(AppDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Student> GetById(Guid? id)
    {
        var record = await _db.Students.FindAsync(id);
        return record != null! ? _mapper.Map<Student>(record) : null!;
    }

    public async Task<IEnumerable<Student>> GetAll()
    {
        var record = await _db.Students.ToListAsync();
        return record != null! ? _mapper.Map<IEnumerable<Student>>(record) : null!;
    }

    public async Task<Student> Create(Student entity)
    {
        var record = _mapper.Map<Models.Student>(entity);
        record.DateCreated = DateTime.UtcNow;
        var created = await _db.Students.AddAsync(record);
        var result = await _db.SaveChangesAsync();
        return result > 0 ? _mapper.Map<Student>(created.Entity) : null!;
    }

    public async Task<bool> Update(Guid? id, Student entity)
    {
        var record = await _db.Students.FindAsync(id);
        if (record == null)
        {
            return false;
        }

        record.DateUpdated = DateTime.UtcNow;
        record.Name = entity.Name;
        record.MiddleMame = entity.MiddleMame;
        record.LastName = entity.LastName;
        record.DateOfBirth = entity.DateOfBirth;
        record.Hometown = entity.Hometown;
        record.IsActive = entity.IsActive;

        _db.Students.Update(record);
        var result = await _db.SaveChangesAsync();
        return result > 0;
    }

    public async Task<bool> Delete(Guid? id)
    {
        var record = await _db.Students.FindAsync(id);
        if (record == null)
        {
            return false;
        }

        _db.Students.Remove(record);
        var result = await _db.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Student> IsExist(Student entity)
    {
        var record = await _db.Students
            .FirstOrDefaultAsync(e =>
                e.Name == entity.Name && e.MiddleMame == entity.MiddleMame && e.LastName == entity.LastName &&
                e.DateOfBirth!.Value.Date == entity.DateOfBirth!.Value.Date);
        return record != null ? _mapper.Map<Student>(record) : null!;
    }

    public bool BatchDelete(IEnumerable<Student> entity)
    {
        var records = _mapper.Map<IEnumerable<Infrastructure.Models.Student>>(entity);
        _db.Students.RemoveRange(records);
        return true;
    }

    public bool BatchUpdate(IEnumerable<Student> entity)
    {
        var records = _mapper.Map<IEnumerable<Infrastructure.Models.Student>>(entity);
        _db.Students.UpdateRange(records);
        return true;
    }

    public async Task<bool> BatchCreate(IEnumerable<Student> entity)
    {
        var records = _mapper.Map<IEnumerable<Infrastructure.Models.Student>>(entity);
        await _db.Students.AddRangeAsync(records);
        return true;
    }
}
