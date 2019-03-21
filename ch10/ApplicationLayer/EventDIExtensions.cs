using System;
using System.Collections.Generic;
using DDD.DomainLayer;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.ApplicationLayer
{
    public static class EventDIExtensions
    {
        private static IDictionary<Type, List<Type>> eventDictionary =
            new Dictionary<Type, List<Type>>();
        public static IServiceCollection AddEventHandler<T, H>
            (this IServiceCollection service)
            where T : IEventNotification
            where H : class, IEventHandler<T>
        {
            service.AddSingleton<H>();
            List<Type> list = null;
            eventDictionary.TryGetValue(typeof(T), out list);
            if (list == null)
            {
                list = new List<Type>();
                eventDictionary.Add(typeof(T), list);
                service.AddSingleton<EventTrigger<T>>(p =>
                {
                    var handlers = new List<IEventHandler<T>>();
                    foreach (var type in eventDictionary[typeof(T)])
                    {
                        handlers.Add(p.GetService(type) as IEventHandler<T>);
                    }
                    return new EventTrigger<T>(handlers);
                });
            }
            list.Add(typeof(H));

            return service;
        }
    }
}
