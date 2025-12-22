using Microsoft.AspNetCore.Mvc;
using MovieRental.Movie;
using MovieRental.Rental;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {

        private readonly IRentalFeatures _features;

        public RentalController(IRentalFeatures features)
        {
            _features = features;
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Rental.Rental rental, CancellationToken ct = default)
        {
            return Ok(await _features.SaveAsync(rental, ct));
        }

        [HttpGet("by-customer")]
        public async Task<IActionResult> GetByCustomer([FromQuery] string name, CancellationToken ct = default)
        {
            return Ok(await _features.GetRentalsByCustomerNameAsync(name, ct));
        }
    }
}
