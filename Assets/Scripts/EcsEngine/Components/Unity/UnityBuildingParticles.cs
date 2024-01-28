using System;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct UnityBuildingParticles
    {
        public ParticleSystem[] fire;
        public ParticleSystem destroyed;
    }
}