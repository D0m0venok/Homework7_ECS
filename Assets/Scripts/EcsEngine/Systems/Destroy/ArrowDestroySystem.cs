using Content;
using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace EcsEngine.Systems
{
    internal sealed class ArrowDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ArrowTag, Inactive>> _filter;
        private readonly EcsCustomInject<EntityManager> _entityManager;
        private readonly EcsCustomInject<ArrowPool> _arrowPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entityId in _filter.Value)
            {
                var entity = _entityManager.Value.Get(entityId);
                _entityManager.Value.Remove(entityId);
                _arrowPool.Value.Put(entity);
            }
        }
    }
}