using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class ArrowLifeTimeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ArrowTag, Timer>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<DeathRequest> _deathRequestPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var timerPool = _filter.Pools.Inc2;
            
            foreach (var entity in _filter.Value)
            {
                ref var timer = ref timerPool.Get(entity);

                timer.remained -= Time.deltaTime;

                if(timer.remained >= 0)
                    continue;

                timer.remained = timer.time;
                
                _deathRequestPool.Value.Add(entity) = new DeathRequest();
            }
        }
    }
}