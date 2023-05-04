using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SerialistaionApp
{
    public class SerialiserXML : ISerialise
    {
        public T DeserialiseFromFile<T>(string filePath)
        {
            Stream fileStream = File.OpenRead(filePath);
            XmlSerializer reader = new XmlSerializer(typeof(T));
            var deserialisedItem = (T)reader.Deserialize(fileStream);
            fileStream.Close();
            return deserialisedItem;
        }

        public void SerialiseToFile<T>(string filePath, T item)
        {
            FileStream fileStream = File.Create(filePath);
            XmlSerializer writer = new XmlSerializer(typeof(T));
            writer.Serialize(fileStream, item);
            fileStream.Close();
        }
    }
}
