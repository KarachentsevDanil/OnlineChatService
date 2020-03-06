using AutoMapper;
using Microsoft.Extensions.Logging;

namespace OCS.WebApi.SignalR.Hubs.Configurations.Contracts
{
    public interface ISignalRBaseConfiguration
    {
        IMapper Mapper { get; }

        ILogger<ISignalRBaseConfiguration> Logger { get; }
    }
}