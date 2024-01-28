using Content;
using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class UnitSpawnerEventSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _eventWorld = EcsWorlds.Events;
        private readonly EcsFilterInject<Inc<UnitSpawnEvent, Position, Rotation, Team, UnityMaterial>> _filter = EcsWorlds.Events;
        private readonly EcsCustomInject<EntityManager> _entityManager;
        private readonly EcsCustomInject<UnitSpawner> _unitSpawner;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var eventPool = _filter.Pools.Inc1;
            var positionPool = _filter.Pools.Inc2;
            var rotationPool = _filter.Pools.Inc3;
            var teamPool = _filter.Pools.Inc4;
            var materialPool = _filter.Pools.Inc5;

            foreach (var @event in _filter.Value)
            {
                var unitType = eventPool.Get(@event).unitType;
                var position = positionPool.Get(@event).value;
                var rotation = rotationPool.Get(@event).value;
                var team = teamPool.Get(@event).value;
                var material = materialPool.Get(@event).value;
                
                var unit =  _unitSpawner.Value.Get(unitType);

                foreach (var child in unit.gameObject.GetComponentsInChildren<Collider>())
                {
                    child.gameObject.layer = team;   
                }
            
                _entityManager.Value.Add(unit);
            
                unit.GetData<Team>().value = team;
                unit.GetData<Position>().value = position;
                unit.GetData<Rotation>().value = rotation;
                ref var mesh = ref unit.GetData<UnitySkinnedMeshRenderer>();
                mesh.body.material = material;
                mesh.head.material = material;
                
                
                _eventWorld.Value.DelEntity(@event);
            }
        }
    }
}