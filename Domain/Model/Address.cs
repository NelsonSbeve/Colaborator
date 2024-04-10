namespace Domain.Model;

public class Address : IAddress
{
    public long Id;
    
    private string _street;
    public string Street
    {
        get { return _street; }
    }
    
    private string _postalCode;
    public string PostalCode
    {
        get { return _postalCode; }
    }


    public Address(string street, string postalCode)
    {
        _street = street;
        _postalCode = postalCode;
    }

    public bool UpdateStreet(string street)
    {
        // por agora, não tem regras de validação
        _street = street;

        return true;
    }

    public bool UpdatePostalCode(string postalCode)
    {
        // por agora, não tem regras de validação
        _postalCode = postalCode;

        return true;
    }
}
