﻿// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using Serilog;
using SquidEyes.PdnLogSmart.LoggingDemo;
using SquidEyes.PdnLogSmart.LogSmart;

// Typically read in from KeyVault or another secret store
var seqApiUri = new Uri("http://host.docker.internal:5341");
var seqApiKey = (string)null!;

// Zero or more app-specific fields to enrich ALL log-items with
var enrichWith = new Dictionary<Tag, object>
{
    { Tag.From("ClientId"), "ABC123" },
    { Tag.From("JunketId"), 12345 },
    { Tag.From("RunDate"), DateTime.Today }
};

// Create a standard logger
Log.Logger = SerilogHelper.GetStandardLogger(
    seqApiUri, seqApiKey, Severity.Debug, enrichWith);

// Log "LogonSuccess" (a custom log-item)
Log.Logger.Log(new LogonSucess(Brokerage.CanonTrading,
    new[] { Endpoint.MarketData, Endpoint.HistoryPlant }));

// Log "MiscLogItem" with a simple "Message" Context
Log.Logger.Log(new MiscLogItem(Severity.Warn,
    Tag.From("MiscWarning"), Context.From("Up != Down")));

// Free-form messages can be intermingled with LogItems
Log.Logger.Debug("Who let the dogs out?");

// Log "MiscLogItem" with a multiple tag-value Context
Log.Logger.Log(new MiscLogItem(
    Severity.Debug, Tag.From("GotSettings"),
    new Context()
    {
        { Tag.From("Border"), 2 },
        { Tag.From("Background"), ConsoleColor.Yellow },
        { Tag.From("Foreground"), ConsoleColor.Black },
        { Tag.From("ShowHelp"), true },
        { Tag.From("Session"), new DateOnly(2023, 1, 1) }
    }));

try
{
    try
    {
        throw new ArgumentException(
            "An \"Inner\" exception was caught!");
    }
    catch (Exception inner)
    {
        throw new Exception(
            "An \"Outer\" exception was caught!", inner);
    }
}
catch (Exception outer)
{
    // Log "ErrorCaught" (a standard log-item)
    Log.Logger.Log(new ErrorCaught(outer, true));
}

// Always close and flush before terminating your app
Log.CloseAndFlush();

Console.WriteLine();
Console.WriteLine($"Go to \"{seqApiUri}\" to review your log-items");