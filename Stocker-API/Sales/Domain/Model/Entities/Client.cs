using Stocker_API.Sales.Domain.Model.Aggregates;

namespace Stocker_API.Sales.Domain.Model.Entities;

public partial class Client
{
    public Client()
    {
        Name = string.Empty;
        Number = string.Empty;
        Email = string.Empty;
    }


    public Client(string name, string number, string email)
    {
        Name = name;
        Number = number;
        Email = email;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string Email { get; set; }
    public ICollection<Sale> Sales { get; }
}