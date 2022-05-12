
namespace QTPayWithFunLight.Logic
{
    public static class Factory
    {
        public static IPaymentsAccess<Models.Payment> CreatePayments()
        {
            return new Facades.PaymentsFacade();
        }
        public static IPaymentsAccess<Models.Payment> CreatePayments(Facades.FacadeObject other)
        {
            return new Facades.PaymentsFacade(other);
        }
    }
}
