using System;
using DynamicKeywordShowcase.Helpers;
using DynamicKeywordShowcase.Models;
using FizzWare.NBuilder;
using Newtonsoft.Json;

namespace DynamicKeywordShowcase
{
    class Program
    {
        static void Main(string[] args)
        {
            var firstCar = new Car();
            Console.WriteLine("First car:");
            Console.WriteLine(JsonConvert.SerializeObject(firstCar,Formatting.Indented));

            var secondCar = Builder<Car>.CreateNew().Build();
            Console.WriteLine("Second car:");
            Console.WriteLine(JsonConvert.SerializeObject(secondCar, Formatting.Indented));

            var recursiveObjectBuilder = new RecursiveObjectBuilder(3,5);
            var thirdCar = recursiveObjectBuilder.CreateGenericObject<Car>(true);
            Console.WriteLine("Third car:");
            Console.WriteLine(JsonConvert.SerializeObject(thirdCar, Formatting.Indented));
        }
    }
}
