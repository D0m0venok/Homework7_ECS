using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    internal sealed class HealthEmptySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Health>, Exc<DeathRequest, Inactive>> _filter;
        private readonly EcsPoolInject<DeathRequest> _deathPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var healthPool = _filter.Pools.Inc1;
            
            foreach (var entity in _filter.Value)
            {
                var health = healthPool.Get(entity);

                if (health.value <= 0)
                    _deathPool.Value.Add(entity) = new DeathRequest();
            }
        }
    }
}