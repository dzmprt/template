using FluentValidation;

namespace VS.Application.Handler.Images.Queries.GetImageFileQuery;

public class GetImageQueryValidator : AbstractValidator<GetImageQuery>
{
    public GetImageQueryValidator()
    {
        RuleFor(cmd => cmd.Id).GreaterThan(0);
    }
}