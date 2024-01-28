using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class TakeDamageRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TakeDamageRequest, TargetEntity, Damage>, Exc<Inactive>> _filter = EcsWorlds.Events;
        private readonly EcsPoolInject<TakeDamageEvent> _damageEventPool = EcsWorlds.Events;
        private readonly EcsPoolInject<OneFrame> _oneFramePool = EcsWorlds.Events;
        private readonly EcsPoolInject<Health> _healthPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var targetPool = _filter.Pools.Inc2;
            var damagePool = _filter.Pools.Inc3;
            
            foreach (var entity in _filter.Value)
            {
                ref var target = ref targetPool.Get(entity).value;
                ref var damage = ref damagePool.Get(entity).value;
                
                if (_healthPool.Value.Has(target))
                {
                    ref var health = ref _healthPool.Value.Get(target).value;
                    health = Mathf.Max(0, health - damage);
                }

                _filter.Pools.Inc1.Del(entity);
                _damageEventPool.Value.Add(entity) = new TakeDamageEvent();
                _oneFramePool.Value.Add(entity) = new OneFrame();
            }
        }
    }
}