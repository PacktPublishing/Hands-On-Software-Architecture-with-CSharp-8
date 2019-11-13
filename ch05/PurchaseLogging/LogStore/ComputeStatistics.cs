using System;
using System.Collections.Generic;
using System.Fabric;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IdempotencyTools;
using Interactions;
using Microsoft.Extensions.Hosting;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;

namespace LogStore
{
    public class ComputeStatistics : BackgroundService
    {
        IReliableQueue<IdempotentMessage<PurchaseInfo>> queue;
        IReliableStateManager stateManager;
        ConfigurationPackage configurationPackage;
        public ComputeStatistics(
            IReliableQueue<IdempotentMessage<PurchaseInfo>> queue,
            IReliableStateManager stateManager,
            ConfigurationPackage configurationPackage)
        {
            this.queue = queue;
            this.stateManager = stateManager;
            this.configurationPackage = configurationPackage;
        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            bool queueEmpty = false;
            var delayString=configurationPackage.Settings.Sections["Timing"]
                .Parameters["MessageMaxDelaySeconds"].Value;
            var delay = int.Parse(delayString);
            var filter = await IdempotencyFilter.NewIdempotencyFilter(
                "logMessages", delay, stateManager);
            var store = await
                stateManager.GetOrAddAsync<IReliableDictionary<string, RunningTotal>>("partialCount");
            while (!stoppingToken.IsCancellationRequested)
            {
                while (!queueEmpty && !stoppingToken.IsCancellationRequested)
                {
                    RunningTotal total = null;
                    using (ITransaction tx = stateManager.CreateTransaction())
                    {
                        var result = await queue.TryDequeueAsync(tx);
                        if (!result.HasValue) queueEmpty = true;
                        else
                        {
                            
                            var item = await filter.NewMessage<PurchaseInfo>(result.Value);
                            if(item != null)
                            {
                                var counter = await store.TryGetValueAsync(tx, item.Location);
                                var newCounter = counter.HasValue ? 
                                    new RunningTotal
                                    {
                                        Count=counter.Value.Count,
                                        Day= counter.Value.Day
                                    }
                                    : new RunningTotal();
                                total = newCounter.Update(item.Time, item.Cost);
                                if (counter.HasValue)
                                    await store.TryUpdateAsync(tx, item.Location, 
                                        newCounter, counter.Value);
                                else
                                    await store.TryAddAsync(tx, item.Location, newCounter);
                            }
                            await tx.CommitAsync();
                            if(total != null)
                            {
                                await SendTotal(total, item.Location);
                            }
                        }

                    }
                }
                await Task.Delay(100, stoppingToken);
                queueEmpty = false;
            }
        }
        protected async Task SendTotal(RunningTotal total, string location)
        {
            
        }
    }
}
