using FileManager;

class Program
{
    static void Main(string[] args)
    {
        var fileManager = new FileManager.FileManager();
        fileManager.RegisterXmlFormat();
        fileManager.RegisterJsonFormat();

        string filePath = "data.json"; 
        string format = "json"; 
/*
        List<Record> records = new List<Record>();

        records.Add(new Record { Date = DateTime.Now, BrandName = "New Car 1", Price = 50000 });
        records.Add(new Record { Date = DateTime.Now, BrandName = "New Car 2", Price = 60000 });

        //fileManager.WriteFile(filePath, format, records);

        Console.WriteLine("Records saved successfully.");

        var newRecordXml = new Record { Date = DateTime.Now, BrandName = "New Car 3", Price = 1000 };
        fileManager.AddRecordToFile(filePath, format, newRecordXml);

        Console.WriteLine("Record add successfully.");

        List<Record> updatedRecords = fileManager.ReadFile(filePath, format);
        Console.WriteLine("Updated Records:");
        foreach (var record in updatedRecords)
        {
            Console.WriteLine($"Date: {record.Date}, BrandName: {record.BrandName}, Price: {record.Price}");
        }
        
        fileManager.DeleteRecord(filePath,format,"New Car 3");
        
        updatedRecords = fileManager.ReadFile(filePath, format);
        Console.WriteLine("Updated Records:");
        foreach (var record in updatedRecords)
        {
            Console.WriteLine($"Date: {record.Date}, BrandName: {record.BrandName}, Price: {record.Price}");
        }
        */
        fileManager.ConvertFile(filePath,format,"lolik.xml","xml");
    }
}