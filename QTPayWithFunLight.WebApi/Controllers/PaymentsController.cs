
using Microsoft.AspNetCore.Mvc;

namespace QTPayWithFunLight.WebApi.Controllers
{
        public class PaymentsController : GenericController<Logic.Entities.Payment, Models.EditPayment, Models.Payment>
    {
        public PaymentsController(Logic.Controllers.PaymentsController controller) : base(controller)
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
            var instanceAccess = DataAccess as Logic.Controllers.PaymentsController;

            return Ok(instanceAccess != null ? ToOutModel(await instanceAccess.QueryByAsync(creditCardNumber, year, month, day)) : Array.Empty<Models.Payment>());
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
            var instanceAccess = DataAccess as Logic.Controllers.PaymentsController;

            return Ok(instanceAccess != null ? await instanceAccess.QueryVolumeByAsync(creditCardNumber, year, month, day) : 0m);
        }
    }

}
