using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public class ArcherAnimatorDispatcher : MonoBehaviour
    {
        [SerializeField] private Entity _entity;
        
        public void Attack()
        {
            if(_entity.IsAlive())
                _entity.AddData(new ArrowSpawnRequest());
        }
    }
}