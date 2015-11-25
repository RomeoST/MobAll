namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Runtime.CompilerServices;

    public class tNpc
    {
        public tNpc Clone()
        {
            tNpc npc = (tNpc)base.MemberwiseClone();
            npc.Fire = (tFire)Fire.Clone();
            npc.Motion = (tMotion)Motion.Clone();
            return npc;
        }

        public float AttackArea { get; set; }

        public int AttackSpeed { get; set; }

        public byte AttackType { get; set; }

        public uint ExtraFlag { get; set; }

        public tFire Fire { get; set; }

        public int Flag1 { get; set; }

        public int Flag2 { get; set; }

        public int Health { get; set; }

        public int Level { get; set; }

        public int Mana { get; set; }

        public tMotion Motion { get; set; }

        public string Name { get; set; }

        public int NoIdea1 { get; set; }

        public int NoIdea2 { get; set; }

        public int NoIdea3 { get; set; }

        public int NoIdea4 { get; set; }

        public int NoIdea5 { get; set; }

        public int NpcID { get; set; }

        public float RunSpeed { get; set; }

        public float Scale { get; set; }

        public float Size { get; set; }

        public int SkillID1 { get; set; }

        public int SkillID2 { get; set; }

        public byte SkillLevel1 { get; set; }

        public byte SkillLevel2 { get; set; }

        public short SkillMaster { get; set; }

        public string SMC { get; set; }

        public float WalkSpeed { get; set; }

        public uint ZoneFlag { get; set; }
    }
}

