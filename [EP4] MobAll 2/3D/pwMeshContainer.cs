namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.CompilerServices;

    internal class pwMeshContainer
    {
        public pwHeaderInfo HeaderInfo { get; set; }

        public pwJoints[] Joints { get; set; }

        public pwMaterials[] Materials { get; set; }

        public pwObject[] Objects { get; set; }

        public pwTextures[] Textures { get; set; }
    }
}

