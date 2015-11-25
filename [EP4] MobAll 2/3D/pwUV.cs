namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct pwUV
    {
        public float U;
        public float V;
        public pwUV(float U, float V)
        {
            this.U = U;
            this.V = V;
        }
    }
}

