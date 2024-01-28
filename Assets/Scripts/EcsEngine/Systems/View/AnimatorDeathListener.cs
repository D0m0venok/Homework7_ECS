using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class AnimatorDeathListener : IEcsRunSystem
    {
        private static readonly int Death = Animator.StringToHash("Death");
        
        private readonly EcsFilterInject<Inc<UnityAnimator, DeathEvent>> _filter;

        private readonly EcsPoolInject<DeathEvent> _eventPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var animatorPool = _filter.Pools.Inc1;
            foreach (var entity in _filter.Value)
            {
                ref var animator = ref animatorPool.Get(entity).value;
                animator.SetTrigger(Death);
            }
        }
    }
}