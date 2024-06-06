using System.Data;
using Microsoft.EntityFrameworkCore.Storage;

public class TransactionService
{
    private readonly ApplicationDatabaseContext _context;
    private readonly RecieptService _recieptService;
    private readonly ItemService _itemService;


    public TransactionService(ApplicationDatabaseContext context, RecieptService recieptService, ItemService itemService)
    {
        _context = context;
        _recieptService = recieptService;
        _itemService = itemService;
    }

    // Execute Payment Transaction
    public async Task<bool> ExecuteTransaction(int recieptId, decimal paid_amount)
    {
        using (IDbContextTransaction transaction = await _context.Database.BeginTransactionAsync())
        {
            try
            {
                await _recieptService.MakePurchase(recieptId, paid_amount);
                await _itemService.UpdateItemsStock(recieptId);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nPurchase Done Successfully\n");
                Console.ResetColor();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return false;
            }
        }
    }
}