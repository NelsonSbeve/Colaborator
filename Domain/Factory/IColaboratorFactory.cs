namespace Domain.Factory;

using Domain.Model;

public interface IColaboratorFactory
{
    Colaborator NewColaborator(string strName, string strEmail, string strStreet, string strPostalCode);
}