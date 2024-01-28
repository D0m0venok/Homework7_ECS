using System;
using UnityEngine;

namespace EcsEngine.Components
{
    [Serializable]
    public struct UnitySkinnedMeshRenderer
    {
        public SkinnedMeshRenderer body;
        public MeshRenderer head;
    }
}