using FluentValidation;
using System;

namespace BillsMonster.Application.Groups.Commands.Create
{
    public class CreateGroupCommandValidator : AbstractValidator<CreateGroupCommand>
    {
        public CreateGroupCommandValidator()
        {
            RuleFor(x => x.Model.Title).MaximumLength(50).NotEmpty();
            RuleFor(x => x.Model.Description).MaximumLength(250);            
        }
    }
}
