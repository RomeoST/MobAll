namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.CompilerServices;

    public class tFire
    {
        public object Clone()
        {
            return base.MemberwiseClone();
        }

        public float Delay0 { get; set; }

        public float Delay1 { get; set; }

        public float Delay2 { get; set; }

        public float Delay3 { get; set; }

        public byte DelayCount { get; set; }

        public string Effect0 { get; set; }

        public string Effect1 { get; set; }

        public string Effect2 { get; set; }

        public byte Object { get; set; }

        public float Speed { get; set; }
    }
}

