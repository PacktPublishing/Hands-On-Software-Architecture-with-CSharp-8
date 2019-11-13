using System;
using System.Collections.Generic;
using System.Fabric;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdempotencyTools;
using Interactions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;

namespace LogStore
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class LogStore : StatefulService, ILogStore
    {
        public LogStore(StatefulServiceContext context)
            : base(context)
        { }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return this.CreateServiceRemotingReplicaListeners<LogStore>();
        }
        private IReliableQueue<IdempotentMessage<PurchaseInfo>> LogQueue = null;
        public async Task<bool> LogPurchase(IdempotentMessage<PurchaseInfo> idempotentMessage)
        {
            if (LogQueue == null) return false;
            using (ITransaction tx = this.StateManager.CreateTransaction())
            {
                await LogQueue.EnqueueAsync(tx, idempotentMessage);
                await tx.CommitAsync();
                return true;
            }
        }
        /// <summary>
        /// This is the main entry point for your service replica.
        /// This method executes when this replica of your service becomes primary and has write status.
        /// </summary>
        /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service replica.</param>
        protected override async Task RunAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            LogQueue = await
                this.StateManager
                .GetOrAddAsync<IReliableQueue<IdempotentMessage<PurchaseInfo>>>("logQueue");
            var configurationPackage = Context
                .CodePackageActivationContext
                .GetConfigurationPackageObject("Config");

            var host = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(this.StateManager);
                    services.AddSingleton(this.LogQueue);
                    services.AddSingleton(configurationPackage);
                    services.AddHostedService<ComputeStatistics>();
                })
                .Build();
            await host.RunAsync(cancellationToken);
        }
    }
}
