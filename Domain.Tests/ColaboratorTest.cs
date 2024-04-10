namespace Domain.Tests;

using Domain.Model;

public class ColaboradorTest
{
    [Theory]
    [InlineData("Catarina Moreira", "catarinamoreira@email.pt", "street", "4000-000")]
    [InlineData("a", "catarinamoreira@email.pt", "street", "4000-000")]
    [InlineData("kasdjflkadjf lkasdfj laksdjf alkdsfjv alkdsfjv asl", "catarinamoreira@email.pt", "street", "4000-000")]
    public void WhenPassingCorrectData_ThenColaboradorIsInstantiated(string strName, string strEmail, string strStreet, string strPostalCode)
    {
        new Colaborator( strName, strEmail, strStreet, strEmail);
    }

    

    [Theory]
    [InlineData("", "catarinamoreira@email.pt", "street", "4000-000")]
    [InlineData("abasdfsc 12", "catarinamoreira@email.pt", "street", "4000-000")]
    [InlineData("       \r\n \t ", "catarinamoreira@email.pt", "street", "4000-000")]
    [InlineData("kasdjflkadjf lkasdfj laksdjf alkdsfjv alkdsfjv asldkfj asldkfvj asdlkvj asdlkfvj asdlkfvj asdflkfvj asfldkjfv jasdflkvj lasf", "catarinamoreira@email.pt", "street", "4000-000")]
    [InlineData(null, "catarinamoreira@email.pt", "street", "4000-000")]
    public void WhenPassingInvalidName_ThenThrowsException(string strName, string strEmail, string strStreet, string strPostalCode)
    {
        // arrange

        // assert
        var ex = Assert.Throws<ArgumentException>(() =>
        
            // act
            new Colaborator(strName, strEmail, strStreet, strPostalCode)
        );
        Assert.Equal(ex.Message, "Invalid arguments: " + strName + ", " + strEmail);
    }

    [Theory]
    [InlineData("Catarina Moreira", "", "street", "4000-000")]
    [InlineData("Catarina Moreira", null, "street", "4000-000")]
    [InlineData("Catarina Moreira", "catarinamoreira.pt", "street", "4000-000")]
    public void WhenPassingInvalidEmail_ThenThrowsException(string strName, string strEmail, string strStreet, string strPostalCode)
    {
        // assert
        Assert.Throws<ArgumentException>(() =>
        
            // act
            new Colaborator(strName, strEmail, strStreet, strPostalCode)
        );
    }

    [Theory]
    [InlineData("", "", "street", "4000-000")]
    [InlineData(null, null, "street", "4000-000")]
    [InlineData("", null, "street", "4000-000")]
    [InlineData(null, "", "street", "4000-000")]
    [InlineData("", "catarinamoreira.pt", "street", "4000-000")]
    public void WhenPassingInvalidNameAndInvalidEmail_Thenz(string strName, string strEmail, string strStreet, string strPostalCode)
    {
        Assert.Throws<ArgumentException>(() => new Colaborator(strName, strEmail, strStreet, strPostalCode));
    }

    [Fact]
    public void WhenPassingName_ShouldGetAttributes()
    {
        // assert
        string nameExpected = "Catarina Moreira";
        var colaborator = new Colaborator("Catarina Moreira", "catarinamoreira@email.pt", "street", "4000-000");

        // act
        string GetName = colaborator.GetName();
        string Name = colaborator.Name;

        string GetEmail = colaborator.GetEmail();
        string Email = colaborator.Email;

        string GetStreet = colaborator.GetStreet();
        
        string GetPostalCode = colaborator.GetPostalCode();

        Address address = colaborator.Address;

        //assert
        Assert.Equal("Catarina Moreira", GetName);
        Assert.Equal("Catarina Moreira", Name);

        Assert.Equal("catarinamoreira@email.pt", GetEmail);
        Assert.Equal("catarinamoreira@email.pt", Email);

        Assert.Equal("street", GetStreet);
        Assert.Equal("street", address.Street);

        Assert.Equal("4000-000", GetPostalCode);
        Assert.Equal("4000-000", address.PostalCode);
    }
}