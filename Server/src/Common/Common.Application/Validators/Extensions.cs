using Common.Application.DTOs;
using FluentValidation;

namespace Common.Application.Validators;

public static class Extensions
{
    public static IRuleBuilderOptions<T, BaseFilter> IsBaseFilter<T>(this IRuleBuilder<T, BaseFilter> ruleBuilder)
    {
        return ruleBuilder
            .NotNull()
            .SetValidator(new BaseFilterValidator());
    }
}