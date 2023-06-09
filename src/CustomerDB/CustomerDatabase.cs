namespace src.CustomerDB.CustomerDB;

class CustomerDatabase
{
    private Dictionary<string, Customer> _customers = new();
    private HashSet<string> _emails = new();
    private Stack<Action> _undoStack = new();
    private Stack<Action> _redoStack = new();

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
            _emails.Add(customer.Id);
            _customers.Add(customer.Id, customer);
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

    public Customer GetEntry(string id)
    {
        return _customers[id];
    }

    public string Update(Customer customer)
    {
       try
       {
        Customer oldCustomer = _customers[customer.Id];
        _customers[customer.Id] = customer;
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

    public bool Delete(string id, bool isUndo = false, bool isRedo = false)
    {
        try
        {
            Customer oldCustomer = _customers[id];
            _customers.Remove(id);
            _emails.Remove(oldCustomer.Email);
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
        string text = "Customer Database:\n ";
        foreach (var item in _customers)
        {
            text += item;
            text += "\n---\n";
        }
        text += "======";
        return text;
    }
}