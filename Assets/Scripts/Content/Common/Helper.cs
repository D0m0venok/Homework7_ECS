using EcsEngine;
using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Content
{
    public class Helper : MonoBehaviour
    {
        [SerializeField] private Entity _blueTeamBuild;
        [SerializeField] private Entity _redTeamBuild;
        
        [Button]
        public void SpawnBlueArcher()
        {
            _blueTeamBuild.AddData(new UnitSpawnRequest{unitType = (int)UnitType.Archer});
        }
        [Button]
        public void SpawnRedArcher()
        {
            _redTeamBuild.AddData(new UnitSpawnRequest{unitType = (int)UnitType.Archer});
        }
        [Button]
        public void SpawnBlueSwordMan()
        {
            _blueTeamBuild.AddData(new UnitSpawnRequest{unitType = (int)UnitType.SwordMan});
        }
        [Button]
        public void SpawnRedSwordMan()
        {
            _redTeamBuild.AddData(new UnitSpawnRequest{unitType = (int)UnitType.SwordMan});
        }
    }
}