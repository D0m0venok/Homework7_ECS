using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class AttackAgentSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackAgent, MoveAgent, TargetEntity, Timer>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<AttackRequest> _attackRequestPool;
        private readonly EcsPoolInject<Inactive> _inactivePool;
        private readonly EcsWorldInject _world;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var moveAgentPool = _filter.Pools.Inc2;
            var targetPool = _filter.Pools.Inc3;
            var timerPool = _filter.Pools.Inc4;

            foreach (var entity in _filter.Value)
            {
                ref var moveAgent = ref moveAgentPool.Get(entity);
                ref var target = ref targetPool.Get(entity).value;
                ref var timer = ref timerPool.Get(entity);

                timer.remained -= Time.deltaTime;

                if(!moveAgent.isReached || timer.remained >= 0 || !_world.Value.IsEntityAlive(target))
                    continue;

                timer.remained = timer.time;
                _attackRequestPool.Value.Add(entity) = new AttackRequest{};
            }
        }
    }
}