namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.CompilerServices;

    internal class tMeshJointWeights
    {
        public uint Count { get; set; }

        public byte[] JointName { get; set; }

        public tMeshWeightsMap[] WeightsMap { get; set; }
    }
}

