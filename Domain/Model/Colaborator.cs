namespace Domain.Model;

using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

public class Colaborator : IColaborator
{
	
	[Key]
	public long Id {get; set;}
    private string _strEmail;
	public string Email
	{
		get { return _strEmail; }
	}

    private string _strName;
	public string Name
	{
		get { return _strName; }
	}

	private Address _address;
	public Address Address
	{
		get { return _address; }
	}

	private Colaborator() {}

    public Colaborator(string strName, string strEmail, string street, string postalCode) {

		if ( IsValidParameters(strName, strEmail) ) {
			_strName = strName;
			_strEmail = strEmail;

            _address = new Address( street, postalCode);
        }
		else {
			throw new ArgumentException("Invalid arguments: " + strName + ", " + strEmail);
		}
	}

	private bool IsValidParameters(string strName, string strEmail) {

		if( strName==null ||
			strName.Length > 50 ||
			string.IsNullOrWhiteSpace(strName) ||
			ContainsAny(strName, ["0", "1", "2", "3", "4", "5", "6", "7", "8", "9"]))
			return false;

		if( !IsValidEmailAddres( strEmail ) )
			return false;
		
		return true;
	}

	private bool ContainsAny(string stringToCheck, params string[] parameters)
	{
		return parameters.Any(parameter => stringToCheck.Contains(parameter));
	}

	// from https://mailtrap.io/blog/validate-email-address-c/
	private bool IsValidEmailAddres(string email)
	{
		var valid = true;

		try
		{
			var emailAddress = new MailAddress(email);
		}
		catch
		{
			valid = false;
		}

		return valid;
	}

	public bool UpdateAddress(string street, string postalCode)
	{
		bool bStreet = _address.UpdateStreet(street);
		bool bPostalCode = _address.UpdatePostalCode(postalCode);

		return bStreet && bPostalCode;
	}

	// public long getId()
	// {
	// 	return id;
	// }

	public string GetName() {
		return _strName;
	}

	public void SetName(string strName)
	{
		_strName = strName;
	}

	public string GetEmail()
	{
		return _strEmail;
	}

	public string GetStreet()
	{
		return _address.Street;
	}

	public string GetPostalCode()
	{
		return _address.PostalCode;
	}

    public long GetId()
    {
       return Id;
    }

    public Address GetAdress()
    {
        return Address;
    }
}
