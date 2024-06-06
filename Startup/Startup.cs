public static class Startup
{
    private static IServiceProvider? _serviceProvider;
    public static async Task Run(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        Console.Clear();
        Console.WriteLine("Welcome To Our System !");
        while (true)
        {
            ItemService itemService = (ItemService)_serviceProvider.GetService(typeof(ItemService))!;
            RecieptService recieptService = (RecieptService)_serviceProvider.GetService(typeof(RecieptService))!;
            Console.WriteLine("1) Create Item");
            Console.WriteLine("2) Create Reciept");
            Console.WriteLine("3) List All Items");
            Console.WriteLine("4) List All Reciepts");
            Console.WriteLine("5) Exit Program");
            var choice1 = Convert.ToInt32(Console.ReadLine());
            if (choice1 == 1)
            {
                Console.Write("Item name: ");
                var name = Console.ReadLine();
                if (name is null)
                    continue;
                Console.Write("Item price: ");
                var price = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Item stock: ");
                var stock = Convert.ToInt32(Console.ReadLine());

                var result = await itemService.CreateItem(name!, stock, price);
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                if (result != null)
                    Console.WriteLine("Item Created !!");
                Console.ResetColor(); 
                Console.WriteLine();
            }
            else if(choice1 == 2)
            {
                Reciept? reciept = await recieptService.CreateReciept();
                if (reciept is null)
                    continue;
                while (true)
                {
                    Console.WriteLine("1) add item to reciept");
                    Console.WriteLine("2) enter amount to pay");
                    var choice2 = Convert.ToInt16(Console.ReadLine());
                    if (choice2 == 1)
                    {
                        Console.Write("Item name: ");
                        var name = Console.ReadLine();
                        if (name is null)
                            continue;
                        Console.Write("Item quantity: ");
                        var quantity = Convert.ToInt32(Console.ReadLine());
                        var result = await recieptService.AddItemToReciept(name, reciept.Id, quantity);
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Green;
                        if (result == true)
                            Console.WriteLine("Item added successfully");
                        Console.ResetColor(); 
                        Console.WriteLine();
                        await recieptService.UpdateTotalReciept(reciept.Id);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine(reciept);
                        Console.ResetColor(); 
                    }
                    else if (choice2 == 2)
                    {
                        Console.Write("Enter Amount: ");
                        decimal paid_amount = Convert.ToDecimal(Console.ReadLine());
                        var context = (ApplicationDatabaseContext)serviceProvider.GetService(typeof(ApplicationDatabaseContext))!;
                        TransactionService transaction = new TransactionService(context, recieptService, itemService);
                        var result = await transaction.ExecuteTransaction(reciept.Id, paid_amount);
                        if (result == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(reciept);
                            Console.ResetColor(); 
                            break;
                        }
                    }
                }
            }
            else if (choice1 == 3)
            {
                await itemService.ListAllItems();
            }
            else if (choice1 == 4)
            {
                await recieptService.ListAllReciepts();
            }
            else if (choice1 == 5)
            {
                Console.WriteLine("Bye Bye !");
                break;
            }
            Console.WriteLine();
        }
    }
}