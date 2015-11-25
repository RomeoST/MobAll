using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Media.Imaging;
using FieryLib.Models;
using _EP4__MobAll_2.WindowWPF;

namespace _EP4__MobAll_2.Converters
{
    class IconConverter
    {
        private static Dictionary<int, Image> icons = new Dictionary<int, Image>();

        public static BitmapImage GetIconByItem(ItemAllLod lod)
        {
            return Icon(lod.TexID, lod.TexRow, lod.TexCol);
        }

        public static BitmapImage GetIconByItem(ItemInfo lod)
        {
            return Icon(lod.TexID, lod.TexRow, lod.TexCol);
        }

        public static BitmapImage GetIconByItem(DropAll.Items lod)
        {
            return Icon(lod.TexID, lod.TexRow, lod.TexCol);
        }

        public static BitmapImage Icon(int ID, int Row, int Col)
        {
            Image image;
            if (!icons.ContainsKey(ID))
            {
                try
                {
                    image = Image.FromFile("../IcoLcPng/ItemBtn" + ID.ToString() + ".png");
                    icons.Add(ID, image);
                }
                catch (System.Exception ex)
                {
                    return null;
                }
            }
            else
                image = icons[ID];
            Bitmap bitmap = new Bitmap(0x20, 0x20);
            Graphics graphics = Graphics.FromImage(bitmap);
            int y = Row * 0x20;
            int x = Col * 0x20;
            Rectangle srcRect = new Rectangle(x, y, 0x40, 0x40);
            graphics.DrawImage(image, 0, 0, srcRect, GraphicsUnit.Pixel);
            graphics.Dispose();
            return Bitmap2BitmapImage(bitmap);
        }

        private static BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                bitmap.Save(ms, ImageFormat.Png);
                ms.Position = 0;
                BitmapImage bi = new BitmapImage();
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.CacheOption = BitmapCacheOption.OnLoad;
                bi.EndInit();

                return bi;
            }
        }
    }
}
