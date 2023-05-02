using System.Linq;

namespace LambdasApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var myList = new List<int> { 3, 7, 1, 2, 8, 3, 0, 4, 5 };

            int allCount = myList.Count();
            int evenCount = myList.Count(IsEven);
            int sum = myList.Sum();

            var people = new List<Person>
            {
                new Person {Name = "Aaron", Age = 40, City = "Ottawa"},
                new Person {Name = "Joe", Age = 55, City = "Manchester"},
                new Person {Name = "Nish", Age = 20, City = "London"}
            };
            var youngPeopleCount = people.Count(IsYoung);
            //Anomynous methods are inline methods which are only used once, Not reusable.

            var evenLCount = myList.Count(x => x % 2 == 0);

            //count people who are under 30
            var youngPeopleCount2 = people.Count(x => x.Age < 30);
            var totalAge = people.Sum(x => x.Age);
            var oldPeopleTotalAge = people.Sum(x => x.Age > 30 ? x.Age : 0);
            var oldPeopleTotalAge2 = people.Where(x => x.Age >30)
                .Select(x => x.Age)
                .Sum();

           //Does not hold any person object 
           //The query needs to be executed
           //Called Deferred execution
           IEnumerable<Person> peopleLondonQuery = people.Where(x => x.City == "London");

            //execute query - when we iterate over it.  Iterating over anything that implements
            // IEnumerable means that the GetEnumerable method is called 
            foreach (var Item in peopleLondonQuery)
            {
                Console.WriteLine(Item);
            }

            //Iterating through the query and adding the people based off the query
            // To a list
            var peopleLondonList = peopleLondonQuery.ToList();
            var peopleLondonCount = peopleLondonQuery.Count();

            //Anything where we iterate over a query leads to the query being executed
            //Includes: loops, ToList and ToArray etc, LINQ aggregate functions such as Sum and Count

        }

        public static bool IsEven(int n) => n % 2 == 0;
        

        public static bool IsYoung(Person person)
        {
            return person.Age < 30;
        }
    }

   

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }


        public override string ToString()
        {
            return $"{Name} - {Age} - {City}";
        }
    }
}