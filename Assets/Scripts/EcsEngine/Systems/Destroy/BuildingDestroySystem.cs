using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace EcsEngine.Systems
{
    internal sealed class BuildingDestroySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BuildingTag, Inactive, UnityBuildingParticles, UnityGameObject>> _filter;
        private readonly EcsCustomInject<EntityManager> _entityManager;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var particlePool = _filter.Pools.Inc3;
            var gameObject = _filter.Pools.Inc4;
            
            foreach (var entity in _filter.Value)
            {
                var particle = particlePool.Get(entity);
                foreach (var particleSystem in particle.fire)
                {
                    particleSystem.Stop(true);
                }
                
                particlePool.Get(entity).destroyed.Play(true);
                gameObject.Get(entity).value.SetActive(false);
                
                EcsAdmin.Instance.IsPlay = false;
                _entityManager.Value.Remove(entity);
            }
        }
    }
}