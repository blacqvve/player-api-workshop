using System;
using FluentValidation;

namespace Application.Players.Commands.AddFriend
{
    public class AddFriendCommandValidator : AbstractValidator<AddFriendCommand>
    {
        public AddFriendCommandValidator()
        {
            RuleFor(x=>x.PlayerId).NotEmpty().NotNull();

            RuleFor(x=>x.FriendId).NotEmpty().NotNull();
        }
    }
}