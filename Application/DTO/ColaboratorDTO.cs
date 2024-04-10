namespace Application.DTO;

using DataModel.Mapper;
using DataModel.Repository;
using Domain.IRepository;
using Domain.Model;

public class ColaboratorDTO
{
	
	public long Id { get; set; }
	public string Email { get; set; }
	public string Name { get; set; }
	public string Street { get; set; }
	public string PostalCode { get; set; }
	
	public ColaboratorDTO() {
	}

	public ColaboratorDTO(long id, string strName, string strEmail, string strStreet, string strPostalCode)
	{
		Id = id;
		Name = strName;
		Email = strEmail;
		Street = strStreet;
		PostalCode = strPostalCode;
	}

	static public ColaboratorDTO ToDTO(Colaborator colab) {

		ColaboratorDTO colabDTO = new ColaboratorDTO(colab.GetId(), colab.GetName(), colab.GetEmail(), colab.GetStreet(), colab.GetPostalCode() );

		return colabDTO;
	}

	static public IEnumerable<ColaboratorDTO> ToDTO(IEnumerable<Colaborator> colabs)
	{
		List<ColaboratorDTO> colabsDTO = new List<ColaboratorDTO>();

		foreach( Colaborator colab in colabs ) {
			ColaboratorDTO colabDTO = ToDTO(colab);

			colabsDTO.Add(colabDTO);
		}

		return colabsDTO;
	}

	static public Colaborator ToDomain(ColaboratorDTO colabDTO) {
		
		Colaborator colab = new Colaborator(colabDTO.Name, colabDTO.Email, colabDTO.Street, colabDTO.PostalCode);

		return colab;
	}


	static public Colaborator UpdateToDomain(Colaborator colab, ColaboratorDTO colabDTO) {

		// não se permite alterar o email
		//colab.Email = colabDTO.strEmail;
		
		colab.SetName(colabDTO.Name);

		colab.UpdateAddress(colabDTO.Street, colabDTO.PostalCode);

		return colab;

	}
}