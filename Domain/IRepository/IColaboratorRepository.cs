namespace Domain.IRepository;

using Domain.Model;

public interface IColaboratorRepository : IGenericRepository<Colaborator>
{
    Task<IEnumerable<Colaborator>> GetColaboratorsAsync();

    Task<Colaborator> GetColaboratorByEmailAsync(string email);

    Task<Colaborator> GetColaboratorByIdAsync(long id);
    Task<Colaborator> GetColaboratorByNameAsync(string name);

    Task<Colaborator> Add(Colaborator colaborator);

    Task<Colaborator> Update(Colaborator colaborator, List<string> errorMessages);

    Task<bool> ColaboratorExists(string email);
    

}
