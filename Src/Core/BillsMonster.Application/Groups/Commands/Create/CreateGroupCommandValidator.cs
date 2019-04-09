using FluentValidation;
using System;

namespace BillsMonster.Application.Groups.Commands.Create
{
    public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupCommandValidator()
        {
            RuleFor(x => x.Title).MaximumLength(50).NotEmpty();
            RuleFor(x => x.Description).MaximumLength(250);            
        }
    }
}
