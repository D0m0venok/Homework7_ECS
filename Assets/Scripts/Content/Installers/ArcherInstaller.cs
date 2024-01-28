using EcsEngine;
using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public sealed class ArcherInstaller : EntityInstaller
    {
        [SerializeField] private Transform _spawnPoint;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new SpawnPoint{value = _spawnPoint});
        }
        protected override void Dispose(Entity entity)
        {
            
        }
    }
}