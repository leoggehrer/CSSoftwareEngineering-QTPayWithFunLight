
namespace QTPayWithFunLight.Logic
{
    public interface IPaymentsAccess<T> : IDataAccess<T>
    {
        Task<T[]> QueryByAsync(string? creditCardNumber, int? year, int? month, int? day);
        Task<decimal> QueryVolumeByAsync(string? creditCardNumber, int? year, int? month, int? day);
    }
}
