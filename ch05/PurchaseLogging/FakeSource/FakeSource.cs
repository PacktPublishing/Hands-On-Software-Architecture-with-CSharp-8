using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdempotencyTools;
using Interactions;
using Microsoft.ServiceFabric.Services.Client;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Client;
using Microsoft.ServiceFabric.Services.Runtime;

namespace FakeSource
{
    /// <summary>
    /// An instance of this class is created for each service instance by the Service Fabric runtime.
    /// </summary>
    internal sealed class FakeSource : StatelessService
    {
        string[] locations = new string[] { "Florence", "London", "New York", "Paris" };
        public FakeSource(StatelessServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
        /// </summary>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
        {
            return new ServiceInstanceListener[0];
        }

        /// <summary>
        /// This is the main entry point for your service instance.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            long iterations = 0;
            Random random = new Random();
            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                PurchaseInfo message = new PurchaseInfo
                {
                    Time = DateTimeOffset.Now,
                    Location = locations[random.Next(0, locations.Length)],
                    Cost = 200m * random.Next(1, 4)
                };
                var partition = new ServicePartitionKey(Math.Abs(message.Location.GetHashCode()) % 1000);
                var client = ServiceProxy.Create<ILogStore>(
                    new Uri("fabric:/PurchaseLogging/LogStore"), partition);
                try
                {
                    while (!await client.LogPurchase(new IdempotentMessage<PurchaseInfo>(message)))
                    {
                        await Task.Delay(TimeSpan.FromMilliseconds(100), cancellationToken);
                    }
                }
                catch
                {

                }
                ServiceEventSource.Current.ServiceMessage(this.Context, "Working-{0}", ++iterations);

                await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
            }
        }
    }
}
