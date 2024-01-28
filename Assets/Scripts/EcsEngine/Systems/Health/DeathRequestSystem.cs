using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    internal sealed class DeathRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeathRequest>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<DeathEvent> _eventPool;
        private readonly EcsPoolInject<Inactive> _tagPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var deathPool = _filter.Pools.Inc1;
            
            foreach (var entity in _filter.Value)
            {
                _eventPool.Value.Add(entity) = new DeathEvent();
                _tagPool.Value.Add(entity) = new Inactive();
                
                deathPool.Del(entity);
            }
        }
    }
}