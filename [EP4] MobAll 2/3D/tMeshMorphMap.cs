namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct tMeshMorphMap
    {
        public byte[] JIndex;
        public byte[] Influence;
        public tMeshMorphMap(byte[] JIndex, byte[] Influence)
        {
            this.JIndex = JIndex;
            this.Influence = Influence;
        }
    }
}

