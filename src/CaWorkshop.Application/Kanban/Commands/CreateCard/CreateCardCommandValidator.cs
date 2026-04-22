using System;
using System.Collections.Generic;
using System.Text;

namespace CaWorkshop.Application.Kanban.Commands.CreateCard;

using FluentValidation;

public class CreateCardCommandValidator : AbstractValidator<CreateCardCommand>
{
    public CreateCardCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(3)
            .MaximumLength(100);
    }
}
