using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public sealed class SwordInstaller : EntityInstaller
    {
        [SerializeField] private int _damage = 1;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new SwordTag());
            entity.AddData(new Damage {value = _damage});
        }
        protected override void Dispose(Entity entity)
        {
            
        }
    }
}