namespace src.CustomerDB.CustomerDB;

class Customer
{
    private string _id;
    private string _firstName;
    private string _lastName;
    private string _email;
    private string _address;

    public string Id
    {
        get
        {
            return _id;
        }
    }
    public string FirstName
    {
        get
        {
            return FirstName;
        }
        set
        {
            if(value.Length < 2 )
            {
                throw new Exception("New first name too short");
            }
            else
            {
                _firstName = value;
            }
        }
    } 
    public string LastName
    {
        get
        {
            return _lastName;
        }
        set
        {
            if(value.Length < 2 )
            {
                throw new Exception("New last name too short");
            }
            else
            {
                _firstName = value;
            }
        }
    }
    public string Email
    {
        get
        {
            return _email;
        }
        set
        {
           if(value.Length < 7 )
            {
                // In real world case this would have more validations.
                throw new Exception("New email too short");
            }
            else
            {
                _email = value;
            } 
        }
    }
    public string Address
    {
        get
        {
            return _address;
        }
        set
        {
            if(value.Length < 15)
            {
                throw new Exception("Address too short");
            }
            else
            {
                _address = value;
            }
        }
    }
    public Customer(string id, string firstName, string lastName, string email, string address )
    {
        this._id = id;
        this._firstName = firstName;
        this._lastName = lastName;
        this._email = email;
        this._address = address;
    }
    public override bool Equals(object? obj)
    {
        if(obj is not null)
        {
            var customerObject = (Customer) obj;
            return customerObject._email == this._email || customerObject._id == this._id;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_id, _email);
    }

    public override string ToString()
    {
        return $"Customer id: {_id}:\nname: {_lastName}, {_firstName}\nemail: {_email}\naddress: {_address}";
    }

    ~Customer()
    {
        Console.WriteLine($"Customer instance {this} deleted");
    }
}