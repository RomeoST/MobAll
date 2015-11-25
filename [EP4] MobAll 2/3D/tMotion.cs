namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.CompilerServices;

    public class tMotion
    {
        public object Clone()
        {
            return base.MemberwiseClone();
        }

        public string Attack { get; set; }

        public string Attack2 { get; set; }

        public string Damage { get; set; }

        public string Die { get; set; }

        public string Idle { get; set; }

        public string Idle2 { get; set; }

        public string Run { get; set; }

        public string Walk { get; set; }
    }
}

