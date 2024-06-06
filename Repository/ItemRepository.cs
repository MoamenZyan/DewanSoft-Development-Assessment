

using Microsoft.EntityFrameworkCore;

public class ItemRepository : IRepository<Item>
{
    private readonly ApplicationDatabaseContext _context;

    public ItemRepository(ApplicationDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Item?> AddAsync(Item item)
    {
        try
        {
            await _context.Item.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp.Message);
            return null;
        }
    }

    public async Task<bool> Delete(Item item)
    {
        try
        {
            _context.Item.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp.Message);
            return false;
        }
    }

    public async Task<List<Item>> GetAllAsync() =>
        await _context.Item.ToListAsync();


    public async Task<Item?> GetById(int id) =>
        await _context.Item.FirstOrDefaultAsync(r => r.Id == id);


    public Item? GetByPredicate(Func<Item, bool> func) =>
        _context.Item.Where(func).FirstOrDefault();

    public async Task Update(Item item)
    {
        _context.Item.Update(item);
        await _context.SaveChangesAsync();
    }



}