namespace Domain.Model;

public interface IColaborator 
{
	//long GetId();
	string GetEmail();
	string GetName();
	string GetStreet();
	string GetPostalCode();
    long GetId();
    Address GetAdress();
}