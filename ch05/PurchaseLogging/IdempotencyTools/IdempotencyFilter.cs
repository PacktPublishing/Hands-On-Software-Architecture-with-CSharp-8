using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using System.Linq;
using System.Threading;

namespace IdempotencyTools
{
    public class IdempotencyFilter
    {
        protected IReliableDictionary<Guid, DateTimeOffset> dictionary;
        protected int maxDelaySeconds;
        protected DateTimeOffset lastClear;
        protected IReliableStateManager sm;
        protected IdempotencyFilter() { }
        public static async Task<IdempotencyFilter> NewIdempotencyFilter(
            string name, 
            int maxDelaySeconds, 
            IReliableStateManager sm)
        {
            var result = new IdempotencyFilter();
            result.dictionary = await sm.GetOrAddAsync<IReliableDictionary<Guid, DateTimeOffset>> (name);
            result.maxDelaySeconds = maxDelaySeconds;
            result.lastClear = DateTimeOffset.Now;
            result.sm = sm;
            return result;
        }
        public async Task<T> NewMessage<T>(IdempotentMessage<T> message)
        {
            DateTimeOffset now = DateTimeOffset.Now;
            if ((now - lastClear).TotalSeconds > 1.5 * maxDelaySeconds)
            {
                await Clear();
                
            }
            if ((now - message.Time).TotalSeconds > maxDelaySeconds)
                return default(T);
            using (ITransaction tx = this.sm.CreateTransaction())
            {
                var result = await dictionary.TryGetValueAsync(tx, message.Id);
                if (result.HasValue)
                {
                    tx.Abort();
                    return default(T);
                }
                else
                {
                    await dictionary.TryAddAsync(tx, message.Id, message.Time);
                    await tx.CommitAsync();
                    return message.Value;
                }
            }
         }
        public async Task Clear()
        {
            DateTimeOffset now = DateTimeOffset.Now;
            var toKeep = new List<KeyValuePair<Guid, DateTimeOffset>>(); 
            using (ITransaction tx = this.sm.CreateTransaction())
            {
                var asyncEnumerable = await dictionary.CreateEnumerableAsync(tx);
                using (var asyncEnumerator = asyncEnumerable.GetAsyncEnumerator())
                {
                    while (await asyncEnumerator.MoveNextAsync(CancellationToken.None))
                    {
                        if((now- asyncEnumerator.Current.Value).TotalSeconds< maxDelaySeconds)
                            toKeep.Add(asyncEnumerator.Current);
                    }
                }
                await dictionary.ClearAsync();
                foreach (var pair in toKeep)
                    await dictionary.TryAddAsync(tx, pair.Key, pair.Value);
                await tx.CommitAsync();
                lastClear = DateTimeOffset.Now;
            }
        }
    }
}
