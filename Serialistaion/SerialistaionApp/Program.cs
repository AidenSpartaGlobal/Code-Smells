using System.Diagnostics;

namespace SerialistaionApp
{
    internal class Program
    {
        private static ISerialise _serialiser;
        private static string _path = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\");
        static void Main(string[] args)
        {
            Trainee trainee = new Trainee
            {
                FirstName = "Joseph",
                LastName = "McCann",
                SpartaNo = 100
            };
            _serialiser = new SerialiserXML();
            _serialiser.SerialiseToFile(_path + "XMLTrainee.xml", trainee );

            Trainee deserialisedXMLTrainee = _serialiser.DeserialiseFromFile<Trainee>(_path + "XMLTrainee.xml");

            Course tech212 = new Course { Title = "TECH 212", Subject = "C# Test", StartDate = new DateTime(2023, 03, 20) };

            tech212.AddTrainee(trainee);
            tech212.AddTrainee(new Trainee { FirstName= "Anthony", LastName= "Naguib", SpartaNo = 101});
            _serialiser.SerialiseToFile(_path + "XMLcourse.xml", tech212);
        }
    }


}

