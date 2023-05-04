using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialistaionApp
{
    [Serializable]
    public class Trainee
        {
            public string? FirstName { get; init; } = string.Empty;//init sets to readonly after set //? indicates might be null but we're accepting it, so it just lets it be null?
            public string? LastName { get; init; } = "";
            public int? SpartaNo { get; init; } //nullable for age makes sense because age cant be null
            public string FullName => $"{FirstName} {LastName}";
            public override string ToString()
            {
                return $"{SpartaNo} - {FullName}";
            }
        }
    [Serializable]
    public class Course
    {
        public string Subject { get; set; }
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public List<Trainee> Trainees { get; } = new List<Trainee>();
        [field: NonSerialized]
        private readonly DateTime _lastRead;
        public Course()
        {
            _lastRead = DateTime.Now;
        }
        public void AddTrainee(Trainee newTrainee)
        {
            Trainees.Add(newTrainee);
        }
    }
}

