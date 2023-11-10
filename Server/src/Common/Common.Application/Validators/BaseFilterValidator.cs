using Common.Application.DTOs;
using FluentValidation;

namespace Common.Application.Validators;

internal sealed class BaseFilterValidator : AbstractValidator<BaseFilter>
{
    public BaseFilterValidator()
    {
        RuleFor(r => r.Limit).GreaterThan(0).When(r => r.Limit.HasValue);
        RuleFor(r => r.Offset).GreaterThanOrEqualTo(0).When(r => r.Offset.HasValue);
    }
}