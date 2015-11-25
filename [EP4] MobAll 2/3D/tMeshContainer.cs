namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.CompilerServices;

    internal class tMeshContainer
    {
        public byte[] FileName { get; set; }

        public string FilePath { get; set; }

        public tHeaderInfo HeaderInfo { get; set; }

        public tMeshMorphMap[] MorphMap { get; set; }

        public tVertex3f[] Normals { get; set; }

        public tMeshObject[] Objects { get; set; }

        public pwTextures[] PWTextureList { get; set; }

        public float Scale { get; set; }

        public tMeshUVMap[] UVMaps { get; set; }

        public uint Value1 { get; set; }

        public tVertex3f[] Vertices { get; set; }

        public tMeshJointWeights[] Weights { get; set; }
    }
}

