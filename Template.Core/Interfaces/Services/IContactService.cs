using Template.Core.Models;

namespace Template.Core.Interfaces.Services;

public interface IContactService : IBaseCrud<Contact>
{
    Task<IEnumerable<Contact>> GetByStudentId(Guid? studentId);
}
