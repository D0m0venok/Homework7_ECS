using System;

namespace EcsEngine.Components
{
    [Serializable]
    public struct Health
    {
        public int initValue;
        public int value;
    }
}