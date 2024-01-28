using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    internal sealed class ParticleUnitTakeDamageListener : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TakeDamageEvent, TargetEntity>> _filter = EcsWorlds.Events;
        private readonly EcsPoolInject<UnityBloodParticle> _particlePool;
        private readonly EcsWorldInject _world;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var targetPool = _filter.Pools.Inc2;
            foreach (var entity in _filter.Value)
            {
                ref var target = ref targetPool.Get(entity).value;
            
                if(!_world.Value.IsEntityAlive(target))
                    continue;
                
                if(_particlePool.Value.Has(target))
                    _particlePool.Value.Get(target).value.Play(true);
            }
        }
    }
}