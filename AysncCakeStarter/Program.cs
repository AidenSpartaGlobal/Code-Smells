using System;
using System.Threading;
using System.Threading.Tasks;

namespace AysncCake
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my birthday party");
            HaveAParty();
            Console.WriteLine("Thanks for a lovely party");
            Console.ReadLine();
        }

        private static void HaveAParty()
        {
            var name = "Cathy";
            Task<Drinks> drinksTask = GetDrinksAsync();
            Console.WriteLine($"Guys, I'm back. {drinksTask.Result}.");

            Task<PassOut> PassOutTask = PassOutAsync();
            Console.WriteLine($"I'm tired. {PassOutTask.Result}.");

            Task<Cake> cakeTask = BakeCakeAsync();
            PlayPartyGames();
            OpenPresents();

            //Result is 
            //var cake = cakeTask.Result;
            Console.WriteLine($"Happy birthday, {name}, {cakeTask.Result}!!");
        }

        //We want to get on with other stuff as we
        //wait for the cake to finish baking
        //Async methods - name of the method with Async at the end
        private async static Task<Cake> BakeCakeAsync()
        {
            Console.WriteLine("Cake is in the oven");
            //Thread.Sleep(TimeSpan.FromSeconds(5));
            await Task.Delay(TimeSpan.FromSeconds(5));
            Console.WriteLine("Cake is done");
            return new Cake();
        }

        private async static Task<Drinks> GetDrinksAsync()
        {
            Console.WriteLine("Someone is buying the drinks");
            await Task.Delay(TimeSpan.FromSeconds(1.5));
            Console.WriteLine("Someone has returned with fresh water");
            return new Drinks();
        }

        private async static Task<PassOut> PassOutAsync()
        {
            Console.WriteLine("Cathy, I'm not feeling too well");
            await Task.Delay(TimeSpan.FromSeconds(3));
            Console.WriteLine("The water was too strong, I think I need to lie down");
            return new PassOut();
        }

        private static void PlayPartyGames()
        {
            Console.WriteLine("Starting games");
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("Finishing Games");
        }

        private static void OpenPresents()
        {
            Console.WriteLine("Opening Presents");
            Thread.Sleep(TimeSpan.FromSeconds(1));
            Console.WriteLine("Finished Opening Presents");
        }
    }

    public class Cake
    {
        public override string ToString()
        {
            return "Here's a cake";
        }
    }

    public class Drinks
    {
        public override string ToString()
        {
            return "Here's a cup of water for everyone";
        }
    }

    public class PassOut
    {
        public override string ToString()
        {
            return "ZZZZzzz";
        }
    }

}
