
using Microsoft.AspNetCore.Mvc;

namespace QTPayWithFunLight.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class Payments2Controller : GenericController<Logic.Models.Payment, Models.EditPayment, Models.Payment>
    {
        public Payments2Controller() 
            : base(Logic.Factory.CreatePayments())
        {
        }

        /// <summary>
        /// This query determines the payments depending on the parameters.
        /// </summary>
        /// <param name="creditCardNumber">The credit card number (optional)</param>
        /// <param name="year">The year (optional)</param>
        /// <param name="month">The month (optional)</param>
        /// <param name="day">The day (optional)</param>
        /// <returns>The result of the query.</returns>
        [HttpGet("query", Name = nameof(QueryAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Models.Payment>>> QueryAsync(
            [FromQuery(Name = "cardNumber")] string? creditCardNumber,
            [FromQuery(Name = "year")] int? year,
            [FromQuery(Name = "month")] int? month,
            [FromQuery(Name = "day")] int? day)
        {
            var models = DataAccess is Logic.IPaymentsAccess<Logic.Models.Payment> instanceDataAccess ? await instanceDataAccess.QueryAsync(creditCardNumber, year, month, day) : Array.Empty<Logic.Models.Payment>();

            return Ok(ToOutModel(models));
        }

        /// <summary>
        /// The query calculates the turnover depending on the parameters.
        /// </summary>
        /// <param name="creditCardNumber">The credit card number (optional)</param>
        /// <param name="year">The year (optional)</param>
        /// <param name="month">The month (optional)</param>
        /// <param name="day">The day (optional)</param>
        /// <returns>The volumn</returns>
        [HttpGet("queryVolume", Name = nameof(QueryVolumeAsync))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<decimal>> QueryVolumeAsync(
            [FromQuery(Name = "cardNumber")] string? creditCardNumber,
            [FromQuery(Name = "year")] int? year,
            [FromQuery(Name = "month")] int? month,
            [FromQuery(Name = "day")] int? day)
        {
            return Ok(DataAccess is Logic.IPaymentsAccess<Logic.Models.Payment> instanceDataAccess ? await instanceDataAccess.QueryVolumeAsync(creditCardNumber, year, month, day) : 0m);
        }
    }
}
