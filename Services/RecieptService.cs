public class RecieptService
{
    private readonly IRepository<Reciept> _recieptRepository;
    private readonly IRepository<Item> _itemRepository;
    private readonly RecieptItemsRepository _recieptItemsRepository;
    public RecieptService(IRepository<Reciept> recieptRepository, IRepository<Item> itemRepository, RecieptItemsRepository recieptItemsRepository)
    {
        _recieptRepository = recieptRepository;
        _itemRepository = itemRepository;
        _recieptItemsRepository = recieptItemsRepository;
    }

    // Adding New Reciept
    public async Task<Reciept?> CreateReciept()
    {
        Reciept reciept = new Reciept
        {
            date = DateTime.Now,
            total_amount = 0m,
            paid_amount = 0m,
            remaining = 0m
        };
        return await _recieptRepository.AddAsync(reciept);
    }

    public async Task UpdateTotalReciept(int id)
    {
        Reciept? reciept = await _recieptRepository.GetById(id);
        if (reciept is null)
            return;
        decimal total = await GetRecieptTotal(id);
        reciept.total_amount = total;

       await _recieptRepository.Update(reciept);
    }

    public async Task<bool> AddItemToReciept(string name, int recieptId, int quantity)
    {
        Item? item = _itemRepository.GetByPredicate(i => i.name == name);
        Reciept? reciept = await _recieptRepository.GetById(recieptId);
        if (reciept is null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: There is no reciept");
            Console.ResetColor(); 
            return false;
        }

        if (item is null)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: There is no item with this name");
            Console.ResetColor(); 
            return false;
        }

        if (quantity > item.stock)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: Item has {item.stock} in repository");
            Console.ResetColor(); 
            return false;
        }

        RecieptItems recieptItems = new RecieptItems
        {
            Item_Id = item.Id,
            Reciept_Id = recieptId,
            quantity = quantity,
            total_price = quantity * item.price,
            Reciept = reciept,
            Item = item
        };
        return await _recieptItemsRepository.AddItemToReciept(recieptItems);
    }

    private async Task<decimal> GetRecieptTotal(int id) =>
        await _recieptItemsRepository.GetRecieptTotal(id);

    public async Task MakePurchase(int id, decimal paid_amount)
    {
        decimal total = await GetRecieptTotal(id);
        if (paid_amount < total)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nError: You don't have enough money\n");
            Console.ResetColor();
            throw new Exception();
        }
        Reciept? reciept = await _recieptRepository.GetById(id);
        if (reciept is null)
            return;
        
        reciept.paid_amount = paid_amount;
        reciept.remaining = paid_amount - total;
        await _recieptRepository.Update(reciept);
    }

    public async Task ListAllReciepts()
    {
        var reciepts = await _recieptRepository.GetAllAsync();
        if (reciepts.Count is 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nNo Reciepts Yet.");
            Console.ResetColor();
            return;
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        foreach(var reciept in reciepts)
        {
            Console.WriteLine(reciept);
        }
        Console.ResetColor();
    }
}