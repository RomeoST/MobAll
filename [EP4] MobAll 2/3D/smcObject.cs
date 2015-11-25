namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct smcObject
    {
        public string Name;
        public string Texture;
        public smcObject(string Name, string Texture)
        {
            this.Name = Name;
            this.Texture = Texture;
        }
    }
}

