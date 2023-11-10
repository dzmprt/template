using FluentValidation;

namespace VS.Application.Handler.VotesSet.Queries.GetVotesSet;

public class VotesSetQueryValidator : AbstractValidator<VotesSetQuery>
{
    public VotesSetQueryValidator()
    {
        RuleFor(q => q.Ticket).NotEmpty().Length(6);
    }
}