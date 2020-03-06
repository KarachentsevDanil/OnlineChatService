using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using OCS.WebApi.SignalR.Hubs.Configurations.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OCS.WebApi.SignalR.Hubs.Abstract
{
    public abstract class BaseChatHub : Hub
    {
        private const string ValidationSeparator = ",";

        protected ISignalRBaseConfiguration Configuration { get; }

        protected BaseChatHub(ISignalRBaseConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected async Task ExecuteActionAsync<TModel>(
            TModel model,
            Func<TModel, CancellationToken, Task> action,
            IValidator<TModel> validator = default,
            CancellationToken ct = default)
            where TModel : class
        {
            try
            {
                ValidationResult validationResult = ValidateModel(model, validator);

                if (validationResult != null && !validationResult.IsValid)
                {
                    Configuration.Logger.LogWarning($"Model ${nameof(TModel)} is invalid. Errors: {validationResult.ToString(ValidationSeparator)}");
                    return;
                }

                await action(model, ct);
            }
            catch (Exception exception)
            {
                Configuration.Logger.LogError(exception.Message);
            }
        }

        protected virtual ValidationResult ValidateModel<TModel>(TModel command, IValidator<TModel> validator)
            where TModel : class
        {
            if (validator != null)
            {
                ValidationResult validationResult = validator.Validate(command);

                if (!validationResult.IsValid)
                {
                    return validationResult;
                }
            }

            return null;
        }
    }
}