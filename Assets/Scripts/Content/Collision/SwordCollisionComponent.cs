using EcsEngine;
using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    [RequireComponent(typeof(Collider))]
    public sealed class SwordCollisionComponent : MonoBehaviour
    {
        [SerializeField] private Entity _entity;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.enabled = false;
        }
        private void OnCollisionEnter(Collision collision)
        {
            var target = collision.gameObject.GetComponentInParent<Entity>();
            if(target == null)
                return;

            _collider.enabled = false;
            
            EcsAdmin.Instance.CreateEntity(EcsWorlds.Events)
                .Add(new CollisionEnterRequest())
                .Add(new SwordTag())
                .Add(new SourceEntity{value = _entity.Id})
                .Add(new TargetEntity{value = target.Id});
        }
    }
}