namespace MovieRental.PaymentProviders
{
    public interface IPaymentProvider
    {
        string Method { get; } // PayPal, MbWay, etc

        Task<bool> PayAsync(double price, CancellationToken ct = default);
    }
}
