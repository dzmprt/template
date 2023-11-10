using FluentValidation;

namespace VS.Application.Handler.VotesSet.Commands.AddOrUpdate;

public class AddOrUpdateVoteSetCommandValidator : AbstractValidator<AddOrUpdateVoteSetCommand>
{
    public AddOrUpdateVoteSetCommandValidator()
    {
        RuleFor(cmd => cmd.ContestId).GreaterThan(0);
        RuleFor(cmd => cmd.TicketKey).NotEmpty().Length(6);
        RuleFor(cmd => cmd.Votes.GroupBy(v => v.ParticipantId).Any(v => v.Count() > 1))
            .Must(any => !any)
            .WithMessage("Duplicated participant");

        RuleForEach(cmd => cmd.Votes)
            .ChildRules(vote =>
            {
                vote.RuleFor(v => v.ParticipantId).GreaterThan(0);
                vote.RuleFor(v => v.PrizeNumber).GreaterThan(0);
            });
    }
}