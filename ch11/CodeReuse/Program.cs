using dotNetStandardLibrary;
using dotNetStandardLibrary.Evaluations.Content;
using dotNetStandardLibrary.Evaluations.Types;
using System;

namespace CodeReuse
{
    class Program
    {
        static void Main(string[] args)
        {
            var buEvaluation = new BasicUsersEvaluation
            {
                Id = 1,
                User = "Gabriel",
                Description = "Good city to visit",
                Grade = 3
            };

            var puEvaluation = new PrimeUsersEvaluation
            {
                Id = 2,
                User = "Francesco",
                Description = "Good city to visit",
                Grade = 4
            };

            var service = new EvaluationService<CityEvaluation>();
            service.content.Evaluations.Add(buEvaluation);
            service.content.Evaluations.Add(puEvaluation);
            Console.WriteLine($"Type of evaluation: {service.GetTypeOfEvaluation()}");
            Console.WriteLine($"The final evaluation is {service.CalculateEvaluationAverage()}");
            Console.ReadKey();
        }
    }
}
