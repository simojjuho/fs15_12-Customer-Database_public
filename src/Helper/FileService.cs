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
    }

    public void AddNewLine(string dataString)
    {
        using(var sw = _fileHandle.AppendText())
        {
            sw.WriteLine(dataString);
        }
    }

    public void WriteAll()
    {
        throw new NotImplementedException();
    }

    public void UpdateLine()
    {
        throw new NotImplementedException();
    }

    public void RemoveLine()
    {
        throw new NotImplementedException();
    }

    public void EmptyAndOverwrite(List<string> stringLines)
    {
        File.WriteAllLines(_path, stringLines);
        // using(var sw = _fileHandle.AppendText())
        // {
            
        // }
    }
}