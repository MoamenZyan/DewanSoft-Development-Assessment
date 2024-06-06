

using Microsoft.EntityFrameworkCore;

public class RecieptRepository : IRepository<Reciept>
{
    private readonly ApplicationDatabaseContext _context;

    public RecieptRepository(ApplicationDatabaseContext context)
    {
        _context = context;
    }

    public async Task<Reciept?> AddAsync(Reciept reciept)
    {
        try
        {
            await _context.Reciept.AddAsync(reciept);
            await _context.SaveChangesAsync();
            return reciept;
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp.Message);
            return null;
        }
    }

    public async Task<bool> Delete(Reciept Reciept)
    {
        try
        {
            _context.Reciept.Remove(Reciept);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception exp)
        {
            Console.WriteLine(exp.Message);
            return false;
        }
    }

    public async Task<List<Reciept>> GetAllAsync() =>
        await _context.Reciept.ToListAsync();


    public async Task<Reciept?> GetById(int id) =>
        await _context.Reciept.FirstOrDefaultAsync(r => r.Id == id);


    public Reciept GetByPredicate(Func<Reciept, bool> func) =>
        _context.Reciept.Where(func).First();


    public async Task Update(Reciept reciept)
    {
        _context.Reciept.Update(reciept);
        await _context.SaveChangesAsync();
    }
}