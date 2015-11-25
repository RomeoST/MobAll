namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    internal class tMeshObject
    {
        public short[] GetFaces()
        {
            List<short> list = new List<short>();
            for (int i = 0; i < this.Faces.Count<tFace>(); i++)
            {
                list.Add(this.Faces[i].A);
                list.Add(this.Faces[i].B);
                list.Add(this.Faces[i].C);
            }
            return list.ToArray();
        }

        public uint FaceCount { get; set; }

        public tFace[] Faces { get; set; }

        public uint FromVert { get; set; }

        public byte[] JData { get; set; }

        public uint JValue { get; set; }

        public byte[] MaterialName { get; set; }

        public tMeshShaderData ShaderData { get; set; }

        public uint ShaderFlag { get; set; }

        public tMeshShaderInfo ShaderInfo { get; set; }

        public tMeshTexture[] Textures { get; set; }

        public uint ToVert { get; set; }

        public uint Value1 { get; set; }
    }
}

