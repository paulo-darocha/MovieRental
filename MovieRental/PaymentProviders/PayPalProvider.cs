
namespace MovieRental.PaymentProviders
{
    public class PayPalProvider
    {
        public Task<bool> Pay(double price)
        {
            //ignore this implementation
            return Task.FromResult<bool>(true);
        }
    }

    public class PayPalPaymentProvider : IPaymentProvider
    {
        private readonly PayPalProvider _provider = new();

        public string Method => "PayPal";

        public Task<bool> PayAsync(double price, CancellationToken ct = default)
        {
            return _provider.Pay(price);
        }
    }
}
