namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct tMeshTexture
    {
        public byte[] InternalName;
        public int Reserverd;
        public tMeshTexture(byte[] Name)
        {
            this.InternalName = Name;
            this.Reserverd = 0;
        }
    }
}

