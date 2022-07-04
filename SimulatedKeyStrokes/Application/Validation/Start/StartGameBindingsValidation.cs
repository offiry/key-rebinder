using Application.Contracts;
using Application.Handlers.Start;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validation.Start
{
    public class StartGameBindingsInput : IRequest
    {
        public int GameId { get; set; }
    }

    public class StartGameBindingsValidation : AbstractValidator<StartGameBindingsQuery>
    {
        private readonly IValidationRepository _validationRepository;

        public StartGameBindingsValidation(IValidationRepository validationRepository)
        {
            _validationRepository = validationRepository ?? throw new ArgumentNullException(nameof(validationRepository));
        }

        public override ValidationResult Validate(ValidationContext<StartGameBindingsQuery> context)
        {
            RuleFor(context => context)
                .Must((context) =>
                {
                    if (_validationRepository.IsGameExists(context.GameId))
                    {
                        return true;
                    }

                    return false;
                })
                .WithMessage("No such game exists in the database!");

            return base.Validate(context);
        }
    }
}
