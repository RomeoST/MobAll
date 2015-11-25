namespace _EP4__MobAll_2.D3D
{
    using SlimDX;
    using System;
    using System.Runtime.InteropServices;

    internal class CustomVertex
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct PositionNormalTextured
        {
            public Vector3 Position;
            public Vector3 Normal;
            public Vector2 Texture;
        }
    }
}

