using FluentValidation;

namespace Application.Players.Commands.PlayerLogin
{
    public class PlayerLoginCommandValidator : AbstractValidator<PlayerLoginCommand>
    {
        public PlayerLoginCommandValidator()
        {
            RuleFor(x=>x.PlayerId)
            .NotEmpty()
            .NotNull();
        }
    }
}