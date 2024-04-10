namespace Domain.Factory;

using Domain.Model;

public class ColaboratorFactory : IColaboratorFactory
{
    public Colaborator NewColaborator(string strName, string strEmail, string strStreet, string strPostalCode)
    {
        return new Colaborator(strName, strEmail, strStreet, strPostalCode);
    }
}