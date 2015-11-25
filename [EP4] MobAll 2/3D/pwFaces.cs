namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct pwFaces
    {
        public short A;
        public short B;
        public short C;
        public pwFaces(short A, short B, short C)
        {
            this.A = A;
            this.B = B;
            this.C = C;
        }
    }
}

