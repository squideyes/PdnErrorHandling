// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using OneOf;

AddUser(null!, "Berman").Switch(
    userId => Console.WriteLine($"User \"{userId}\" added!"),
    message => Console.WriteLine($"ERROR: {message}"));

static OneOf<Success, Failure> AddUser(string firstName, string lastName)

{
    if (string.IsNullOrWhiteSpace(firstName))
        return new Failure("A valid \"firstName\" must be supplied!");

    if (string.IsNullOrWhiteSpace(lastName))
        return new Failure("A valid \"lastName\" must be supplied!");

    var userId = Guid.NewGuid();

    return new Success(userId);
}

record Success(Guid UserId);
record Failure(string Message);