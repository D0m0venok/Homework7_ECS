using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    internal sealed class ParticleBuildingTakeDamageListener : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TakeDamageEvent, TargetEntity>> _filter = EcsWorlds.Events;
        private readonly EcsPoolInject<UnityBuildingParticles> _particlePool;
        private readonly EcsPoolInject<Health> _healthPool;
        private readonly EcsWorldInject _world;
    
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var targetPool = _filter.Pools.Inc2;
            foreach (var entity in _filter.Value)
            {
                var target = targetPool.Get(entity).value;
            
                if(!_world.Value.IsEntityAlive(target))
                    continue;

                if (_particlePool.Value.Has(target))
                {
                    var particle = _particlePool.Value.Get(target);
                    var health = _healthPool.Value.Get(target);
                    
                    if(health.value <= health.initValue * 0.4f)
                        particle.fire[0].Play(true);
                    else if(health.value <= health.initValue * 0.75f)
                        particle.fire[1].Play(true);
                }
            }
        }
    }
}