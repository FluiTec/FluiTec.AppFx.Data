using FluentValidation;

namespace FluiTec.AppFx.Data.Ef;

/// <summary>
/// An ef SQL service options validator.
/// </summary>
public class EfSqlServiceOptionsValidator : AbstractValidator<EfSqlServiceOptions>
{
    /// <summary>   Default constructor. </summary>
    public EfSqlServiceOptionsValidator()
    {
        RuleFor(options => options.ConnectionString).NotEmpty();
    }
}