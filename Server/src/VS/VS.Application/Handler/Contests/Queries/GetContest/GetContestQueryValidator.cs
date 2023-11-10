using FluentValidation;

namespace VS.Application.Handler.Contests.Queries.GetContest;

public class GetContestQueryValidator : AbstractValidator<GetContestQuery>
{
    public GetContestQueryValidator()
    {
        RuleFor(cmd => cmd.Id).GreaterThan(0);
    }
}