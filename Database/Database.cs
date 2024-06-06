using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Data.SqlClient;
using MySql;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;

// I will use ADO.NET

public static class Database
{
    public static void CreateTables()
    {
        var Configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

        using (var connection = new MySqlConnection(Configuration.GetSection("ConnectionStrings").Value))
        {
            try
            {
                connection.Open();
                string commands = $@"
                {TablesSchema.ItemTable};
                {TablesSchema.RecieptTable};
                {TablesSchema.Reciept_Items};";
                MySqlCommand command = new MySqlCommand(commands, connection);
                command.ExecuteNonQuery();
                Console.WriteLine("Initialized Database Tables");
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
            }
        }
    }
}