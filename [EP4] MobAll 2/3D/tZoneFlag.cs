namespace _EP4__MobAll_2.D3D
{
    using System;

    internal class tZoneFlag
    {
        public uint Extra;
        public uint Flag;
        public int NpcID;

        public tZoneFlag(int NpcID, uint Flag, uint Extra)
        {
            this.NpcID = NpcID;
            this.Flag = Flag;
            this.Extra = Extra;
        }
    }
}

