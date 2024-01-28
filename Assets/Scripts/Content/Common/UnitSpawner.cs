using System;
using DG.Tweening;
using EcsEngine;
using EcsEngine.Components;
using JetBrains.Annotations;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    [UsedImplicitly]
    public sealed class UnitSpawner
    {
        private readonly GameObjectsPool<Entity> _archerPool;
        private readonly GameObjectsPool<Entity> _swordManPool;
        
        public UnitSpawner(Entity archer, Entity swordMan, Transform activeContainer, Transform disableContainer)
        {
            _archerPool = new GameObjectsPool<Entity>(archer, activeContainer, disableContainer);
            _swordManPool = new GameObjectsPool<Entity>(swordMan, activeContainer, disableContainer);
        }
        public Entity Get(int type)
        {
            switch ((UnitType)type)
            {
                case UnitType.Archer:
                    return _archerPool.Get();
                case UnitType.SwordMan:
                    return _swordManPool.Get();
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        public void Put(Entity entity, Action action)
        {
            var type = entity.GetData<UnitType>();
            entity.transform.DOMoveY(-2, 2).SetDelay(1).OnComplete(() =>
            {
                action?.Invoke();
                switch (type)
                {
                    case UnitType.Archer:
                        _archerPool.Put(entity);
                        break;
                    case UnitType.SwordMan:
                        _swordManPool.Put(entity);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
        }
    }
}