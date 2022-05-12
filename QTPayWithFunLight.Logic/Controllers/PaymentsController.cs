using QTPayWithFunLight.Logic.Entities;

namespace QTPayWithFunLight.Logic.Controllers
{
    public sealed class PaymentsController : GenericController<Entities.Payment>, IPaymentsAccess<Entities.Payment>
    {
        public PaymentsController()
        {
        }

        public PaymentsController(ControllerObject other) : base(other)
        {
        }

        protected override void ValidateEntity(ActionType actionType, Payment entity)
        {
            if (actionType == ActionType.Insert)
            {
                CheckEntity(entity);
            }
            else if (actionType == ActionType.Update)
            {
                CheckEntity(entity);
            }
            base.ValidateEntity(actionType, entity);
        }
        protected override void BeforeActionExecute(ActionType actionType, Payment entity)
        {
            if (actionType == ActionType.Insert)
            {
                entity.Date = DateTime.UtcNow;
            }
            base.BeforeActionExecute(actionType, entity);
        }
        protected override async Task BeforeActionExecuteAsync(ActionType actionType, Payment entity)
        {
            if (actionType == GenericController<Payment>.ActionType.Update)
            {
                using var ctrl = new PaymentsController();
                var dbEntity = await ctrl.EntitySet.FindAsync(entity.Id).ConfigureAwait(false);

                if (dbEntity != null)
                {
                    entity.Date = dbEntity.Date;
                }
            }
            await base.BeforeActionExecuteAsync(actionType, entity).ConfigureAwait(false);
        }

        public Task<Payment[]> QueryByAsync(string? creditCardNumber, int? year, int? month, int? day)
        {
            var query = EntitySet.AsQueryable();

            if (creditCardNumber != null)
            {
                query = query.Where(e => e.CreditCardNumber.Contains(creditCardNumber));
            }
            if (year.HasValue)
            {
                query = query.Where(e => e.Date.Year == year.Value);
            }
            if (month.HasValue)
            {
                query = query.Where(e => e.Date.Month == month.Value);
            }
            if (day.HasValue)
            {
                query = query.Where(e => e.Date.Day == day.Value);
            }
            return query.AsNoTracking().ToArrayAsync();
        }
        public async Task<decimal> QueryVolumeByAsync(string? creditCardNumber, int? year, int? month, int? day)
        {
            return (await QueryByAsync(creditCardNumber, year, month, day).ConfigureAwait(false)).Sum(e => e.Amount);
        }

        public static void CheckEntity(Payment entity)
        {
            if (Number.CheckCreditCardNumber(entity.CreditCardNumber) == false)
            {
                throw new Modules.Exceptions.LogicException($"Invalid {nameof(entity.CreditCardNumber)}.");
            }
        }
    }
}
