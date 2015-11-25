namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct tRGB
    {
        public byte R;
        public byte G;
        public byte B;
        public tRGB(byte R, byte G, byte B)
        {
            this.R = R;
            this.G = G;
            this.B = B;
        }
    }
}

