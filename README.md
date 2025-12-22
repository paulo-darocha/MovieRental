# MovieRental Exercise

This is a dummy representation of a movie rental system.
Can you help us fix some issues and implement missing features?

 * The app is throwing an error when we start, please help us. Also, tell us what caused the issue.
        IRentalFeatures is registered as Singleton, but it depends on MovieRentalDbContext which is Scoped (AddDbContext). A singleton can't hold a scoped dependency, so DI throws at startup.
        Also: IMovieFeatures is not registered, so MovieController can't be constructed.
 * The rental class has a method to save, but it is not async, can you make it async and explain to us what is the difference?
        Sync SaveChanges() blocks the request thread while waiting for DB I/O.
        Async await SaveChangesAsync() still "waits" logically, but it doesn't block a thread during the DB roundtrip, so better scalability under load.
        Async is not "runs in parallel"; it's mainly about not blocking threads during I/O.
 * Please finish the method to filter rentals by customer name, and add the new endpoint.
 * We noticed we do not have a table for customers, it is not good to have just the customer name in the rental.
   Can you help us add a new entity for this? Don't forget to change the customer name field to a foreign key, and fix your previous method!
 * In the MovieFeatures class, there is a method to list all movies, tell us your opinion about it.
        No pagination/filtering: can return a huge table and slow the API.
        Tracks entities by default: unnecessary memory/CPU for read-only calls (we can use AsNoTracking()).
        Exposes EF entities directly: couples API contracts to DB schema (DTOs are cleaner).
        No ordering: results can be inconsistent.
 * No exceptions are being caught in this api, how would you deal with these exceptions?
        Adding global exception handler middleware, and return consistent error responses
        Log exceptions (ILogger)
        Map known exceptions to proper status codes (400/404/409/500, etc.)

	## Challenge (Nice to have)
We need to implement a new feature in the system that supports automatic payment processing. Given the advancements in technology, it is essential to integrate multiple payment providers into our system.

Here are the specific instructions for this implementation:

* Payment Provider Classes:
    * In the "PaymentProvider" folder, you will find two classes that contain basic (dummy) implementations of payment providers. These can be used as a starting point for your work.
* RentalFeatures Class:
    * Within the RentalFeatures class, you are required to implement the payment processing functionality.
* Payment Provider Designation:
    * The specific payment provider to be used in a rental is specified in the Rental model under the attribute named "PaymentMethod".
* Extensibility:
    * The system should be designed to allow the addition of more payment providers in the future, ensuring flexibility and scalability.
* Payment Failure Handling:
    * If the payment method fails during the transaction, the system should prevent the creation of the rental record. In such cases, no rental should be saved to the database.
