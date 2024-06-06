using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Initialize Database Tables
Database.CreateTables();

// loading appsettings
var Configuration = new ConfigurationBuilder()
.AddJsonFile("appsettings.json")
.Build();

// Adding Services
var service = new ServiceCollection();
service.AddDbContext<ApplicationDatabaseContext>(options => 
options.UseMySQL(Configuration.GetSection("ConnectionStrings").Value!));
service.AddTransient<IRepository<Item>, ItemRepository>();
service.AddTransient<IRepository<Reciept>, RecieptRepository>();
service.AddTransient<ItemService>();
service.AddTransient<RecieptService>();
service.AddTransient<RecieptItemsRepository>();

ServiceProvider serviceProvider = service.BuildServiceProvider();

await Startup.Run(serviceProvider);
