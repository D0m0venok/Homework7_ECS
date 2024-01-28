using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class AttackRequestSystem : IEcsRunSystem
    {
        private static readonly int Attack = Animator.StringToHash("Attack");
        
        private readonly EcsFilterInject<Inc<AttackRequest, UnityAnimator>, Exc<Inactive>> _filter;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var requestPool = _filter.Pools.Inc1;
            var animatorPool = _filter.Pools.Inc2;
            
            foreach (var entity in _filter.Value)
            {
                requestPool.Del(entity);
                animatorPool.Get(entity).value.SetTrigger(Attack);
            }
        }
    }
}