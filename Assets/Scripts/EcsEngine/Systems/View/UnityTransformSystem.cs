using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    internal sealed class UnityTransformSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnityTransform, Position>> _filter;
        private readonly EcsPoolInject<Rotation> _rotationPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var unityTransformPool = _filter.Pools.Inc1;
            var positionPool = _filter.Pools.Inc2;
            var rotationPool = _rotationPool.Value;
            
            foreach (var entity in _filter.Value)
            {
                ref var unityTransform = ref unityTransformPool.Get(entity);
                ref var position = ref positionPool.Get(entity).value;

                unityTransform.value.position = position;

                if (_rotationPool.Value.Has(entity))
                    unityTransform.value.rotation = rotationPool.Get(entity).value;
            }
        }
    }
}