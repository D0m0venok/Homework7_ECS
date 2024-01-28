using JetBrains.Annotations;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    [UsedImplicitly]
    public sealed class ArrowPool : GameObjectsPool<Entity>
    {
        
        public ArrowPool(Entity prefab, Transform activeContainer = null, Transform disableContainer = null) : base(prefab, activeContainer, disableContainer)
        {
        }
    }
}