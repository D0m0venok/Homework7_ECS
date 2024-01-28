using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class ObserverSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Observer, Team, Position>, Exc<TargetEntity, Inactive>> _observerfilter;
        private readonly EcsFilterInject<Inc<Team, Position, Health>, Exc<Inactive>> _targetFilter;
        private readonly EcsPoolInject<TargetEntity> _targetPool;
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var observerTeamPool = _observerfilter.Pools.Inc2;
            var observerPositionPool = _observerfilter.Pools.Inc3;
            
            var targetTeamPool = _targetFilter.Pools.Inc1;
            var positionPool = _targetFilter.Pools.Inc2;
            
            var minDistance = float.MaxValue;
            var target = -1;
            
            foreach (var observerEntity in _observerfilter.Value)
            {
                var observerTeam = observerTeamPool.Get(observerEntity).value;
                var observerPosition = observerPositionPool.Get(observerEntity).value;
                
                foreach (var targetEntity in _targetFilter.Value)
                {
                    if(observerEntity == targetEntity)
                        continue;
                    
                    var targetTeam = targetTeamPool.Get(targetEntity).value;
                    if(observerTeam == targetTeam)
                        continue;

                    var targetPosition = positionPool.Get(targetEntity).value;
                    var distance = Vector3.Distance(observerPosition, targetPosition);

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        target = targetEntity;
                    }
                }

                if (target >= 0)
                    _targetPool.Value.Add(observerEntity) = new TargetEntity() { value = target };
            }
        }
    }
}