public class ItemService
{
    private readonly IRepository<Item> _itemRepository;
    private readonly RecieptItemsRepository _recieptItemsRepository;
    public ItemService(IRepository<Item> itemRepository, RecieptItemsRepository recieptItemsRepository)
    {
        _itemRepository = itemRepository;
        _recieptItemsRepository = recieptItemsRepository;
    }

    public async Task<Item?> CreateItem(string name, int stock, decimal price)
    {
        Item item = new Item
        {
            name = name,
            stock = stock,
            price = price
        };
        return await _itemRepository.AddAsync(item);
    }

    public async Task UpdateItemsStock(int id)
    {
       List<RecieptItemsDto> items = await _recieptItemsRepository.GetItemsFromReciept(id);
       foreach(RecieptItemsDto i in items)
       {
            Item? item = await _itemRepository.GetById(i.Item_Id);
            if (item is null)
                continue;
            
            item.stock -= i.quantity;
            await _itemRepository.Update(item);
       }
    }

    public async Task ListAllItems()
    {
        var items = await _itemRepository.GetAllAsync();
        if (items.Count is 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nNo Items Yet.");
            Console.ResetColor();
            return;
        }
        Console.ForegroundColor = ConsoleColor.Yellow;
        foreach(var item in items)
        {
            Console.WriteLine(item);
        }
        Console.ResetColor();
    }
}