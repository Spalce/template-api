using Template.Core.Models;

namespace Template.Core.Interfaces.Repositories;

public interface IContactRepository : IBaseCrud<Contact>
{
    Task<IEnumerable<Contact>> GetByStudentId(Guid? studentId);
}
