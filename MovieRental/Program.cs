using MovieRental.Data;
using MovieRental.Movie;
using MovieRental.PaymentProviders;
using MovieRental.Rental;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddEntityFrameworkSqlite().AddDbContext<MovieRentalDbContext>();

builder.Services.AddScoped<IRentalFeatures, RentalFeatures>();
builder.Services.AddScoped<IMovieFeatures, MovieFeatures>();

builder.Services.AddScoped<IPaymentProvider, PayPalPaymentProvider>();
builder.Services.AddScoped<IPaymentProvider, MbWayPaymentProvider>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// using DI to create the DbContext
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<MovieRentalDbContext>();
    db.Database.EnsureCreated();
}

app.Run();
