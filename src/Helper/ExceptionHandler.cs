namespace src.Helper;

class ExceptionHandler : Exception 
{
    private string _message;
    public ExceptionHandler(string message)
    {
        _message = message;
    }
    
    public static ExceptionHandler FileException(string message)
    {
        return new ExceptionHandler(message ?? "File not found or corrupted");
    }

    public static ExceptionHandler ArgumentException(string message)
    {
        return new ExceptionHandler(message ?? "Unknown error when opening a file.");
    }
}