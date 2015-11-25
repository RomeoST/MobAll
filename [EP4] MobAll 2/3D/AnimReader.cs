namespace _EP4__MobAll_2.D3D
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
        internal class AnimReader
        {
        public static cAnimFile ReadFile(string FileName)
        {
            Encoding encoding = Encoding.GetEncoding(0x4e3);
            cAnimFile file = new cAnimFile();
            using (BinaryReader reader = new BinaryReader(File.Open(FileName, FileMode.Open)))
            {
                byte[] buffer = reader.ReadBytes(4);
                file.Header = new cAnimHeader();
                file.Header.Version = reader.ReadInt32();
                file.Header.AnimCount = reader.ReadInt32();
                file.Animation = new cAnimation[file.Header.AnimCount];
                for (int i = 0; i < file.Header.AnimCount; i++)
                {
                    cAnimation animation = new cAnimation();
                        animation.SkaPath = encoding.GetString(reader.ReadBytes(reader.ReadInt32()));
                        animation.AnimeName = encoding.GetString(reader.ReadBytes(reader.ReadInt32()));
                        animation.fps = reader.ReadSingle();
                        animation.last_frame = reader.ReadInt32();
                        animation.extra_val1 = reader.ReadInt32();
                        animation.extra_val2 = reader.ReadInt32();
                        animation.extra_val3 = reader.ReadInt32();
                        animation.JointCount = reader.ReadInt32();
                        animation.BoneAnim = new cJointAnim[animation.JointCount];
                    for (int j = 0; j < animation.JointCount; j++)
                    {
                        cJointAnim anim = new cJointAnim {
                            JointName = encoding.GetString(reader.ReadBytes(reader.ReadInt32()))
                        };
                        float[] numArray = new float[12];
                        int index = 0;
                        while (index < 12)
                        {
                            numArray[index] = reader.ReadSingle();
                            index++;
                        }
                        anim.PositionCount = reader.ReadInt32();
                        anim.Positions = new cPositionKeyFrame[anim.PositionCount];
                        index = 0;
                        while (index < anim.PositionCount)
                        {
                            anim.Positions[index] = new cPositionKeyFrame(reader.ReadInt16(), reader.ReadInt16(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                            index++;
                        }
                        anim.RotationCount = reader.ReadInt32();
                        anim.Rotations = new cRotationKeyFrame[anim.RotationCount];
                        for (index = 0; index < anim.RotationCount; index++)
                        {
                            anim.Rotations[index] = new cRotationKeyFrame(reader.ReadInt16(), reader.ReadInt16(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
                        }
                        anim.Unknown = reader.ReadSingle();
                        animation.BoneAnim[j] = anim;
                    }
                    file.Animation[i] = animation;
                    file.Animation[i].EndData = reader.ReadInt32();
                }
            }
            return file;
        }

        public static bool WriteFile(cAnimFile animData, string FileName)
        {
            try
            {
                float[] numArray = new float[] { 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f };
                Encoding encoding = Encoding.GetEncoding(0x4e3);
                using (BinaryWriter writer = new BinaryWriter(File.Create(FileName)))
                {
                    writer.Write(encoding.GetBytes("ANIM"));
                    writer.Write(animData.Header.Version);
                    writer.Write(animData.Header.AnimCount);
                    for (int i = 0; i < animData.Animation.Count<cAnimation>(); i++)
                    {
                        cAnimation animation = animData.Animation[i];
                        writer.Write(encoding.GetBytes(animation.SkaPath).Length);
                        writer.Write(encoding.GetBytes(animation.SkaPath));
                        writer.Write(encoding.GetBytes(animation.AnimeName).Length);
                        writer.Write(encoding.GetBytes(animation.AnimeName));
                        writer.Write(animation.fps);
                        writer.Write(animation.last_frame);
                        writer.Write(animation.extra_val1);
                        writer.Write(animation.extra_val2);
                        writer.Write(animation.extra_val3);
                        writer.Write(animation.BoneAnim.Count<cJointAnim>());
                        for (int j = 0; j < animation.BoneAnim.Count<cJointAnim>(); j++)
                        {
                            cJointAnim anim = animation.BoneAnim[j];
                            writer.Write(encoding.GetBytes(anim.JointName).Length);
                            writer.Write(encoding.GetBytes(anim.JointName));
                            int index = 0;
                            while (index < 12)
                            {
                                writer.Write(numArray[index]);
                                index++;
                            }
                            writer.Write(anim.Positions.Count<cPositionKeyFrame>());
                            index = 0;
                            while (index < anim.Positions.Count<cPositionKeyFrame>())
                            {
                                cPositionKeyFrame frame = anim.Positions[index];
                                writer.Write(frame.Frame);
                                writer.Write(frame.Flags);
                                writer.Write(frame.x);
                                writer.Write(frame.y);
                                writer.Write(frame.z);
                                index++;
                            }
                            writer.Write(anim.Rotations.Count<cRotationKeyFrame>());
                            for (index = 0; index < anim.Rotations.Count<cRotationKeyFrame>(); index++)
                            {
                                cRotationKeyFrame frame2 = anim.Rotations[index];
                                writer.Write(frame2.Frame);
                                writer.Write(frame2.Flags);
                                writer.Write(frame2.w);
                                writer.Write(frame2.x);
                                writer.Write(frame2.y);
                                writer.Write(frame2.z);
                            }
                            writer.Write(anim.Unknown);
                        }
                        writer.Write(animation.EndData);
                    }
                    writer.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
