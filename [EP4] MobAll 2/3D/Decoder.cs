namespace _EP4__MobAll_2.D3D
{
    using System;

    internal class Decoder
    {
        private static byte[] XorCode;

        public static uint Decode(uint Code)
        {
            byte[] bytes = new byte[4];
            bytes = BitConverter.GetBytes(Code);
            for (int i = 0; i < 4; i++)
            {
                bytes[i] = (byte) (bytes[i] ^ XorCode[i]);
                XorCode[i] = (byte) (XorCode[i] + 0x59);
            }
            return BitConverter.ToUInt32(bytes, 0);
        }

        public static void Reset()
        {
            XorCode = new byte[] { 0x65, 0x48, 0x35, 30 };
        }
    }
}

