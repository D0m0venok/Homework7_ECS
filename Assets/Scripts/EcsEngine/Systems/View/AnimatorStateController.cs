using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class AnimatorStateController : IEcsRunSystem
    {
        private static readonly int State = Animator.StringToHash("State");
        private static readonly int IDLE = 0;
        private static readonly int MOVE = 1;

        private readonly EcsFilterInject<Inc<UnityAnimator, MoveDirection>, Exc<Inactive>> _filter;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var animatorPool = _filter.Pools.Inc1;
            var directionPool = _filter.Pools.Inc2;
            foreach (var entity in _filter.Value)
            {
                ref var animator = ref animatorPool.Get(entity).value;
                ref var direction = ref directionPool.Get(entity).value;
                animator.SetInteger(State, direction == Vector3.zero ? IDLE : MOVE);
            }
        }
    }
}