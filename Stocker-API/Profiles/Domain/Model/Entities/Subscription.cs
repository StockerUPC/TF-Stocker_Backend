using Stocker_API.Profiles.Domain.Model.Aggregates;

namespace Stocker_API.Profiles.Domain.Model.Entities;

public partial class Subscription
{
    public Subscription()
    {
        Name = string.Empty;
        MonthlyPrice = decimal.Zero;
    }


    public Subscription(string name, decimal monthlyPrice)
    {
        Name = name;
        MonthlyPrice = monthlyPrice;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal MonthlyPrice { get; set; }
    
    public ICollection<Profile> Profiles { get; }
}