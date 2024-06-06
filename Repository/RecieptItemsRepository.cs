using Microsoft.EntityFrameworkCore;

public class RecieptItemsRepository
{
    private readonly ApplicationDatabaseContext _context;
    public RecieptItemsRepository(ApplicationDatabaseContext context)
    {
        _context = context;
    }

    // Adding New Reciept
    public async Task<bool> AddItemToReciept(RecieptItems recieptItems)
    {
        await _context.Reciept_Items.AddAsync(recieptItems);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<decimal> GetRecieptTotal(int id)
    {
        decimal total = await _context.Reciept_Items.Where(r => r.Reciept_Id == id).SumAsync(s => s.total_price);
        return total;
    }

    public async Task<List<RecieptItemsDto>> GetItemsFromReciept(int id)
    {
        var results = await _context.Reciept_Items.Where(r => r.Reciept_Id == id).Select(i =>
        new RecieptItemsDto
        {Item_Id = i.Item_Id,
        quantity = i.quantity
        }).ToListAsync();

        return results;
    }
}