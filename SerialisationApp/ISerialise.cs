using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialisationApp
{
    internal interface ISerialise
    {
        void SerialiseToFile<T>(string filePath, T item);
        T DeserialisationFromFile<T>(string filePath);
    }
}
