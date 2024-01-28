using EcsEngine;
using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public class BuildingInstaller : EntityInstaller
    {
        [SerializeField] private int _health = 15;
        [SerializeField] private TeamType _team;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private Transform _damagePoint;
        [SerializeField] private Material _material;
        [SerializeField] private GameObject _model;
        [SerializeField] private ParticleSystem[] _fire;
        [SerializeField] private ParticleSystem _destroyed;

        protected override void Install(Entity entity)
        {
            entity.AddData(new BuildingTag());
            entity.AddData(new Health {initValue = _health, value = _health});
            entity.AddData(new SpawnPoint {value = _spawnPoint});
            entity.AddData(new Team{value = (int)_team});
            entity.AddData(new UnityMaterial{value = _material});
            entity.AddData(new Position{value = _damagePoint.position});
            entity.AddData(new UnityBuildingParticles{fire = _fire, destroyed = _destroyed});
            entity.AddData(new UnityGameObject{value = _model});
        }
        protected override void Dispose(Entity entity)
        {
            
        }
    }
}