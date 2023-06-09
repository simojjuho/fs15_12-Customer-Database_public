using src.CustomerDB;

namespace Name;

public class Program
{
    public static void Main(string[] args)
    {
        /* Right now to test removing customers happend by copying an id from the customers.csv file passing it to database.Delete in
        a Guid form: new Guid([id as a string here]); */
        string id = "ea35e0c6-0506-4414-a178-20517ecf53b5";
        try
        {
            var database = CustomerDatabase.NewCustomerDatabase();
            database.Delete(new Guid(id));
            // Customer cust1 = new Customer("Juho", "Simojoki", "juho@mail.com", "address 123 a 4, 33100 Tampere");
            // Customer cust2 = new Customer("Urho", "Kekkonen", "urkki@mail.com", "address 123 a 4, 00100 Helsinki");
            // Customer cust3 = new Customer("Mauno", "Koivisto", "manu@mail.com", "address 123 a 4, 00120 Helsinki");
            // Customer cust4 = new Customer("Martti", "Ahtisaari", "mara@mail.com", "address 123 a 4, 90100 Oulu");
            // Customer cust5 = new Customer("Kyösti", "Kallio", "kalliok@mail.com", "address 123 a 4, 20100 Turku");
            // Customer cust6 = new Customer("Tarja", "Halonen", "tarja_h@mail.com", "address 123 a 4, 00300 Helsinki");
            // Customer cust7 = new Customer("Mauno", "Koivisto", "manu@mail.com", "heaven's way 1, 00000 Heaven");

            // var custId1 = database.AddNew(cust1);
            // var custId2 = database.AddNew(cust2);
            // var custId3 = database.AddNew(cust3);

            // database.Undo();
            // database.Undo();
            // database.Redo();
            // database.Undo();
            // database.Redo();
            // database.Redo();
    
            // database.Update(cust7);
            Console.WriteLine(database);
        }
        catch (System.Exception ex)
        {
             Console.WriteLine(ex.Message);
        }

       /*  try
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
        } */
    }    
}
