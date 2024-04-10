using Domain.IRepository;

namespace UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository Generic { get; }

    IColaboratorRepository ColaboratorRepository { get; }

    Task<int> CompleteAsync();
}
