using EcsEngine;
using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public sealed class ArrowCollisionComponent : MonoBehaviour
    {
        [SerializeField] private Entity _entity;
        
        private void OnCollisionEnter(Collision collision)
        {
            var target = collision.gameObject.GetComponentInParent<Entity>();
            if(target == null)
                return;

            if(!_entity.IsAlive() || _entity.HasData<DeathRequest>() || _entity.HasData<Inactive>())
                return;
            
            EcsAdmin.Instance.CreateEntity(EcsWorlds.Events)
                .Add(new CollisionEnterRequest())
                .Add(new ArrowTag())
                .Add(new SourceEntity{value = _entity.Id})
                .Add(new TargetEntity{value = target.Id});
        }
    }
}