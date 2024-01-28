using Content;
using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace EcsEngine.Systems
{
    internal sealed class ArrowSpawnRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ArrowSpawnRequest, Team, SpawnPoint, TargetEntity>> _filter;
        private readonly EcsPoolInject<TargetEntity> _targetPool;
        private readonly EcsPoolInject<Position> _positionPool;
        private readonly EcsCustomInject<EntityManager> _entityManager;
        private readonly EcsCustomInject<ArrowPool> _arrowPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var teamPool = _filter.Pools.Inc2;
            var pointPool = _filter.Pools.Inc3;
            var targetPool = _filter.Pools.Inc4;

            foreach (var entity in _filter.Value)
            {
                ref var team = ref teamPool.Get(entity).value;
                ref var point = ref pointPool.Get(entity).value;
                ref var target = ref targetPool.Get(entity).value;
                
                var arrow = _arrowPool.Value.Get();
                arrow.gameObject.layer = team;

                _entityManager.Value.Add(arrow);
                var pos = point.position;
                arrow.GetData<Position>().value = pos;
                arrow.GetData<Rotation>().value = point.rotation;

                var targetPos = _positionPool.Value.Get(target).value;
                
                pos.y = 0;
                arrow.GetData<MoveDirection>().value = (targetPos - pos).normalized;
            }
        }
    }
}