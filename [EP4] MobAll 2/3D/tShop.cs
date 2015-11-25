namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class tShop
    {
        public object Clone()
        {
            return base.MemberwiseClone();
        }

        public int BuyRate { get; set; }

        public List<int> Items { get; set; }

        public int NpcID { get; set; }

        public int SellRate { get; set; }
    }
}

