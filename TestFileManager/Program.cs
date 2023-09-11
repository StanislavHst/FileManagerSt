using FileManager;

class Program
{
    static void Main(string[] args)
    {
        var fileManager = new FileManager.FileManager();
        fileManager.RegisterXmlFormat();
        fileManager.RegisterJsonFormat();

        string jsonFilePath = "data.json",xmlFilePath = "data.xml"; 
        string jsonFormat = "json",xmlFormat="xml"; 

        List<Record> records = new List<Record>();

        records.Add(new Record { Date = DateTime.Now, BrandName = "New Car 1", Price = 50000 });
        records.Add(new Record { Date = DateTime.Now, BrandName = "New Car 2", Price = 60000 });

        fileManager.WriteFile(jsonFilePath, jsonFormat, records);
        fileManager.WriteFile(xmlFilePath, xmlFormat, records);

        Console.WriteLine("Records saved successfully.");

        var newRecordXml = new Record { Date = DateTime.Now, BrandName = "New Car Xml", Price = 1000 };
        fileManager.AddRecordToFile(xmlFilePath, xmlFormat, newRecordXml);
        
        var newRecordJson = new Record { Date = DateTime.Now, BrandName = "New Car Json", Price = 1000 };
        fileManager.AddRecordToFile(jsonFilePath, jsonFormat, newRecordXml);

        Console.WriteLine("Record add successfully.");

        List<Record> updatedRecords = fileManager.ReadFile(xmlFilePath, xmlFormat);
        Console.WriteLine("Updated Records:Xml");
        foreach (var record in updatedRecords)
        {
            Console.WriteLine($"Date: {record.Date}, BrandName: {record.BrandName}, Price: {record.Price}");
        }
        
        fileManager.DeleteRecord(jsonFilePath,jsonFormat,"New Car 2");
        
        updatedRecords = fileManager.ReadFile(jsonFilePath, jsonFormat);
        Console.WriteLine("Updated Records:Json");
        foreach (var record in updatedRecords)
        {
            Console.WriteLine($"Date: {record.Date}, BrandName: {record.BrandName}, Price: {record.Price}");
        }
        
        fileManager.ConvertFile(xmlFilePath,xmlFormat,"test.json","json");
    }
}