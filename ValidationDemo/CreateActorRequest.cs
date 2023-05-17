// ********************************************************
// The use of this source code is licensed under the terms
// of the MIT License (https://opensource.org/licenses/MIT)
// ********************************************************

using FluentValidation;

namespace ValidationDemo;

public class CreateActorRequest
{
    public class Validator : AbstractValidator<CreateActorRequest>
    {
        public Validator()
        {
            const string MUSTBE = "'{PropertyName}' must be ";

            RuleFor(x => x.UserName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Length(8)
                .Must(v => v.IsBase32())
                .WithMessage(MUSTBE + "an 8-character BASE32 code.");

            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(v => v!.IsNonEmptyAndTrimmed())
                .WithMessage(MUSTBE + "non-empty and trimmed.");

            RuleFor(x => x.Initial)
                .Must(v => !v.HasValue || char.IsAsciiLetterUpper(v!.Value))
                .WithMessage(MUSTBE + "an uppper-case ASCII letter or NULL.");

            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .Must(v => v!.IsNonEmptyAndTrimmed())
                .WithMessage(MUSTBE + "non-empty and trimmed.");
        }
    }

    public string? UserName { get; init; }
    public string? FirstName { get; init; }
    public char? Initial { get; init; }
    public string? LastName { get; init; }
}