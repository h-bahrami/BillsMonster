using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BillsMonster.Application.Groups.Commands.Delete
{
    class DeleteGroupCommandValidator: AbstractValidator<DeleteGroupCommand>
    {
        public DeleteGroupCommandValidator()
        {
            RuleFor(x =>x.Id).NotEmpty();
        }
    }
}
