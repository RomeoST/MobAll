namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct tMeshWeightsMap
    {
        public int Index;
        public float Weight;
        public tMeshWeightsMap(int Index, float Weight)
        {
            this.Index = Index;
            this.Weight = Weight;
        }
    }
}

