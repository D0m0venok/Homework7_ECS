using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public class SwordManAnimatorDispatcher : MonoBehaviour
    {
        [SerializeField] private Entity _entity;
        [SerializeField] private Collider _collider;
        
        public void StartAttack()
        {
            if(!_entity.IsAlive())
                return;
            
            _entity.AddData(new ArrowSpawnRequest());
            _collider.enabled = true;
        }
        public void EndAttack()
        {
            _collider.enabled = false;
        }
    }
}