using System.Collections.Generic;
using UnityEngine;

namespace Leopotam.EcsLite.Entities
{
    public sealed class EntityManager
    {
        private EcsWorld _world;

        private readonly Dictionary<int, Entity> _entities = new();
        
        public void Initialize(EcsWorld world)
        {
            var entities = Object.FindObjectsOfType<Entity>();
            for (int i = 0, count = entities.Length; i < count; i++)
            {
                var entity = entities[i];
                entity.Initialize(world);
                _entities.Add(entity.Id, entity);
            }
            
            _world = world;
        }

        public void Add(Entity entity)
        {
            entity.Initialize(_world);
            _entities.Add(entity.Id, entity);
        }
        public void Remove(int id)
        {
            if (_entities.Remove(id, out var entity))
            {
                entity.Dispose();
            }
        }
        public Entity Create(Entity prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            var entity = Object.Instantiate(prefab, position, rotation, parent);
            entity.Initialize(_world);
            _entities.Add(entity.Id, entity);
            return entity;
        }
        public void Destroy(int id)
        {
            if (_entities.Remove(id, out Entity entity))
            {
                entity.Dispose();
                Object.Destroy(entity.gameObject);
            }
        }

        public Entity Get(int id)
        {
            return _entities[id];
        }
    }
}