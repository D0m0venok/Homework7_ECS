using EcsEngine.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace EcsEngine.Systems
{
    internal sealed class OneFrameEventSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<OneFrame>> _filter = EcsWorlds.Events;
        private readonly EcsWorldInject _eventworld = EcsWorlds.Events;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _eventworld.Value.DelEntity(entity);
            }
        }
    }
}