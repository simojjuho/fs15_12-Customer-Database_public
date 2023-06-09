using src.Helper;

namespace src.CustomerDB;

class CustomerDatabase
{
    private Dictionary<Guid, Customer> _customers = new();
    private HashSet<string> _emails = new();
    private Stack<Action> _undoStack = new();
    private Stack<Action> _redoStack = new();
    private FileService _fileService = new FileService("customers.csv");
    private static CustomerDatabase? _customerDatabase = null;

    private CustomerDatabase()
    {
        initializeCustomers();
    }

    public int RedoStack
    {
        get
        {
            return _redoStack.Count;
        }
    }
    public int UndoStack
    {
        get
        {
            return _redoStack.Count;
        }
    }
    
    public bool AddNew(Customer customer, bool isUndo = false, bool isRedo = false)
    {
        if(!_emails.Contains(customer.Email))
        {
            _emails.Add(customer.Email);
            _customers.Add(customer.Id, customer);
            _fileService.AddNewLine($"{customer.Id};{customer.FirstName};{customer.LastName};{customer.Email};{customer.Address}");
            // TODO: This needs to be changed. It works, but now it's just too complicated to figure out.
            if(isUndo)
            {
                _redoStack.Push(delegate(){
                    this.Delete(customer.Id, false, true);
                });
            }
            else if (isRedo)
            {
                _undoStack.Push(delegate(){
                    this.Delete(customer.Id, true);
                });
            }
            if(!isRedo && !isUndo) {
                _redoStack.Clear();
                _undoStack.Push(delegate(){
                    this.Delete(customer.Id, true);
                });
            }
            return true;
        }
        else
        {
            throw new Exception("A customer with the given email already exists.");
        }
    }

    public Customer GetEntry(Guid id)
    {
        return _customers[id];
    }

    public string Update(Customer customer)
    {
       try
       {
        Customer oldCustomer = _customers[customer.Id];
        _customers[customer.Id] = customer;
        reWriteAllCustomers();
        _undoStack.Push(delegate(){
            _customers[customer.Id] = oldCustomer;
        });
        _redoStack.Clear();
        return $"Customer {customer.Id} updated";
       }
       catch
       {
        throw new KeyNotFoundException("Could not update.");
       }
    }

    public bool Delete(Guid id, bool isUndo = false, bool isRedo = false)
    {
        try
        {
            Customer oldCustomer = _customers[id];
            _customers.Remove(id);
            _emails.Remove(oldCustomer.Email);
            reWriteAllCustomers();
            // TODO: This needs to be changed. It works, but now it's just too complicated to figure out.
            if(isUndo)
            {
                _redoStack.Push(delegate(){
                    this.AddNew(oldCustomer, false, true);
                });
            }
            else if (isRedo)
            {
                _undoStack.Push(delegate(){
                    this.AddNew(oldCustomer, true);
                });
            }
            if(!isRedo && !isUndo) {
                _redoStack.Clear();
                _undoStack.Push(delegate(){
                    this.AddNew(oldCustomer, true);
                });
            }
            return true;
        }
        catch
        {
            throw new Exception("Could not find a customer with the given id");
        }
    }
    // TODO: Now when undoing things, I'm moving that action into the redoStack,
    // which is not working. Instead I should figure out, how to move the opposite action there.
    public void Redo()
    {
        Action redoAction = _redoStack.Pop();
        redoAction();
    }

    public void Undo()
    {
        Action undoAction = _undoStack.Pop();
        undoAction();
    }

    public override string ToString()
    {
        string text = "Customer Database:\n";
        foreach (var item in _customers)
        {
            text += item.Value;
            text += "\n---\n";
        }
        text += "======";
        return text;
    }

    public static CustomerDatabase NewCustomerDatabase()
    {
        if (_customerDatabase is null)
        {
            _customerDatabase = new CustomerDatabase();
        }
        return _customerDatabase;
    }

    private void reWriteAllCustomers()
    {
        Customer[] customerArray = _customers.Values.ToArray();
        List<string> stringList = new();
        foreach(var customer in customerArray)
        {
            stringList.Add($"{customer.Id};{customer.FirstName};{customer.LastName};{customer.Email};{customer.Address}");
        }
        _fileService.EmptyAndOverwrite(stringList);        
    }

    private void initializeCustomers()
    {
        string[] customerStrings = _fileService.GetAllData();
        if(customerStrings.Length > 0)
        {
            foreach(string val in customerStrings)
            {
                string[] customerArray = val.Split(';');
                if(customerArray.Length != 5)
                {
                    continue;
                }
                var customer = new Customer(customerArray[1], customerArray[2], customerArray[3], customerArray[4], customerArray[0]);
                _emails.Add(customer.Email);
                _customers.Add(customer.Id, customer);
            }
        }        
    }
}