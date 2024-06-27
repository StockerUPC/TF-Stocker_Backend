using Stocker_API.Purchases.Domain.Model.Aggregates;

namespace Stocker_API.Purchases.Domain.Model.Entities;

public partial class Supplier
{
    public Supplier()
    {
        Name = string.Empty;
        Number = string.Empty;
        Email = string.Empty;
    }


    public Supplier(string name, string number, string email)
    {
        Name = name;
        Number = number;
        Email = email;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Number { get; set; }
    public string Email { get; set; }
    public ICollection<Purchase> Purchases { get; }
}