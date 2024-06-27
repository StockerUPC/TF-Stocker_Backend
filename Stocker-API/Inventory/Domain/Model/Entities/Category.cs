using Stocker_API.Inventory.Domain.Model.Aggregates;

namespace Stocker_API.Inventory.Domain.Model.Entities;

public partial class Category
{
    public Category()
    {
        Name = string.Empty;
    }


    public Category(string name)
    {
        Name = name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    
    public ICollection<Product> Products { get; }
}