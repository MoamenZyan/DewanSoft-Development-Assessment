using Microsoft.VisualBasic;

public static class TablesSchema
{
    public static string ItemTable = @"CREATE TABLE IF NOT EXISTS Item (
        Id INT AUTO_INCREMENT PRIMARY KEY,
        name TEXT,
        price DECIMAL(10, 2),
        stock INT)";

    public static string RecieptTable = @"CREATE TABLE IF NOT EXISTS Reciept (
        Id INT AUTO_INCREMENT PRIMARY KEY,
        date DateTime,
        total_amount DECIMAL(10, 2),
        paid_amount DECIMAL(10, 2),
        remaining DECIMAL(10, 2)
    )";
    public static string Reciept_Items = @"CREATE TABLE IF NOT EXISTS Reciept_Items (
        Item_Id INT,
        Reciept_Id INT,
        quantity INT,
        total_price DECIMAL(10, 2),
        PRIMARY KEY(Item_Id, Reciept_Id),
        FOREIGN KEY (Item_Id) REFERENCES Item(Id),
        FOREIGN KEY (Reciept_Id) REFERENCES Reciept(Id)
    )";
}