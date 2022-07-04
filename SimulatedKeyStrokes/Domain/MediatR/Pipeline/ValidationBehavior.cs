using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.MediatR.Pipeline
{
    internal class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators ?? throw new ArgumentNullException(nameof(validators));
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            var validationResults = await Task.WhenAll(
                _validators.Select(v => Task.Factory.StartNew(() =>
                    {
                        var context = new ValidationContext<TRequest>(request);
                        return v.Validate(context);
                    }, cancellationToken)
                ));

            var validationFailures = validationResults
                .SelectMany(e => e.Errors)
                .Where(f => f != null)
                .Distinct()
                .ToList();

            if (validationFailures.Count > 0)
            {
                throw new ValidationException(validationFailures);
            }

            return await next();
        }
    }
}
