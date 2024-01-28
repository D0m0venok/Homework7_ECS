using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public sealed class ArrowInstaller : EntityInstaller
    {
        [SerializeField] private int _damage = 1;
        [SerializeField] private float _moveSpeed = 15;
        [SerializeField] private float _lifeTime = 5;
         
        protected override void Install(Entity entity)
        {
            entity.AddData(new ArrowTag());
            entity.AddData(new Position {value = transform.position});
            entity.AddData(new MoveDirection());
            entity.AddData(new Rotation());
            entity.AddData(new MoveSpeed {value = _moveSpeed});
            entity.AddData(new UnityTransform {value = transform});
            entity.AddData(new Damage {value = _damage});
            entity.AddData(new Timer {time = _lifeTime, remained = _lifeTime});
        }
        protected override void Dispose(Entity entity)
        {
            
        }
    }
}