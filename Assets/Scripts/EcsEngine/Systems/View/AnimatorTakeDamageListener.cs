using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class AnimatorTakeDamageListener : IEcsRunSystem
    {
        private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");
        
        private readonly EcsFilterInject<Inc<TakeDamageEvent, TargetEntity>> _filter = EcsWorlds.Events;

        private readonly EcsPoolInject<UnityAnimator> _animatorPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            // var targetPool = _filter.Pools.Inc2;
            // foreach (var entity in _filter.Value)
            // {
            //     var target = targetPool.Get(entity).value;
            //
            //     if (_animatorPool.Value.Has(target))
            //         _animatorPool.Value.Get(target).value.SetTrigger(TakeDamage);
            // }
        }
    }
}