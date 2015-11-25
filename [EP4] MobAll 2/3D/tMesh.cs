namespace _EP4__MobAll_2.D3D
{
    using SlimDX.Direct3D9;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct tMesh
    {
        public Mesh MeshData;
        public Texture TexData;
        public tMesh(Mesh mesh, Texture texture)
        {
            this.MeshData = mesh;
            this.TexData = texture;
        }
    }
}

