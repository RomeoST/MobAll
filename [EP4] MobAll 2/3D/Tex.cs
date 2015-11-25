namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class Tex
    {
        public static tTexture lcTex;

        public static texFormat GetFormat()
        {
            texFormat uNKNOWN = texFormat.UNKNOWN;
            if (lcTex.Header.Format == "FRMC")
            {
                if ((lcTex.Header.Bits == 4) || (lcTex.Header.Bits == 13))
                {
                    return texFormat.DXT1;
                }
                return texFormat.DXT3;
            }
            if (!(lcTex.Header.Format == "FRMS"))
            {
                return uNKNOWN;
            }
            if ((lcTex.Header.Bits == 0) || (lcTex.Header.Bits == 2))
            {
                return texFormat.RGB;
            }
            return texFormat.ARGB;
        }

        public static Bitmap makeRGB(byte[] data2, int width, int height)
        {
            List<byte> source = new List<byte>();
            for (int i = 0; i < data2.Length; i += 4)
            {
                source.Add(data2[i + 2]);
                source.Add(data2[i + 1]);
                source.Add(data2[i]);
                source.Add(0xff);
            }
            try
            {
                Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
                Marshal.Copy(source.ToArray(), 0, bitmapdata.Scan0, source.Count<byte>());
                bitmap.UnlockBits(bitmapdata);
                return bitmap;
            }
            catch
            {
                return new Bitmap(width, height);
            }
        }

        public static Bitmap makeRGB8(byte[] data2, int width, int height)
        {
            List<byte> source = new List<byte>();
            for (int i = 0; i < data2.Length; i += 3)
            {
                source.Add(data2[i + 1]);
                source.Add(data2[i]);
                source.Add(data2[i + 2]);
            }
            try
            {
                Bitmap bitmap = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                BitmapData bitmapdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
                Marshal.Copy(source.ToArray(), 0, bitmapdata.Scan0, source.Count<byte>());
                bitmap.UnlockBits(bitmapdata);
                return bitmap;
            }
            catch
            {
                return new Bitmap(width, height);
            }
        }

        public static void ReadFile(string FileName)
        {
            Encoding encoding = Encoding.GetEncoding(0x4e3);
            FileStream input = new FileStream(FileName, FileMode.Open);
            BinaryReader b = new BinaryReader(input);
            lcTex = new tTexture();
            lcTex.Header = new tHeader();
            lcTex.Header.VersionChunk = b.ReadBytes(4);
            lcTex.Header.Version = b.ReadInt32();
            lcTex.Header.DataChunk = b.ReadBytes(4);
            lcTex.Header.Width = b.ReadUInt32() ^ 0x12143d3e;
            lcTex.Header.Shift = b.ReadUInt32() ^ 0x55578081;
            lcTex.Header.Height = b.ReadUInt32() ^ 0x989ac3c4;
            lcTex.Header.MipMap = b.ReadUInt32() ^ 0xdbdd0607;
            lcTex.Header.Bits = b.ReadUInt32() ^ 0x1e20494a;
            lcTex.Header.Unknown = b.ReadUInt32() ^ 0x61638c8d;
            lcTex.Header.Format = encoding.GetString(b.ReadBytes(4));
            lcTex.Header.AnimOffset = b.ReadInt32();
            lcTex.Header.Width = Shift(lcTex.Header.Width, lcTex.Header.Shift);
            lcTex.Header.Height = Shift(lcTex.Header.Height, lcTex.Header.Shift);
            if (lcTex.Header.Format == "FRMC")
            {
                ReadFRMC(lcTex, b);
            }
            else if (lcTex.Header.Format == "FRMS")
            {
                ReadFRMS(lcTex, b);
            }
            input.Close();
        }

        private static void ReadFRMC(tTexture lcTex, BinaryReader b)
        {
            lcTex.imageData = new byte[lcTex.Header.MipMap][];
            for (int i = 0; i < lcTex.Header.MipMap; i++)
            {
                lcTex.imageData[i] = b.ReadBytes(b.ReadInt32());
            }
        }

        private static void ReadFRMS(tTexture lcTex, BinaryReader b)
        {
            int count = (int) (lcTex.Header.Width * lcTex.Header.Height);
            if ((lcTex.Header.Bits == 0) || (lcTex.Header.Bits == 2))
            {
                count *= 3;
            }
            else
            {
                count *= 4;
            }
            lcTex.imageData = new byte[lcTex.Header.MipMap][];
            for (int i = 0; i < lcTex.Header.MipMap; i++)
            {
                lcTex.imageData[i] = b.ReadBytes(count);
            }
        }

        private static uint Shift(uint Val, uint Shifter)
        {
            Val = (uint)((int)Val >> (int)Shifter);
            return Val;
        }
    }
}

