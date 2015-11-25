namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal class SMCReader
    {
        public static List<smcMesh> ReadFile(string FileName)
        {
            int num;
            string[] source = Path.GetDirectoryName(FileName).Split(new char[] { '\\' });
            string str = "";
            bool flag = true;
            for (num = 0; num < source.Count<string>(); num++)
            {
                if (source[num].ToUpper() == "DATA")
                {
                    flag = false;
                }
                if (flag)
                {
                    str = str + source[num] + @"\";
                }
            }
            List<string> list = File.ReadAllLines(FileName).ToList<string>();
            for (int i = list.Count<string>() - 1; i >= 0; i--)
            {
                list[i] = list[i].Trim();
                list[i] = list[i].Replace("TFNM", "");
                if ((((list[i].Contains("{") || list[i].Contains("}")) || (list[i].Contains(",") || list[i].Contains("NAME"))) || ((list[i].Contains("COLISION") || list[i].Contains("TEXTURES")) || (list[i].Contains("ANIM") || list[i].Contains("SKELETON")))) || list[i].Contains("_TAG"))
                {
                    list.RemoveAt(i);
                }
            }
            int num3 = -1;
            List<smcMesh> list2 = new List<smcMesh>();
            for (num = 0; num < list.Count<string>(); num++)
            {
                try
                {
                    string[] strArray2;
                    if (list[num].Substring(0, 4) == "MESH")
                    {
                        num3++;
                        strArray2 = list[num].Split(new char[] { '"' });
                        list2.Add(new smcMesh(str + strArray2[1]));
                    }
                    else
                    {
                        strArray2 = list[num].Split(new char[] { '"' });
                        list2[num3].Object.Add(new smcObject(strArray2[1], str + strArray2[3]));
                    }
                }
                catch
                {
                }
            }
            return list2;
        }
    }
}

