namespace MovieRental.Rental;

public interface IRentalFeatures
{
    Task<Rental> SaveAsync(Rental rental, CancellationToken ct = default);

    Task<IEnumerable<Rental>> GetRentalsByCustomerNameAsync(string customerName, CancellationToken ct = default);
}