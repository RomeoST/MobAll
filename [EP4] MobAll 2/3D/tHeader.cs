namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.CompilerServices;

    internal class tHeader
    {
        public int AnimOffset { get; set; }

        public uint Bits { get; set; }

        public byte[] DataChunk { get; set; }

        public string Format { get; set; }

        public uint Height { get; set; }

        public uint MipMap { get; set; }

        public uint Shift { get; set; }

        public uint Unknown { get; set; }

        public int Version { get; set; }

        public byte[] VersionChunk { get; set; }

        public uint Width { get; set; }
    }
}

