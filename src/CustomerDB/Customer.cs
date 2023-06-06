namespace src.CustomerDB.CustomerDB;

class Customer
{
    private string _id { get; }
    private string _firstName { get; }
    private string _lastName { get; }
    private string _email { get; set; }
    private string _address { get; set; }

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
}