using System.ComponentModel.DataAnnotations;

public class Item
{
    [Key]
    public int Id {get; set;}
    public required string name {get; set;}
    public decimal price {get; set;}
    public int stock {get; set;}

    public override string ToString()
    {
        return "\n-----------------------\n" +
            $"Item Name: {name}\n" +
            $"Item Price: {price}$\n" +
            $"Item Stock: {stock}\n";
    }
}
