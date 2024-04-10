namespace DataModel.Repository;

using Microsoft.EntityFrameworkCore;

using DataModel.Model;
using DataModel.Mapper;

using Domain.Model;
using Domain.IRepository;
using Microsoft.EntityFrameworkCore.ChangeTracking;

public class ColaboratorRepository : GenericRepository<Colaborator>, IColaboratorRepository
{    
    ColaboratorMapper _colaboratorMapper;
    public ColaboratorRepository(AbsanteeContext context, ColaboratorMapper mapper) : base(context!)
    {
        _colaboratorMapper = mapper;
    }

    public async Task<IEnumerable<Colaborator>> GetColaboratorsAsync()
    {
        try {
            IEnumerable<ColaboratorDataModel> colaboratorsDataModel = await _context.Set<ColaboratorDataModel>()
                    .Include(c => c.Address)
                    .ToListAsync();

            IEnumerable<Colaborator> colaborators = _colaboratorMapper.ToDomain(colaboratorsDataModel);

            return colaborators;
        }
        catch
        {
            throw;
        }
    }

    public async Task<Colaborator> GetColaboratorByEmailAsync(string email)
    {
        try {
            ColaboratorDataModel colaboratorDataModel = await _context.Set<ColaboratorDataModel>()
                    .Include(c => c.Address)
                    .FirstAsync(c => c.Email == email);

            Colaborator colaborator = _colaboratorMapper.ToDomain(colaboratorDataModel);

            return colaborator;
        }
        catch
        {
            return null;throw;
        }
    }

    public async Task<Colaborator> GetColaboratorByIdAsync(long id)
    {
        try {
            ColaboratorDataModel colaboratorDataModel = await _context.Set<ColaboratorDataModel>()
                    .Include(c => c.Address)
                    .FirstAsync(c => c.Id==id);

            Colaborator colaborator = _colaboratorMapper.ToDomain(colaboratorDataModel);

            return colaborator;
        }
        catch
        {
            return null;//throw;
        }
    }

    public async Task<Colaborator> GetColaboratorByNameAsync(string name)
    {
        try {
            ColaboratorDataModel colaboratorDataModel = await _context.Set<ColaboratorDataModel>()
                    .FirstAsync(c => c.Name==name);

            Colaborator colaborator = _colaboratorMapper.ToDomain(colaboratorDataModel);

            return colaborator;
        }
        catch
        {
            throw;
        }
    }

    public async Task<Colaborator> Add(Colaborator colaborator)
    {
        try {
            ColaboratorDataModel colaboratorDataModel = _colaboratorMapper.ToDataModel(colaborator);

            EntityEntry<ColaboratorDataModel> colaboratorDataModelEntityEntry = _context.Set<ColaboratorDataModel>().Add(colaboratorDataModel);
            
            await _context.SaveChangesAsync();

            ColaboratorDataModel colaboratorDataModelSaved = colaboratorDataModelEntityEntry.Entity;

            Colaborator colaboratorSaved = _colaboratorMapper.ToDomain(colaboratorDataModelSaved);

            return colaboratorSaved;    
        }
        catch
        {
            throw;
        }
    }

    public async Task<Colaborator> Update(Colaborator colaborator, List<string> errorMessages)
    {
        try {
            ColaboratorDataModel colaboratorDataModel = await _context.Set<ColaboratorDataModel>()
                    .Include(c => c.Address)
                    .FirstAsync(c => c.Email==colaborator.Email);

            _colaboratorMapper.UpdateDataModel(colaboratorDataModel, colaborator);

            _context.Entry(colaboratorDataModel).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return colaborator;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await ColaboratorExists(colaborator.Email))
            {
                errorMessages.Add("Not found");
                
                return null;
            }
            else
            {
                throw;
            }

            return null;
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> ColaboratorExists(string email)
    {
        return await _context.Set<ColaboratorDataModel>().AnyAsync(e => e.Email == email);
    }


}