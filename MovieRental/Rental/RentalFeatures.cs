using Microsoft.EntityFrameworkCore;
using MovieRental.Data;
using MovieRental.PaymentProviders;

namespace MovieRental.Rental
{
    public class RentalFeatures : IRentalFeatures
    {
        private readonly MovieRentalDbContext _movieRentalDb;
        private readonly IReadOnlyDictionary<string, IPaymentProvider> _paymentProvider;

        public RentalFeatures(MovieRentalDbContext movieRentalDb, IEnumerable<IPaymentProvider> paymentProvider)
        {
            _movieRentalDb = movieRentalDb;
            _paymentProvider = paymentProvider.ToDictionary(p => p.Method, StringComparer.OrdinalIgnoreCase);
        }

        public async Task<Rental> SaveAsync(Rental rental, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(rental.PaymentMethod))
            {
                throw new ArgumentNullException(nameof(rental.PaymentMethod));
            }

            if (!_paymentProvider.TryGetValue(rental.PaymentMethod, out var payProvider))
            {
                throw new ArgumentException($"Payment method not supported: {rental.PaymentMethod}");
            }

            var price = rental.DaysRented * 10.0; // the price has to come from some other table

            var paymentSucceded = await payProvider!.PayAsync(price, ct);

            if (!paymentSucceded) { throw new InvalidOperationException("Payment has failed."); }

            await _movieRentalDb.Rentals.AddAsync(rental);
            await _movieRentalDb.SaveChangesAsync();

            return rental;
        }


        public async Task<IEnumerable<Rental>> GetRentalsByCustomerNameAsync(string customerName, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(customerName)) { return []; }

            var nameLike = $"%{customerName.Trim()}%";

            return await _movieRentalDb.Rentals
                .AsNoTracking()
                .Include(r => r.Movie)
                .Include(r => r.Customer)
                .Where(r => EF.Functions.Like(r.Customer!.Name, nameLike))
                .ToListAsync(ct);
        }

    }
}
