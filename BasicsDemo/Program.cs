// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using System.Net;

SimpleTryCatch();
//CatchSpecificException();
//CatchesFirstExceptionTypeThatMatches();
//CatchAndRethrowException();
//ExceptionFilters();
//TryCatchFinally();
//UnhandledExceptionInMainThread();
//ExceptionsInChildThreadIgnored();
//SilentExceptionsAreSilent();

void SimpleTryCatch()
{
    try
    {
        _ = File.OpenRead("MissingFile.jpg");
    }
    catch (Exception ex)
    {
        ShowAndLogTheError(ex, nameof(SimpleTryCatch));
    }
}

void CatchSpecificException()
{
    try
    {
        _ = File.OpenRead("MissingFile.jpg");
    }
    catch (FileNotFoundException ex)
    {
        ShowAndLogTheError(ex, nameof(CatchSpecificException));
    }
    catch
    {
        throw new Exception("This should never be hit!!!");
    }
}

void CatchesFirstExceptionTypeThatMatches()
{
    try
    {
        _ = File.OpenRead("MissingFile.jpg");
    }
    catch (Exception ex)
    {
        ShowAndLogTheError(ex, nameof(CatchSpecificException));
    }
    //catch (FileNotFoundException ex)
    //{
    //    throw new Exception("This doens't get hit!!!");
    //}
}

void CatchAndRethrowException()
{
    try
    {
        try
        {
            throw new Exception("Inner Exception");
        }
        catch (Exception inner)
        {
            throw new Exception("Ooopsie!!", inner);
        }
    }
    catch (Exception outer)
    {
        ShowAndLogTheError(outer, nameof(CatchAndRethrowException));
    }
}

void ExceptionFilters()
{
    try
    {
        throw new HttpRequestException(
            "'Name' field is empty.", null, HttpStatusCode.BadRequest);
    }
    catch (HttpRequestException ex)
        when (ex.StatusCode == HttpStatusCode.Forbidden)
    {
        ShowAndLogTheError(ex, nameof(ExceptionFilters));

        // Do extra security logging
    }
    catch (HttpRequestException ex)
        when (ex.StatusCode == HttpStatusCode.BadRequest)
    {
        ShowAndLogTheError(ex, nameof(ExceptionFilters));
    }
}

void TryCatchFinally()
{
    try
    {
        _ = File.OpenRead("MissingFile.jpg");
    }
    catch (Exception ex)
    {
        ShowAndLogTheError(ex, nameof(TryCatchFinally));
    }
    finally
    {
        Console.WriteLine("dispose objects, send alerts, etc.");
    }
}

void UnhandledExceptionInMainThread()
{
    AppDomain.CurrentDomain.UnhandledException += (s, e) =>
    {
        var error = (Exception)e.ExceptionObject;

        Console.WriteLine("Unhandled Exception: " +
            ((Exception)e.ExceptionObject).Message);

        Environment.Exit(0);
    };

    throw new Exception("An unhandled exception!");
}

void ExceptionsInChildThreadIgnored()
{
    AppDomain.CurrentDomain.UnhandledException += (s, e) =>
    {
        var error = (Exception)e.ExceptionObject;

        Console.WriteLine("Unhandled Exception: " +
            ((Exception)e.ExceptionObject).Message);

        Environment.Exit(0);
    };

    _ = Task.Run(() => throw new Exception("Ooopsie!"));

    Console.Write("Press any key to terminate...");

    Console.ReadKey(true);
}

void SilentExceptionsAreSilent()
{
    async Task DoWorkAsync()
    {
        await Task.CompletedTask;

        throw new Exception("Ooopsie!");
    }

    _ = Task.Run(async () => await DoWorkAsync());

    Console.Write("Press any key to terminate...");

    Console.ReadKey(true);
}

void ShowAndLogTheError(Exception ex, string example)
{
    Console.WriteLine();

    Console.WriteLine(
        $"{ex.GetType()} Error: {ex.Message} (Example: {example})");

    // Log to Serilog...
    // Log to ...
}