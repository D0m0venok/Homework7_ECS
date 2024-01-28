using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<MoveDirection, MoveSpeed, Position>, Exc<Inactive>> _filter;
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var directionPool = _filter.Pools.Inc1;
            var speedPool = _filter.Pools.Inc2;
            var positionPool = _filter.Pools.Inc3;

            var deltaTime = Time.deltaTime;
            
            foreach (var entity in _filter.Value)
            {
                ref var position = ref positionPool.Get(entity);
                ref var direction = ref directionPool.Get(entity);
                ref var speed = ref speedPool.Get(entity);

                position.value += deltaTime * speed.value * direction.value;
            }
        }
    }
}