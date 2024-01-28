using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace EcsEngine.Systems
{
    internal sealed class TargetInactiveSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TargetEntity>> _filter;
        private readonly EcsPoolInject<Inactive> _inactivePool;
        private readonly EcsWorldInject _world;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var targetPool = _filter.Pools.Inc1;
            foreach (var entity in _filter.Value)
            {
                ref var target = ref targetPool.Get(entity).value; 
                
                if(!_world.Value.IsEntityAlive(target) || _inactivePool.Value.Has(target))
                    targetPool.Del(entity);
            }
        }
    }
}