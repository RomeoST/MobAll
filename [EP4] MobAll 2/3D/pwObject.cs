namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.CompilerServices;

    internal class pwObject
    {
        public pwBoneIndex[] BoneIndex { get; set; }

        public byte[] ExtraData { get; set; }

        public pwFaces[] Faces { get; set; }

        public int FaceVertsCount { get; set; }

        public int MaterialIndex { get; set; }

        public byte[] MeshName { get; set; }

        public pwNormal[] Normals { get; set; }

        public int TextureIndex { get; set; }

        public pwUV[] UVs { get; set; }

        public int VertexCount { get; set; }

        public pwVertexWeight[] VertexWeight { get; set; }

        public pwVertex[] Vertices { get; set; }
    }
}

