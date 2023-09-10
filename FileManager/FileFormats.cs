using System.Globalization;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace FileManager
{
    public static class FileFormats
    {
        public static List<Record> ReadXmlFile(string filePath)
        {
            var records = new List<Record>();

            try
            {
                if (!File.Exists(filePath))
                    throw new Exception("The XML file does not exist.");

                var doc = XDocument.Load(filePath);
                foreach (var carElement in doc.Descendants("Car"))
                {
                    var dateStr = carElement.Element("Date").Value;
                    var brandName = carElement.Element("BrandName").Value;
                    var priceStr = carElement.Element("Price").Value;

                    if (DateTime.TryParseExact(dateStr, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date) &&
                        int.TryParse(priceStr, out var price))
                        records.Add(new Record { Date = date, BrandName = brandName, Price = price });
                    else
                        throw new Exception("Invalid data format in XML.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading XML file: {ex.Message}");
            }

            return records;
        }

        public static void WriteToXmlFile(string filePath, List<Record> data)
        {
            try
            {
                var root = new XElement("Document",
                    data.Select(record =>
                        new XElement("Car",
                            new XElement("Date", record.Date.ToString("dd.MM.yyyy")),
                            new XElement("BrandName", record.BrandName),
                            new XElement("Price", record.Price.ToString()))));
                var doc = new XDocument(root);
                doc.Save(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error writing to XML file: {ex.Message}");
            }
        }

        public static void AddRecordXml(string filePath, Record record)
        {
            try
            {
                XDocument doc;

                if (!File.Exists(filePath))
                    doc = new XDocument(new XElement("Document"));
                else
                    doc = XDocument.Load(filePath);

                XElement carElement = new XElement("Car");
                carElement.Add(new XElement("Date", record.Date.ToString("dd.MM.yyyy")));
                carElement.Add(new XElement("BrandName", record.BrandName));
                carElement.Add(new XElement("Price", record.Price.ToString()));

                doc.Root.Add(carElement);

                doc.Save(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding to XML file: {ex.Message}");
            }
        }

        public static void RemoveRecordXml(string filePath, string brandNameToRemove)
        {
            try
            {
                XDocument doc;

                if (!File.Exists(filePath))
                    throw new FileNotFoundException("The XML file does not exist.");
                else
                    doc = XDocument.Load(filePath);

                doc.Root.Elements("Car")
                    .Where(carElement => carElement.Element("BrandName")?.Value == brandNameToRemove)
                    .Remove();

                doc.Save(filePath);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while deleting from the XML file: {ex.Message}");
            }
        }


        public static List<Record> ReadJsonFile(string filePath)
        {
            try
            {
                var json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<List<Record>>(json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading JSON file: {ex.Message}");
            }
        }

        public static void WriteToJsonFile(string filePath, List<Record> data)
        {
            try
            {
                var json = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error writing to JSON file: {ex.Message}");
            }
        }

        public static void AddToJsonFile(string filePath, Record record)
        {
            try
            {
                List<Record> existingRecords;

                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    existingRecords = JsonConvert.DeserializeObject<List<Record>>(json);
                }
                else
                    existingRecords = new List<Record>();

                existingRecords.Add(record);

                string updatedJson = JsonConvert.SerializeObject(existingRecords, Formatting.Indented);

                File.WriteAllText(filePath, updatedJson);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding to JSON file: {ex.Message}");
            }
        }

        public static void RemoveRecordJson(string filePath, string brandNameToRemove)
        {
            try
            {
                if (!File.Exists(filePath))
                    throw new FileNotFoundException("The JSON file does not exist.");

                List<Record> records = ReadJsonFile(filePath);

                var updatedRecords = records.Where(record => record.BrandName != brandNameToRemove).ToList();

                WriteToJsonFile(filePath, updatedRecords);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error while deleting from the JSON file: {ex.Message}");
            }
        }
    }
}