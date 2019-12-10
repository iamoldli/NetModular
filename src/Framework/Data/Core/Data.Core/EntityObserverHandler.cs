using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NetModular.Lib.Data.Abstractions;
using NetModular.Lib.Data.Abstractions.Entities;

namespace NetModular.Lib.Data.Core
{
    public class EntityObserverHandler : IEntityObserverHandler
    {
        private readonly IServiceProvider _sp;

        public EntityObserverHandler(IServiceProvider sp)
        {
            _sp = sp;
        }

        public Task Add<T>(dynamic id) where T : IEntity
        {
            var observers = _sp.GetServices<IEntityObserver<T>>().ToList();
            if (observers.Any())
            {
                var tasks = new Task[observers.Count];
                for (int i = 0; i < observers.Count; i++)
                {
                    tasks[i] = observers[i].Add(id);
                }

                return Task.WhenAll(tasks);
            }

            return Task.CompletedTask;
        }

        public Task Update<T>(dynamic id) where T : IEntity
        {
            var observers = _sp.GetServices<IEntityObserver<T>>().ToList();
            if (observers.Any())
            {
                var tasks = new Task[observers.Count];
                for (int i = 0; i < observers.Count; i++)
                {
                    tasks[i] = observers[i].Update(id);
                }

                return Task.WhenAll(tasks);
            }

            return Task.CompletedTask;
        }

        public Task Delete<T>(dynamic id) where T : IEntity
        {
            var observers = _sp.GetServices<IEntityObserver<T>>().ToList();
            if (observers.Any())
            {
                var tasks = new Task[observers.Count];
                for (int i = 0; i < observers.Count; i++)
                {
                    tasks[i] = observers[i].Delete(id);
                }

                return Task.WhenAll(tasks);
            }

            return Task.CompletedTask;
        }
    }
}
