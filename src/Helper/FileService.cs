using System;
using System.IO;
using src.CustomerDB;
using src.Helper;

namespace src.Helper;

class FileService
{
    private string _path { get; set; }
    private FileInfo _fileHandle;

    public FileService(string path)
    {
        _path = path;
        _fileHandle = new FileInfo(_path);
    }

    public string[] GetAllData()
    {
        try
        {
            string[] fileData = File.ReadAllLines(_path);
            return fileData;
        }
        catch (FileNotFoundException ex)
        {
            throw ExceptionHandler.FileException(ex.Message);
        }
        catch (ArgumentException ex)
        {
            throw ExceptionHandler.ArgumentException(ex.Message);
        }
    }

    public void AddNewLine(string dataString)
    {
        try
        {
            using(var sw = _fileHandle.AppendText())
            {
                sw.WriteLine(dataString);
            }
        }
        catch (System.Exception ex)
        {
             throw ExceptionHandler.FileException(ex.Message);
        }
    }

    public void EmptyAndOverwrite(List<string> stringLines)
    {
        try
        {
            File.WriteAllLines(_path, stringLines);
        }
        catch (FileNotFoundException ex)
        {
            throw ExceptionHandler.FileException(ex.Message);
        }
        catch (ArgumentNullException ex)
        {
            throw ExceptionHandler.ArgumentException(ex.Message);
        }
    }
}