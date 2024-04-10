namespace Application.Services;

using Domain.Model;
using Application.DTO;

using Microsoft.EntityFrameworkCore;
using Domain.IRepository;

public class ColaboratorService {

    private readonly IColaboratorRepository _colaboratorRepository;
    
    public ColaboratorService(IColaboratorRepository colaboratorRepository) {
        _colaboratorRepository = colaboratorRepository;
    }

    public async Task<IEnumerable<ColaboratorDTO>> GetAllWithAddress()
    {    
        IEnumerable<Colaborator> colabs = await _colaboratorRepository.GetColaboratorsAsync();

        //IEnumerable<Colaborator> colabs2 = _colaboratorRepository.GetAll();

        IEnumerable<ColaboratorDTO> colabsDTO = ColaboratorDTO.ToDTO(colabs);

        return colabsDTO;
    }

    public async Task<ColaboratorDTO> GetByIdWithAddress(long id)
    {    
        Colaborator colaborator = await _colaboratorRepository.GetColaboratorByIdAsync(id);

        if(colaborator!=null)
        {
            ColaboratorDTO colabDTO = ColaboratorDTO.ToDTO(colaborator);
            return colabDTO;
        }
        return null;
    }

    public async Task<ColaboratorDTO> GetByName(string name)
    {    
        Colaborator colaborator = await _colaboratorRepository.GetColaboratorByNameAsync(name);

        ColaboratorDTO colabDTO = ColaboratorDTO.ToDTO(colaborator);

        return colabDTO;
    }

    public async Task<ColaboratorDTO> GetByEmailWithAddress(string strEmail)
    {    
        Colaborator colaborator =  await _colaboratorRepository.GetColaboratorByEmailAsync(strEmail);

        if(colaborator!=null)
        {
            ColaboratorDTO colabDTO = ColaboratorDTO.ToDTO(colaborator);
            return colabDTO;
        }
        return null;
    }

    public async Task<ColaboratorDTO> Add(ColaboratorDTO colaboratorDTO, List<string> errorMessages)
    {
        bool bExists = await _colaboratorRepository.ColaboratorExists(colaboratorDTO.Email);
        if(bExists) {
            errorMessages.Add("Already exists");
            return null;
        }

        Colaborator colaborator = ColaboratorDTO.ToDomain(colaboratorDTO);

        Colaborator colaboratorSaved = await _colaboratorRepository.Add(colaborator);

        ColaboratorDTO colabDTO = ColaboratorDTO.ToDTO(colaboratorSaved);

        return colabDTO;
    }

    public async Task<bool> Update(string email, ColaboratorDTO colaboratorDTO, List<string> errorMessages)
    {
        Colaborator colaborator = await _colaboratorRepository.GetColaboratorByEmailAsync(email);

        if(colaborator!=null)
        {
            ColaboratorDTO.UpdateToDomain(colaborator, colaboratorDTO);

            await _colaboratorRepository.Update(colaborator, errorMessages);

            return true;
        }
        else
        {
            errorMessages.Add("Not found");

            return false;
        }
    }
}