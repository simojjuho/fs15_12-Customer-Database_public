using src.CustomerDB.CustomerDB;

namespace Name;

public class Program
{
    public static void Main(string[] args)
    {
        var database = new CustomerDatabase();
        
        Customer cust1 = new Customer("123", "Juho", "Simojoki", "juho@mail.com", "address 123 a 4, 33100 Tampere");
        Customer cust2 = new Customer("234", "Urho", "Kekkonen", "urkki@mail.com", "address 123 a 4, 00100 Helsinki");
        Customer cust3 = new Customer("345", "Mauno", "Koivisto", "manu@mail.com", "address 123 a 4, 00120 Helsinki");
        Customer cust4 = new Customer("456", "Martti", "Ahtisaari", "mara@mail.com", "address 123 a 4, 90100 Oulu");
        Customer cust5 = new Customer("567", "Kyösti", "Kallio", "kalliok@mail.com", "address 123 a 4, 20100 Turku");
        Customer cust6 = new Customer("678", "Tarja", "Halonen", "tarja_h@mail.com", "address 123 a 4, 00300 Helsinki");

        database.AddNew(cust1);
        Console.WriteLine("Undo stack size after first add: " + database.UndoStack);
        Console.WriteLine("Redo stack size after first add: " + database.RedoStack);
        database.AddNew(cust2);
        Console.WriteLine("Undo stack size after second add: " + database.UndoStack);
        Console.WriteLine("Redo stack size after second add: " + database.RedoStack);
        database.AddNew(cust3);
        Console.WriteLine("Undo stack size after third add: " + database.UndoStack);
        Console.WriteLine("Redo stack size after third add: " + database.RedoStack);
        database.Undo();
        Console.WriteLine("Undo stack size after first undo: " + database.UndoStack);
        Console.WriteLine("Redo stack size after first undo: " + database.RedoStack);
        database.Undo();
        Console.WriteLine("Undo stack size after second undo: " + database.UndoStack);
        Console.WriteLine("Redo stack size after second undo: " + database.RedoStack);
        Console.WriteLine(database);
        database.Redo();
        Console.WriteLine("Undo stack size: " + database.UndoStack);
        Console.WriteLine(database);
        database.Undo();
        Console.WriteLine("Undo stack size: " + database.UndoStack);
        Console.WriteLine("Redo stack size: " + database.RedoStack);
        Console.WriteLine(database);
        try
        {
            database.Redo();
            Console.WriteLine(database);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            Console.WriteLine("Redo stack size: " + database.RedoStack);
        }
    }    
}
