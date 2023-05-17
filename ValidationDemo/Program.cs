// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation;
using System.Reflection;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using ValidationDemo;

var requests = new List<CreateActorRequest>
{
    GetRequest("ABC123", "Fred", null, "Muggs"),
    GetRequest("ABC123", "Fred", 'C', "Muggs"),
    GetRequest("", "Fred", 'C', "Muggs"),
    GetRequest("ABC123", " Fred ", 'C', "Muggs"),
    GetRequest("ABC123", "Fred", '9', "Muggs"),
    GetRequest("ABC123", "Fred", 'C', null!)
};

SetFluentValidationGlobals();

var validator = new CreateActorRequest.Validator();

var options = new JsonSerializerOptions()
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
};

options.Converters.Add(new JsonStringEnumConverter());

foreach (var request in requests)
{
    Console.Clear();

    var result = validator.Validate(request);

    var json = JsonSerializer.Serialize(result, options);

    Console.WriteLine(json);

    Console.WriteLine();

    Console.Write("Press any key to continue...");

    Console.ReadKey();
}

static CreateActorRequest GetRequest(string userName,
    string firstName, char? initial, string lastName)
{
    return new CreateActorRequest()
    {
        UserName = userName,
        FirstName = firstName,
        Initial = initial,
        LastName = lastName
    };
}

static void SetFluentValidationGlobals()
{
    static string GetName(MemberInfo member)
    {
        if (member != null)
            return member.Name;

        return null!;
    }

    ValidatorOptions.Global.DisplayNameResolver = (_, member, _) =>
        GetName(member);
}