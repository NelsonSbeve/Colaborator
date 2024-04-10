namespace DataModel.Model;

using Domain.Model;

public class AddressDataModel
{
    public long Id {get; set;}
    public string Street {get;set;}
    public string PostalCode {get;set;}

    public AddressDataModel() {}
    
    public AddressDataModel(Address address)
    {
        Id = address.Id;
        Street = address.Street;
        PostalCode = address.PostalCode;
    }
}
