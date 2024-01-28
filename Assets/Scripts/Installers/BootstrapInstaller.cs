using Content;
using UnityEngine;
using Zenject;
using EcsEngine;
using Leopotam.EcsLite.Entities;

namespace Installers
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private Entity _archerPrefab;
        [SerializeField] private Entity _swordManPrefab;
        [SerializeField] private Transform _activeUnitsContainer;
        [SerializeField] private Transform _disableUnitsContainer;
        [SerializeField] private Entity _arrowPrefab;
        [SerializeField] private Transform _activeArrowsContainer;
        [SerializeField] private Transform _disableArrowsContainer;
        
        public override void InstallBindings()
        {
            Container.Bind<EcsAdmin>().FromInstance(EcsAdmin.Instance).AsSingle();
            Container.Bind<EntityManager>().AsSingle();
            Container.Bind<UnitSpawner>().AsSingle().WithArguments(_archerPrefab, _swordManPrefab, _activeUnitsContainer, _disableUnitsContainer);
            Container.Bind<ArrowPool>().AsSingle().WithArguments(_arrowPrefab, _activeArrowsContainer, _disableArrowsContainer);
        }
    }
}