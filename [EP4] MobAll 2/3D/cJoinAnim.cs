namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.CompilerServices;

    internal class cJointAnim
    {
        public string JointName { get; set; }

        public int PositionCount { get; set; }

        public cPositionKeyFrame[] Positions { get; set; }

        public int RotationCount { get; set; }

        public cRotationKeyFrame[] Rotations { get; set; }

        public float Unknown { get; set; }
    }
}
