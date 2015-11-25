namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.CompilerServices;

    internal class pwHeaderInfo
    {
        public byte[] Format { get; set; }

        public uint JointCount { get; set; }

        public uint MaterialCount { get; set; }

        public uint MeshCount { get; set; }

        public uint TextureCount { get; set; }

        public uint TypeMask { get; set; }

        public uint UnknownValue1 { get; set; }

        public uint UnknownValue2 { get; set; }

        public uint UnknownValue3 { get; set; }

        public int Version { get; set; }

        public uint VertexType { get; set; }
    }
}

