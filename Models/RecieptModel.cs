using System.ComponentModel.DataAnnotations;


public class Reciept
{
    [Key]
    public int Id {get; set;}
    public DateTime date {get; set;}
    public decimal total_amount {get; set;}
    public decimal paid_amount {get; set;}
    public decimal remaining {get; set;}

    public override string ToString()
    {
        return $"\n---------- Reciept ----------\n" +
                $"Reciept Date: {date}\n"+
                $"Reciept Total: {total_amount}$\n" +
                $"Reciept Paid Amount: {paid_amount}$\n" +
                $"Reciept Remaining Amount: {remaining}\n";
    }
}