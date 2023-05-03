using System.Security.Cryptography.X509Certificates;

namespace LambdaLab_Lib
{
    public class Exercsises
    {
        public static int CountTotalNumberOfSpartans(List<Spartan> spartans) => spartans.Count();


        public static int CountTotalNumberOfSpartansInUKAndUSA(List<Spartan> spartans) => spartans.Count(x => x.Country == "U.K." || x.Country == "U.S.A.");
      

        public static int NumberOfSpartansBornAfter1980(List<Spartan> spartans) => spartans.Count(x => x.DateOfBirth.Year > 1980);


        public static int SumOfAllSpartaMarksMoreThan50Inclusive(List<Spartan> spartans) => spartans.Where(x => x.SpartaMark >= 50).Sum(x => x.SpartaMark);
                                                                                           //spartans.Where(x => X.SpartaMark >= 50).Select(x => x.SpartaMark).Sum();  

        //To 2 decimal points
        public static double AverageSpartanMark(List<Spartan> spartans) => Math.Round(spartans.Average(x => x.SpartaMark), 2);
                                                                          //spartans.Average(x => x.SpartaMark.Sum());
                                                                                

        public static List<Spartan> SortByAlphabeticallyByLastName(List<Spartan> spartans) => spartans.OrderBy(x => x.LastName).ToList();
                                                                                        //spartans.OrderBy(x => x.LastName);

        public static List<string> ListAllDistinctCities(List<Spartan> spartans) => spartans.Select(x => x.City).Distinct().ToList();
                                                                //spartans.Where(x => x.city).Select(x => x.City.Distinct()).ToList();


        public static List<Spartan> ListAllSpartansWithIdBeginingWithB(List<Spartan> spartans) => spartans.FindAll(x => x.SpartanId.StartsWith("B"));
      
    }
}