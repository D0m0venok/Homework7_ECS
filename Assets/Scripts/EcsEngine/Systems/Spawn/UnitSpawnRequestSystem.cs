using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    internal sealed class UnitSpawnRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnitSpawnRequest, Team, SpawnPoint, UnityMaterial>> _filter;

        private readonly EcsWorldInject _eventWorld = EcsWorlds.Events;
        private readonly EcsPoolInject<UnitSpawnEvent> _spawnPool = EcsWorlds.Events;
        private readonly EcsPoolInject<Position> _positionPool = EcsWorlds.Events;
        private readonly EcsPoolInject<Rotation> _rotationPool = EcsWorlds.Events;
        private readonly EcsPoolInject<Team> _teamEventPool = EcsWorlds.Events;
        private readonly EcsPoolInject<UnityMaterial> _materialPool = EcsWorlds.Events;
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var requestPool = _filter.Pools.Inc1;
            var teamPool = _filter.Pools.Inc2;
            var transformPool = _filter.Pools.Inc3;
            var materialPool = _filter.Pools.Inc4;

            foreach (var entity in _filter.Value)
            {
                var request = requestPool.Get(entity);
                var team = teamPool.Get(entity);
                var transform = transformPool.Get(entity);
                var material = materialPool.Get(entity);
                
                var spawnEvent = _eventWorld.Value.NewEntity();
                
                _spawnPool.Value.Add(spawnEvent) = new UnitSpawnEvent{unitType = request.unitType};

                _positionPool.Value.Add(spawnEvent) = new Position {value = transform.value.position};
                _rotationPool.Value.Add(spawnEvent) = new Rotation {value = transform.value.rotation};
                _teamEventPool.Value.Add(spawnEvent) = new Team { value = team.value };
                _materialPool.Value.Add(spawnEvent) = new UnityMaterial { value = material.value };
                
                requestPool.Del(entity);
            }
        }
    }
}