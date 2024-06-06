
public class RecieptItems
{
    public int Item_Id {get; set;}
    public int Reciept_Id {get; set;}
    public int quantity {get; set;}
    public decimal total_price {get; set;}

    public required Item Item { get; set; }
    public required Reciept Reciept { get; set; }
}


public class RecieptItemsDto
{
    public int Item_Id {get; set;}
    public int quantity {get; set;}
}
