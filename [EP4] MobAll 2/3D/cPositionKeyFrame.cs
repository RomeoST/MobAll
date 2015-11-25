namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct cPositionKeyFrame
    {
        public short Frame;
        public short Flags;
        public float x;
        public float y;
        public float z;
        public cPositionKeyFrame(short Frame, short Flags, float x, float y, float z)
        {
            this.Frame = Frame;
            this.Flags = Flags;
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
