namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct pwBoneIndex
    {
        public byte A;
        public byte B;
        public byte C;
        public byte D;
        public pwBoneIndex(byte A, byte B, byte C, byte D)
        {
            this.A = A;
            this.B = B;
            this.C = C;
            this.D = D;
        }
    }
}

