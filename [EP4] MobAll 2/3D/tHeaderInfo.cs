namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.CompilerServices;

    internal class tHeaderInfo
    {
        public byte[] Format { get; set; }

        public uint JointCount { get; set; }

        public uint MeshCount { get; set; }

        public int MeshDataSize { get; set; }

        public uint NormalCount { get; set; }

        public uint ObjCount { get; set; }

        public uint TextureMaps { get; set; }

        public uint UnknownCount { get; set; }

        public int Version { get; set; }

        public uint VertexCount { get; set; }
    }
}

