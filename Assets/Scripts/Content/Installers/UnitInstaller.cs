using EcsEngine;
using EcsEngine.Components;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace Content
{
    public sealed class UnitInstaller : EntityInstaller
    {
        [SerializeField] private int _health = 5;
        [SerializeField] private int _moveSpeed = 5;
        [SerializeField] private float _attackDellay = 1;
        [SerializeField] private float _attackdistance = 15f;
        [SerializeField] private Animator _animator; 
        [SerializeField] private SkinnedMeshRenderer _bodyMeshRenderer;
        [SerializeField] private MeshRenderer _headMeshRenderer;
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private UnitType _unitType;
    
        protected override void Install(Entity entity)
        {
            entity.AddData(new Position {value = transform.position});
            entity.AddData(new Rotation {value = transform.rotation});
            entity.AddData(new MoveDirection());
            entity.AddData(new MoveSpeed {value = _moveSpeed});
            entity.AddData(new MoveAgent());
            entity.AddData(new AttackAgent());
            entity.AddData(new Timer{time = _attackDellay});
            entity.AddData(new Health {value = _health});
            entity.AddData(new AttackDistance {value = _attackdistance});
            entity.AddData(new Team());
            entity.AddData(new UnitySkinnedMeshRenderer{body = _bodyMeshRenderer, head = _headMeshRenderer});
            entity.AddData(new UnityBloodParticle{value = _particle});
            entity.AddData(new Observer());
            entity.AddData(_unitType);
            
            entity.AddData(new UnityTransform {value = transform});
            entity.AddData(new UnityAnimator() {value = _animator});
        }
        protected override void Dispose(Entity entity)
        {
            
        }
    }
}