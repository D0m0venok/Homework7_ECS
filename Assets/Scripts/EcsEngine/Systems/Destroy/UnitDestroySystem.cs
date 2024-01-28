using Content;
using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace EcsEngine.Systems
{
    internal sealed class UnitDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnitType, DeathEvent>> _filter;
        private readonly EcsCustomInject<EntityManager> _entityManager;
        private readonly EcsCustomInject<UnitSpawner> _unitSpawner;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entityId in _filter.Value)
            {
                var entity = _entityManager.Value.Get(entityId);
                _unitSpawner.Value.Put(entity, () => _entityManager.Value.Remove(entityId));
            }
        }
    }
}