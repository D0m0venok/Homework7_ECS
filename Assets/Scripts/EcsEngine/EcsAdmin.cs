using System;
using Content;
using EcsEngine.Components;
using EcsEngine.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.Helpers;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace EcsEngine 
{
    public sealed class EcsAdmin : MonoBehaviour 
    {
        private EcsWorld _world;        
        private EcsWorld _events;        
        private IEcsSystems _systems;
        private EntityManager _entityManager;
        private UnitSpawner _unitsSpawner;
        private ArrowPool _arrowPool;
        
        public static EcsAdmin Instance { get; private set; }

        [NonSerialized]
        public bool IsPlay = true;
        
        [Inject]
        public void Construct(UnitSpawner unitsSpawner, ArrowPool arrowPool)
        {
            _unitsSpawner = unitsSpawner;
            _arrowPool = arrowPool;
        }
        private void Awake()
        {
            Instance = this;
            
            _entityManager = new EntityManager();
            _world = new EcsWorld();
            _events = new EcsWorld();
            _systems = new EcsSystems (_world);

            _systems.AddWorld(_events, EcsWorlds.Events);
            
            _systems
                .Add(new MovementSystem())
                .Add(new ObserverSystem())
                .Add(new MoveAgentSystem())
                .Add(new AttackAgentSystem())
                .Add(new AttackRequestSystem())
                .Add(new ArrowSpawnRequestSystem())
                .Add(new ArrowLifeTimeSystem())
                .Add(new UnitSpawnRequestSystem())
                .Add(new UnitSpawnerEventSystem())
                .Add(new TakeDamageRequestSystem())
                .Add(new HealthEmptySystem())
                .Add(new DeathRequestSystem())
                .Add(new ArrowCollisionRequestSystem())
                .Add(new SwordCollisionRequestSystem())
                .Add(new ArrowDestroySystem())
                .Add(new UnitDestroySystem())
                .Add(new BuildingDestroySystem())
                .Add(new TargetInactiveSystem())

                .Add(new UnityTransformSystem())
                .Add(new AnimatorStateController())
                .Add(new AnimatorTakeDamageListener())
                .Add(new AnimatorDeathListener())
                .Add(new ParticleUnitTakeDamageListener())
                .Add(new ParticleBuildingTakeDamageListener())
#if UNITY_EDITOR
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(EcsWorlds.Events))
#endif
                .Add(new OneFrameEventSystem())
                
                .DelHere<DeathEvent>()
                .DelHere<ArrowSpawnRequest>();
        }
        private void Start() 
        {
            _entityManager.Initialize(_world);
            
            _systems.Inject(_entityManager);
            _systems.Inject(_unitsSpawner);
            _systems.Inject(_arrowPool);
            _systems.Init();
        }

        private void Update() 
        {
            if(IsPlay)
                _systems?.Run();
        }

        private void OnDestroy() 
        {
            if (_systems != null) 
            {
                _systems.Destroy();
                _systems = null;
            }
            
            if (_world != null) 
            {
                _world.Destroy();
                _world = null;
            }
        }

        public EcsEntityBuilder CreateEntity(string worldName = null)
        {
            return new EcsEntityBuilder(_systems.GetWorld(worldName));
        }
        
        // public EcsWorld GetWorld(string worldName = null)
        // {
        //     return worldName switch
        //     {
        //         null => _world,
        //         EcsWorlds.Events => _events,
        //         _ => throw new Exception($"World with name {worldName} is not found!")
        //     };
        // }
    }
}