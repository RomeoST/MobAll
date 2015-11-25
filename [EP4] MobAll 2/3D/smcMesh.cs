namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct smcMesh
    {
        public string FileName;
        public List<smcObject> Object;
        public smcMesh(string FileName)
        {
            this.FileName = FileName;
            this.Object = new List<smcObject>();
        }
    }
}

