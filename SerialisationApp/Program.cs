namespace SerialisationApp
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
                SparaNo = 100
            };

            _serialiser = new SerialiserXML();
            _serialiser.SerialiseToFile(_path + "XMLTrainee.xml", trainee);

            Trainee deserialisedXMLTrainee = _serialiser.DeserialisationFromFile<Trainee>(_path + "XMLTrainee.xml");

            Course Tech212 = new Course { Title = "TECH 212", Subject = "C# Test", StartDate = new DateOnly(2023, 3, 20) };

            Tech212.AddTrainee(trainee);
            Tech212.AddTrainee(new Trainee { FirstName = "Anthony", LastName = "Naguib", SparaNo = 101});
            _serialiser.SerialiseToFile(_path + "XMLCourse.xml", Tech212);

            var editedCourse = _serialiser.DeserialisationFromFile<Course>(_path + "XMLCourseedited.xml");
        }
    }
}