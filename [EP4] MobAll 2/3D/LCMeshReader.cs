namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.IO;
    using System.Text;

    internal class LCMeshReader
    {
        private static Encoding encoding = Encoding.GetEncoding(0x4e3);
        public static string OpenedFile = "";
        public static tMeshContainer pMesh;

        public static bool ReadFile(string FileName)
        {
            OpenedFile = FileName;
            pMesh = new tMeshContainer();
            FileStream input = new FileStream(FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            BinaryReader b = new BinaryReader(input);
            pMesh.HeaderInfo = new tHeaderInfo();
            pMesh.HeaderInfo.Format = b.ReadBytes(4);
            pMesh.HeaderInfo.Version = b.ReadInt32();
            pMesh.HeaderInfo.MeshDataSize = b.ReadInt32();
            pMesh.HeaderInfo.MeshCount = b.ReadUInt32();
            pMesh.HeaderInfo.VertexCount = b.ReadUInt32();
            pMesh.HeaderInfo.JointCount = b.ReadUInt32();
            pMesh.HeaderInfo.TextureMaps = b.ReadUInt32();
            pMesh.HeaderInfo.NormalCount = b.ReadUInt32();
            pMesh.HeaderInfo.ObjCount = b.ReadUInt32();
            pMesh.HeaderInfo.UnknownCount = b.ReadUInt32();
            pMesh.FileName = b.ReadBytes(b.ReadInt32());
            pMesh.Scale = b.ReadSingle();
            pMesh.Value1 = b.ReadUInt32();
            pMesh.FilePath = FileName;
            bool flag = false;
            if (pMesh.HeaderInfo.Version == 0x10)
            {
                if (ReadV10(b, b.BaseStream.Position))
                {
                    flag = true;
                }
            }
            else if ((pMesh.HeaderInfo.Version == 0x11) && ReadV11(b, b.BaseStream.Position))
            {
                flag = true;
            }
            b.Close();
            return flag;
        }

        private static bool ReadV10(BinaryReader b, long Pos)
        {
            int num;
            int num2;
            tHeaderInfo headerInfo = new tHeaderInfo();
            headerInfo = pMesh.HeaderInfo;
            headerInfo.NormalCount = pMesh.HeaderInfo.UnknownCount;
            headerInfo.JointCount = pMesh.HeaderInfo.NormalCount;
            headerInfo.UnknownCount = pMesh.HeaderInfo.TextureMaps;
            headerInfo.ObjCount = pMesh.HeaderInfo.ObjCount;
            headerInfo.TextureMaps = pMesh.HeaderInfo.JointCount;
            pMesh.HeaderInfo = headerInfo;
            pMesh.Vertices = new tVertex3f[pMesh.HeaderInfo.VertexCount];
            for (num = 0; num < pMesh.HeaderInfo.VertexCount; num++)
            {
                pMesh.Vertices[num] = new tVertex3f(b.ReadSingle(), b.ReadSingle(), b.ReadSingle());
            }
            pMesh.Normals = new tVertex3f[pMesh.HeaderInfo.VertexCount];
            for (num = 0; num < pMesh.HeaderInfo.VertexCount; num++)
            {
                pMesh.Normals[num] = new tVertex3f(b.ReadSingle(), b.ReadSingle(), b.ReadSingle());
            }
            if (pMesh.HeaderInfo.TextureMaps > 0)
            {
                pMesh.UVMaps = new tMeshUVMap[pMesh.HeaderInfo.TextureMaps];
                for (num = 0; num > pMesh.HeaderInfo.TextureMaps; num++)
                {
                    pMesh.UVMaps[num] = new tMeshUVMap();
                    pMesh.UVMaps[num].Name = b.ReadBytes(b.ReadInt32());
                    pMesh.UVMaps[num].Coords = new tTextCoord[pMesh.HeaderInfo.VertexCount];
                    num2 = 0;
                    while (num2 < pMesh.HeaderInfo.VertexCount)
                    {
                        pMesh.UVMaps[num].Coords[num2] = new tTextCoord(b.ReadSingle(), b.ReadSingle());
                        num2++;
                    }
                }
            }
            pMesh.Objects = new tMeshObject[pMesh.HeaderInfo.ObjCount];
            for (num = 0; num < pMesh.HeaderInfo.ObjCount; num++)
            {
                tMeshObject obj2 = new tMeshObject();

                    obj2.MaterialName = b.ReadBytes(b.ReadInt32());
                    obj2.Value1 = b.ReadUInt32();
                    obj2.FromVert = b.ReadUInt32();
                    obj2.ToVert = b.ReadUInt32();
                    obj2.FaceCount = b.ReadUInt32();
                    obj2.Faces = new tFace[obj2.FaceCount];

                num2 = 0;
                while (num2 < obj2.FaceCount)
                {
                    obj2.Faces[num2] = new tFace(b.ReadInt16(), b.ReadInt16(), b.ReadInt16());
                    num2++;
                }
                obj2.JValue = b.ReadUInt32();
                obj2.JData = new byte[obj2.JValue];
                num2 = 0;
                while (num2 < obj2.JValue)
                {
                    obj2.JData[num2] = b.ReadByte();
                    num2++;
                }
                obj2.ShaderFlag = b.ReadUInt32();
                if (obj2.ShaderFlag > 0)
                {
                    obj2.ShaderInfo = new tMeshShaderInfo();
                    obj2.ShaderInfo.cParam1 = b.ReadUInt32();
                    obj2.ShaderInfo.cParamFloats = b.ReadUInt32();
                    obj2.ShaderInfo.cTextureUnits = b.ReadUInt32();
                    obj2.ShaderInfo.cParam2 = b.ReadUInt32();
                    tMeshShaderInfo info2 = new tMeshShaderInfo {
                        cTextureUnits = obj2.ShaderInfo.cParam1,
                        cParamFloats = obj2.ShaderInfo.cParamFloats,
                        cParam1 = obj2.ShaderInfo.cParam2,
                        cParam2 = obj2.ShaderInfo.cTextureUnits
                    };
                    obj2.ShaderInfo = info2;
                    obj2.ShaderData = new tMeshShaderData();
                    obj2.ShaderData.ShaderName = b.ReadBytes(b.ReadInt32());
                    obj2.Textures = new tMeshTexture[obj2.ShaderInfo.cTextureUnits];
                    num2 = 0;
                    while (num2 < obj2.ShaderInfo.cTextureUnits)
                    {
                        obj2.Textures[num2] = new tMeshTexture(b.ReadBytes(b.ReadInt32()));
                        num2++;
                    }
                    if (obj2.ShaderInfo.cParam1 > 0)
                    {
                        obj2.ShaderData.Param1 = new uint[obj2.ShaderInfo.cParam1];
                    }
                    if (obj2.ShaderInfo.cParamFloats > 0)
                    {
                        obj2.ShaderData.ParamFloats = new float[obj2.ShaderInfo.cParamFloats];
                    }
                    if (obj2.ShaderInfo.cParam2 > 0)
                    {
                        obj2.ShaderData.Param2 = new uint[obj2.ShaderInfo.cParam2];
                    }
                    obj2.ShaderData.cParam0 = b.ReadUInt32();
                    num2 = 0;
                    while (num2 < obj2.ShaderInfo.cParam2)
                    {
                        obj2.ShaderData.Param2[num2] = b.ReadUInt32();
                        num2++;
                    }
                    num2 = 0;
                    while (num2 < obj2.ShaderInfo.cParamFloats)
                    {
                        obj2.ShaderData.ParamFloats[num2] = b.ReadSingle();
                        num2++;
                    }
                    num2 = 0;
                    while (num2 < obj2.ShaderInfo.cParam1)
                    {
                        obj2.ShaderData.Param1[num2] = b.ReadUInt32();
                        num2++;
                    }
                    pMesh.Objects[num] = obj2;
                }
            }
            pMesh.Weights = new tMeshJointWeights[pMesh.HeaderInfo.JointCount];
            for (num = 0; num < pMesh.HeaderInfo.JointCount; num++)
            {
                pMesh.Weights[num] = new tMeshJointWeights();
                pMesh.Weights[num].JointName = b.ReadBytes(b.ReadInt32());
                pMesh.Weights[num].Count = b.ReadUInt32();
                pMesh.Weights[num].WeightsMap = new tMeshWeightsMap[pMesh.Weights[num].Count];
                for (num2 = 0; num2 < pMesh.Weights[num].Count; num2++)
                {
                    pMesh.Weights[num].WeightsMap[num2] = new tMeshWeightsMap(b.ReadInt32(), b.ReadSingle());
                }
            }
            pMesh.MorphMap = new tMeshMorphMap[pMesh.HeaderInfo.VertexCount];
            for (num = 0; num < pMesh.HeaderInfo.VertexCount; num++)
            {
                pMesh.MorphMap[num] = new tMeshMorphMap(b.ReadBytes(4), b.ReadBytes(4));
            }
            return (b.BaseStream.Position == (pMesh.HeaderInfo.MeshDataSize + 8));
        }

        private static bool ReadV11(BinaryReader b, long Pos)
        {
            int num;
            int num2;
            b.BaseStream.Position = Pos;
            Decoder.Reset();
            pMesh.HeaderInfo.MeshCount = Decoder.Decode(pMesh.HeaderInfo.MeshCount);
            pMesh.HeaderInfo.VertexCount = Decoder.Decode(pMesh.HeaderInfo.VertexCount);
            pMesh.HeaderInfo.JointCount = Decoder.Decode(pMesh.HeaderInfo.JointCount);
            pMesh.HeaderInfo.TextureMaps = Decoder.Decode(pMesh.HeaderInfo.TextureMaps);
            pMesh.HeaderInfo.NormalCount = Decoder.Decode(pMesh.HeaderInfo.NormalCount);
            pMesh.HeaderInfo.ObjCount = Decoder.Decode(pMesh.HeaderInfo.ObjCount);
            pMesh.HeaderInfo.UnknownCount = Decoder.Decode(pMesh.HeaderInfo.UnknownCount);
            pMesh.Value1 = Decoder.Decode(pMesh.Value1);
            pMesh.Vertices = new tVertex3f[pMesh.HeaderInfo.VertexCount];
            for (num = 0; num < pMesh.HeaderInfo.VertexCount; num++)
            {
                pMesh.Vertices[num] = new tVertex3f(b.ReadSingle(), b.ReadSingle(), b.ReadSingle());
            }
            pMesh.Normals = new tVertex3f[pMesh.HeaderInfo.NormalCount];
            for (num = 0; num < pMesh.HeaderInfo.NormalCount; num++)
            {
                pMesh.Normals[num] = new tVertex3f(b.ReadSingle(), b.ReadSingle(), b.ReadSingle());
            }
            if (pMesh.HeaderInfo.TextureMaps > 0)
            {
                pMesh.UVMaps = new tMeshUVMap[pMesh.HeaderInfo.TextureMaps];
                for (num = 0; num < pMesh.HeaderInfo.TextureMaps; num++)
                {
                    tMeshUVMap map = new tMeshUVMap {
                        Name = b.ReadBytes(b.ReadInt32()),
                        Coords = new tTextCoord[pMesh.HeaderInfo.VertexCount]
                    };
                    num2 = 0;
                    while (num2 < pMesh.HeaderInfo.VertexCount)
                    {
                        map.Coords[num2] = new tTextCoord(b.ReadSingle(), b.ReadSingle());
                        num2++;
                    }
                    pMesh.UVMaps[num] = map;
                }
            }
            pMesh.Objects = new tMeshObject[pMesh.HeaderInfo.ObjCount];
            for (num = 0; num < pMesh.HeaderInfo.ObjCount; num++)
            {
                tMeshObject obj2 = new tMeshObject();

                obj2.FromVert = Decoder.Decode(b.ReadUInt32());
                    obj2.ToVert = Decoder.Decode(b.ReadUInt32());
                    obj2.FaceCount = Decoder.Decode(b.ReadUInt32());
                    obj2.Faces = new tFace[obj2.FaceCount];
             
                num2 = 0;
                while (num2 < obj2.FaceCount)
                {
                    obj2.Faces[num2] = new tFace(b.ReadInt16(), b.ReadInt16(), b.ReadInt16());
                    num2++;
                }
                obj2.MaterialName = b.ReadBytes(b.ReadInt32());
                obj2.Value1 = Decoder.Decode(b.ReadUInt32());
                obj2.JValue = Decoder.Decode(b.ReadUInt32());
                obj2.JData = new byte[obj2.JValue];
                num2 = 0;
                while (num2 < obj2.JValue)
                {
                    obj2.JData[num2] = b.ReadByte();
                    num2++;
                }
                obj2.ShaderFlag = Decoder.Decode(b.ReadUInt32());
                if (obj2.ShaderFlag > 0)
                {
                    obj2.ShaderInfo = new tMeshShaderInfo();
                    obj2.ShaderInfo.cParam1 = Decoder.Decode(b.ReadUInt32());
                    obj2.ShaderInfo.cParamFloats = Decoder.Decode(b.ReadUInt32());
                    obj2.ShaderInfo.cTextureUnits = Decoder.Decode(b.ReadUInt32());
                    obj2.ShaderInfo.cParam2 = Decoder.Decode(b.ReadUInt32());
                    obj2.ShaderData = new tMeshShaderData();
                    obj2.ShaderData.ShaderName = b.ReadBytes(b.ReadInt32());
                    obj2.Textures = new tMeshTexture[obj2.ShaderInfo.cTextureUnits];
                    num2 = 0;
                    while (num2 < obj2.ShaderInfo.cTextureUnits)
                    {
                        obj2.Textures[num2] = new tMeshTexture();
                        obj2.Textures[num2].InternalName = b.ReadBytes(b.ReadInt32());
                        num2++;
                    }
                    if (obj2.ShaderInfo.cParam2 > 0)
                    {
                        obj2.ShaderData.Param1 = new uint[obj2.ShaderInfo.cParam1];
                    }
                    if (obj2.ShaderInfo.cParamFloats > 0)
                    {
                        obj2.ShaderData.ParamFloats = new float[obj2.ShaderInfo.cParamFloats];
                    }
                    if (obj2.ShaderInfo.cParam1 > 0)
                    {
                        obj2.ShaderData.Param2 = new uint[obj2.ShaderInfo.cParam2];
                    }
                    obj2.ShaderData.cParam0 = Decoder.Decode(b.ReadUInt32());
                    num2 = 0;
                    while (num2 < obj2.ShaderInfo.cParam2)
                    {
                        obj2.ShaderData.Param2[num2] = Decoder.Decode(b.ReadUInt32());
                        num2++;
                    }
                    num2 = 0;
                    while (num2 < obj2.ShaderInfo.cParamFloats)
                    {
                        obj2.ShaderData.ParamFloats[num2] = b.ReadSingle();
                        num2++;
                    }
                    num2 = 0;
                    while (num2 < obj2.ShaderInfo.cParam1)
                    {
                        obj2.ShaderData.Param1[num2] = Decoder.Decode(b.ReadUInt32());
                        num2++;
                    }
                }
                pMesh.Objects[num] = obj2;
            }
            pMesh.Weights = new tMeshJointWeights[pMesh.HeaderInfo.JointCount];
            for (num = 0; num < pMesh.HeaderInfo.JointCount; num++)
            {
                pMesh.Weights[num] = new tMeshJointWeights();
                pMesh.Weights[num].JointName = b.ReadBytes(b.ReadInt32());
                pMesh.Weights[num].Count = Decoder.Decode(b.ReadUInt32());
                pMesh.Weights[num].WeightsMap = new tMeshWeightsMap[pMesh.Weights[num].Count];
                for (num2 = 0; num2 < pMesh.Weights[num].Count; num2++)
                {
                    pMesh.Weights[num].WeightsMap[num2] = new tMeshWeightsMap(b.ReadInt32(), b.ReadSingle());
                }
            }
            pMesh.MorphMap = new tMeshMorphMap[pMesh.HeaderInfo.VertexCount];
            for (num = 0; num < pMesh.HeaderInfo.VertexCount; num++)
            {
                pMesh.MorphMap[num] = new tMeshMorphMap(b.ReadBytes(4), b.ReadBytes(4));
            }
            Pos = b.BaseStream.Position;
            return (Pos == (pMesh.HeaderInfo.MeshDataSize + 8));
        }
    }
}

