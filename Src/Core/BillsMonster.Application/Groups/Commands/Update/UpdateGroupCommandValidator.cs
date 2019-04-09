using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsMonster.Application.Groups.Commands.Update
{
    public class UpdateGroupCommandValidator : AbstractValidator<UpdateGroupCommand>
    {
        public UpdateGroupCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.Title).MaximumLength(50).NotEmpty();
            RuleFor(x => x.Description).MaximumLength(250);
        }
    }
}
