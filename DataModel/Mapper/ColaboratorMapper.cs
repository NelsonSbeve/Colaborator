namespace DataModel.Mapper;

using DataModel.Model;

using Domain.Model;
using Domain.Factory;

public class ColaboratorMapper
{
    private IColaboratorFactory _colaboratorFactory;

    public ColaboratorMapper(IColaboratorFactory colaboratorFactory)
    {
        _colaboratorFactory = colaboratorFactory;
    }

    public Colaborator ToDomain(ColaboratorDataModel colabDM)
    {
        Colaborator colabDomain = _colaboratorFactory.NewColaborator(colabDM.Name, colabDM.Email, colabDM.Address.Street, colabDM.Address.PostalCode);

        colabDomain.Id = colabDM.Id;

        return colabDomain;
    }

    public IEnumerable<Colaborator> ToDomain(IEnumerable<ColaboratorDataModel> colabsDataModel)
    {

        List<Colaborator> colabsDomain = new List<Colaborator>();

        foreach(ColaboratorDataModel colabDataModel in colabsDataModel)
        {
            Colaborator colabDomain = ToDomain(colabDataModel);

            colabsDomain.Add(colabDomain);
        }

        return colabsDomain.AsEnumerable();
    }

    public ColaboratorDataModel ToDataModel(Colaborator colab)
    {
        ColaboratorDataModel colabDataModel = new ColaboratorDataModel(colab);

        return colabDataModel;
    }


    public bool UpdateDataModel(ColaboratorDataModel colaboratorDataModel, Colaborator colaboratorDomain)
    {
        colaboratorDataModel.Name = colaboratorDomain.GetName();

        // pode ser necessário mais atualizações, e com isso o retorno não ser sempre true
        // contudo, porque colaboratorDataModel está a ser gerido pelo DbContext, para atualizarmos a DB, é este que tem de ser alterado, e não criar um novo

        colaboratorDataModel.Address.Street = colaboratorDomain.GetStreet();
        colaboratorDataModel.Address.PostalCode = colaboratorDomain.GetPostalCode();
        return true;
    }

}