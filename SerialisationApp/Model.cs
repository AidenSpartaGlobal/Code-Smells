
namespace SerialisationApp
{
    [Serializable]
    public class Trainee
    {
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = "";
        public int? SparaNo { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public override string ToString()
        {
            return $"{SparaNo} - {FullName}";
        }
    }

    [Serializable]
    public class Course
    {
        public string Subject { get; set; }
        public string Title { get; set; }
        public DateOnly StartDate { get; set; }
        public List<Trainee> Trainees { get; } = new List<Trainee>();
        [field: NonSerialized]
        private readonly DateOnly _lastRead;
        public Course()
        {
            _lastRead = DateOnly.FromDateTime(DateTime.Now);
        }
        public void AddTrainee(Trainee newTrainee)
        {
            Trainees.Add(newTrainee);
        }
    }
}

