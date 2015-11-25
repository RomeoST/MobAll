namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct cRotationKeyFrame
    {
        public short Frame;
        public short Flags;
        public float w;
        public float x;
        public float y;
        public float z;
        public cRotationKeyFrame(short Frame, short Flags, float w, float x, float y, float z)
        {
            this.Frame = Frame;
            this.Flags = Flags;
            this.w = w;
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
