
namespace MovieRental.PaymentProviders
{
    public class MbWayProvider
    {
        public Task<bool> Pay(double price)
        {
            //ignore this implementation
            return Task.FromResult<bool>(true);
        }
    }

    public class MbWayPaymentProvider : IPaymentProvider
    {
        private readonly MbWayProvider _provider = new();
        public string Method => "MbWay";

        public Task<bool> PayAsync(double price, CancellationToken ct = default)
        {
            return _provider.Pay(price);
        }
    }
}
