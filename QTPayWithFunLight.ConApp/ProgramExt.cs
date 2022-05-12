using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTPayWithFunLight.ConApp
{
    partial class Program
    {
        static readonly Random Random = new(DateTime.Now.Millisecond);
        static partial void AfterRun()
        {
            Task.Run(async () => await CreateDemoDataAsync()).Wait();
        }

        static async Task CreateDemoDataAsync()
        {
            using var ctrl = new Logic.Controllers.PaymentsController();

            for (int i = 0; i < 200; i++)
            {
                var entity = new Logic.Entities.Payment
                {
                    Date = DateTime.Now.AddDays(Random.Next(-1000, 0)),
                    CreditCardNumber = Number.CreateCreditCardNumber(),
                    Amount = 250.0m * (decimal)Random.NextDouble(), 
                };

                await ctrl.InsertAsync(entity);
            }
            await ctrl.SaveChangesAsync();
        }
    }
}
