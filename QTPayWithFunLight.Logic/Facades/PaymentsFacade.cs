
namespace QTPayWithFunLight.Logic.Facades
{
    internal class PaymentsFacade : GenericFacade<Models.Payment, Entities.Payment>, IPaymentsAccess<Models.Payment>
    {
        public PaymentsFacade() 
            : base(new Controllers.PaymentsController())
        {
        }

        public PaymentsFacade(FacadeObject facade)
            : base(new Controllers.PaymentsController(facade.ControllerObject))
        {
        }

        internal override Models.Payment ToModel(Entities.Payment entity)
        {
            var result = new Models.Payment();

            result.Source = entity;
            return result;
        }

        public async Task<Models.Payment[]> QueryByAsync(string? creditCardNumber, int? year, int? month, int? day)
        {
            var entities = Controller is Controllers.PaymentsController instanceCtrl ? await instanceCtrl.QueryByAsync(creditCardNumber, year, month, day).ConfigureAwait(false) : Array.Empty<Entities.Payment>();

            return entities != null ? entities.Select(e => ToModel(e)).ToArray() : Array.Empty<Models.Payment>();
        }
        public Task<decimal> QueryVolumeByAsync(string? creditCardNumber, int? year, int? month, int? day)
        {
            return Controller is Controllers.PaymentsController instanceCtrl ? instanceCtrl.QueryVolumeByAsync(creditCardNumber, year, month, day) : Task.FromResult<decimal>(0); 
        }
    }
}
