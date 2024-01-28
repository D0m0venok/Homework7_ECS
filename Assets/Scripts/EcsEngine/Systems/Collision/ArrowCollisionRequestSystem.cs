using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Helpers;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class ArrowCollisionRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CollisionEnterRequest, ArrowTag, SourceEntity, TargetEntity>> _filter = EcsWorlds.Events;
        private readonly EcsWorldInject _world;
        private readonly EcsWorldInject _eventWorld = EcsWorlds.Events;
        private readonly EcsFactoryInject<TakeDamageRequest, SourceEntity, TargetEntity, Damage> _takeDamageEmitter = EcsWorlds.Events;
        
        private readonly EcsPoolInject<Damage> _damagePool;
        private readonly EcsPoolInject<DeathRequest> _deathRequestPool;
        private readonly EcsPoolInject<Health> _healthPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var sourceEntityPool = _filter.Pools.Inc3;
            var targetEntityPool = _filter.Pools.Inc4;
            
            foreach (var entity in _filter.Value)
            {
                var sourceEntity = sourceEntityPool.Get(entity);
                var arrow = sourceEntity.value;
                if (!_deathRequestPool.Value.Has(arrow))
                {
                    var targetEntity = targetEntityPool.Get(entity);
                    var target = targetEntity.value;
                    
                    if (_world.Value.IsEntityAlive(target) && _healthPool.Value.Has(target))
                        _takeDamageEmitter.Value.NewEntity(new TakeDamageRequest(), sourceEntity, targetEntity, _damagePool.Value.Get(arrow));
                    
                    _deathRequestPool.Value.Add(arrow) = new DeathRequest();
                }
                
                _eventWorld.Value.DelEntity(entity);
            }
        }
    }
}