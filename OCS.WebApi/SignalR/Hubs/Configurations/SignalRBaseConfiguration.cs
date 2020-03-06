using AutoMapper;
using Microsoft.Extensions.Logging;
using OCS.WebApi.SignalR.Hubs.Configurations.Contracts;

namespace OCS.WebApi.SignalR.Hubs.Configurations
{
    public class SignalRBaseConfiguration : ISignalRBaseConfiguration
    {
        public SignalRBaseConfiguration(IMapper mapper, ILogger<SignalRBaseConfiguration> logger)
        {
            Mapper = mapper;
            Logger = logger;
        }

        public IMapper Mapper { get; }

        public ILogger<ISignalRBaseConfiguration> Logger { get; }
    }
}