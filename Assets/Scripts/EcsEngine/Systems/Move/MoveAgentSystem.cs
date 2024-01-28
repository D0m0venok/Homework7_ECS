using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class MoveAgentSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TargetEntity, Position, Rotation, MoveDirection, MoveAgent, AttackDistance>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<Position> _positionPool;
        private readonly EcsWorldInject _world;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var targetPool = _filter.Pools.Inc1;
            var positionPool = _filter.Pools.Inc2;
            var rotationPool = _filter.Pools.Inc3;
            var directionPool = _filter.Pools.Inc4;
            var moveAgentPool = _filter.Pools.Inc5;
            var attackDistancePool = _filter.Pools.Inc6;
            
            foreach (var entity in _filter.Value)
            {
                ref var target = ref targetPool.Get(entity).value;
                ref var position = ref positionPool.Get(entity).value;
                ref var rotation = ref rotationPool.Get(entity).value;
                ref var direction = ref directionPool.Get(entity).value; 
                ref var moveAgent = ref moveAgentPool.Get(entity);
                ref var distance = ref attackDistancePool.Get(entity).value;

                if (!_world.Value.IsEntityAlive(target))
                {
                    moveAgent.isReached = false;
                    continue;
                }
                
                var vector = _positionPool.Value.Get(target).value - position;

                rotation = Quaternion.LookRotation(vector);

                moveAgent.isReached = vector.magnitude <= distance;

                if (moveAgent.isReached)
                    vector = Vector3.zero;

                direction = vector.normalized;
            }
        }
    }
}