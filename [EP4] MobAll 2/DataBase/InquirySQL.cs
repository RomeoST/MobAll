using System;
using System.Linq;
using LinqToDB;
using FieryLib.Models;

namespace _EP4__MobAll_2.DataBase
{
    class InquirySQL
    {
        public enum ActionSQL
        {
            UPDATE,
            ADD,
            DEL
        }

        public bool UpdateThis(sqlTable.Data.t_npc npc, DBInfoConnection context)
        {
            try
            {
                using (var db = context.CreateMySQL)
                {
                    db.t_npc.Where(p => p.a_index == npc.a_index)
                        .Set(p => p.a_name, npc.a_name)
                        .Set(p => p.a_level, npc.a_level)
                        .Set(p => p.a_mp, npc.a_mp)
                        .Set(p => p.a_hp, npc.a_hp)
                        .Set(p => p.a_attackSpeed, npc.a_attackSpeed)
                        .Set(p => p.a_run_speed, npc.a_run_speed)
                        .Set(p => p.a_walk_speed, npc.a_walk_speed)
                        .Set(p => p.a_file_smc, npc.a_file_smc)
                        .Set(p => p.a_attack_area, npc.a_attack_area)
                        .Set(p => p.a_flag, npc.a_flag)
                        .Set(p => p.a_flag1, npc.a_flag1)
                        .Set(p => p.a_scale, npc.a_scale)
                        .Set(p => p.a_size, npc.a_size)
                        .Set(p => p.a_skillmaster, npc.a_skillmaster)
                        .Set(p => p.a_sskill_master, npc.a_sskill_master)
                        .Set(p => p.a_motion_run, npc.a_motion_run)
                        .Set(p => p.a_motion_walk, npc.a_motion_walk)
                        .Set(p => p.a_motion_idle, npc.a_motion_idle)
                        .Set(p => p.a_motion_idle2, npc.a_motion_idle2)
                        .Set(p => p.a_motion_die, npc.a_motion_die)
                        .Set(p => p.a_motion_dam, npc.a_motion_dam)
                        .Set(p => p.a_motion_attack, npc.a_motion_attack)
                        .Set(p => p.a_motion_attack2, npc.a_motion_attack2)
                        .Set(p => p.a_fireDelayCount, npc.a_fireDelayCount)
                        .Set(p => p.a_fireDelay0, npc.a_fireDelay0)
                        .Set(p => p.a_fireDelay1, npc.a_fireDelay1)
                        .Set(p => p.a_fireDelay2, npc.a_fireDelay2)
                        .Set(p => p.a_fireDelay3, npc.a_fireDelay3)
                        .Set(p => p.a_fireObject, npc.a_fireObject)
                        .Set(p => p.a_fireSpeed, npc.a_fireSpeed)
                        .Set(p => p.a_attackType, npc.a_attackType)
                        .Set(p => p.a_fireEffect0, npc.a_fireEffect0)
                        .Set(p => p.a_fireEffect1, npc.a_fireEffect1)
                        .Set(p => p.a_fireEffect2, npc.a_fireEffect2)
                        .Set(p => p.a_rvr_grade, npc.a_rvr_grade)
                        .Set(p => p.a_rvr_value, npc.a_rvr_value).Update();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            
        }
        public bool AddThis(sqlTable.Data.t_npc npc, DBInfoConnection context)
        {
            try
            {
                using (var db = context.CreateMySQL)
                {
                    db.t_npc
                        .Value(p => p.a_enable, 1)
                        .Value(p => p.a_index, npc.a_index)
                        .Value(p => p.a_name, npc.a_name)
                        .Value(p => p.a_level, npc.a_level)
                        .Value(p => p.a_mp, npc.a_mp)
                        .Value(p => p.a_hp, npc.a_hp)
                        .Value(p => p.a_attackSpeed, npc.a_attackSpeed)
                        .Value(p => p.a_run_speed, npc.a_run_speed)
                        .Value(p => p.a_walk_speed, npc.a_walk_speed)
                        .Value(p => p.a_file_smc, npc.a_file_smc)
                        .Value(p => p.a_attack_area, npc.a_attack_area)
                        .Value(p => p.a_flag, npc.a_flag)
                        .Value(p => p.a_flag1, npc.a_flag1)
                        .Value(p => p.a_scale, npc.a_scale)
                        .Value(p => p.a_size, npc.a_size)
                        .Value(p => p.a_skillmaster, npc.a_skillmaster)
                        .Value(p => p.a_sskill_master, npc.a_sskill_master)
                        .Value(p => p.a_motion_run, npc.a_motion_run)
                        .Value(p => p.a_motion_walk, npc.a_motion_walk)
                        .Value(p => p.a_motion_idle, npc.a_motion_idle)
                        .Value(p => p.a_motion_idle2, npc.a_motion_idle2)
                        .Value(p => p.a_motion_die, npc.a_motion_die)
                        .Value(p => p.a_motion_dam, npc.a_motion_dam)
                        .Value(p => p.a_motion_attack, npc.a_motion_attack)
                        .Value(p => p.a_motion_attack2, npc.a_motion_attack2)
                        .Value(p => p.a_fireDelayCount, npc.a_fireDelayCount)
                        .Value(p => p.a_fireDelay0, npc.a_fireDelay0)
                        .Value(p => p.a_fireDelay1, npc.a_fireDelay1)
                        .Value(p => p.a_fireDelay2, npc.a_fireDelay2)
                        .Value(p => p.a_fireDelay3, npc.a_fireDelay3)
                        .Value(p => p.a_fireObject, npc.a_fireObject)
                        .Value(p => p.a_fireSpeed, npc.a_fireSpeed)
                        .Value(p => p.a_attackType, npc.a_attackType)
                        .Value(p => p.a_fireEffect0, npc.a_fireEffect0)
                        .Value(p => p.a_fireEffect1, npc.a_fireEffect1)
                        .Value(p => p.a_fireEffect2, npc.a_fireEffect2)
                        .Value(p => p.a_rvr_grade, npc.a_rvr_grade)
                        .Value(p => p.a_rvr_value, npc.a_rvr_value).Insert();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool UpdateExtraInfo(sqlTable.Data.t_npc npc, DBInfoConnection context)
        {
            try
            {
                using(var db = context.CreateMySQL)
                {
                    db.t_npc.Where(p => p.a_index == npc.a_index)
                        .Set(p => p.a_maxplus, npc.a_maxplus)
                        .Set(p => p.a_minplus, npc.a_minplus)
                        .Set(p => p.a_probplus, npc.a_probplus)
                        .Set(p => p.a_prize, npc.a_prize)
                        .Set(p => p.a_skill_point, npc.a_skill_point)
                        .Set(p => p.a_exp, npc.a_exp)
                        .Set(p => p.a_item_0, npc.a_item_0)
                        .Set(p => p.a_item_1, npc.a_item_1)
                        .Set(p => p.a_item_2, npc.a_item_2)
                        .Set(p => p.a_item_3, npc.a_item_3)
                        .Set(p => p.a_item_4, npc.a_item_4)
                        .Set(p => p.a_item_5, npc.a_item_5)
                        .Set(p => p.a_item_6, npc.a_item_6)
                        .Set(p => p.a_item_7, npc.a_item_7)
                        .Set(p => p.a_item_8, npc.a_item_8)
                        .Set(p => p.a_item_9, npc.a_item_9)
                        .Set(p => p.a_item_10, npc.a_item_10)
                        .Set(p => p.a_item_11, npc.a_item_11)
                        .Set(p => p.a_item_12, npc.a_item_12)
                        .Set(p => p.a_item_13, npc.a_item_13)
                        .Set(p => p.a_item_14, npc.a_item_14)
                        .Set(p => p.a_item_15, npc.a_item_15)
                        .Set(p => p.a_item_16, npc.a_item_16)
                        .Set(p => p.a_item_17, npc.a_item_17)
                        .Set(p => p.a_item_18, npc.a_item_18)
                        .Set(p => p.a_item_19, npc.a_item_19)
                        .Set(p => p.a_item_percent_0, npc.a_item_percent_0)
                        .Set(p => p.a_item_percent_1, npc.a_item_percent_1)
                        .Set(p => p.a_item_percent_2, npc.a_item_percent_2)
                        .Set(p => p.a_item_percent_3, npc.a_item_percent_3)
                        .Set(p => p.a_item_percent_4, npc.a_item_percent_4)
                        .Set(p => p.a_item_percent_5, npc.a_item_percent_5)
                        .Set(p => p.a_item_percent_6, npc.a_item_percent_6)
                        .Set(p => p.a_item_percent_7, npc.a_item_percent_7)
                        .Set(p => p.a_item_percent_8, npc.a_item_percent_8)
                        .Set(p => p.a_item_percent_9, npc.a_item_percent_9)
                        .Set(p => p.a_item_percent_10, npc.a_item_percent_10)
                        .Set(p => p.a_item_percent_11, npc.a_item_percent_11)
                        .Set(p => p.a_item_percent_12, npc.a_item_percent_12)
                        .Set(p => p.a_item_percent_13, npc.a_item_percent_13)
                        .Set(p => p.a_item_percent_14, npc.a_item_percent_14)
                        .Set(p => p.a_item_percent_15, npc.a_item_percent_15)
                        .Set(p => p.a_item_percent_16, npc.a_item_percent_16)
                        .Set(p => p.a_item_percent_17, npc.a_item_percent_17)
                        .Set(p => p.a_item_percent_18, npc.a_item_percent_18)
                        .Set(p => p.a_item_percent_19, npc.a_item_percent_19)
                        .Update();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                
            }
        }

        // DropAll
        public void DellDropAll_NPC(sqlTable.Data.t_npc_drop_all npc, DBInfoConnection context)
        {
            try
            {
                if(context.TestConnection())
                {
                    using (var db = context.CreateMySQL)
                    {
                        db.t_npc_drop_all.Where(p => p.a_npc_idx == npc.a_npc_idx)
                            .Delete();
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        public void DropAll_NPC_Add(sqlTable.Data.t_npc_drop_all npc, DBInfoConnection context)
        {
            try
            {
                if (context.TestConnection())
                {
                    using (var db = context.CreateMySQL)
                    {
                        db.t_npc_drop_all
                            .Value(p => p.a_npc_idx, npc.a_npc_idx)
                            .Value(p => p.a_item_idx, npc.a_item_idx)
                            .Value(p => p.a_prob, npc.a_prob).Insert();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        // Генирация для таблицы листа SQL
        public void GenerateListForTable(ref sqlTable.Data.t_npc mob, MobAllLod lod)
        {
            mob.a_index = lod.NpcID;
            mob.a_name = lod.Name;
            mob.a_level = lod.Level;
            mob.a_hp = lod.HP;
            mob.a_mp = lod.MP;
            mob.a_attackSpeed = Convert.ToSByte(lod.AttackSpeed);
            mob.a_run_speed = lod.RunSpeed;
            mob.a_walk_speed = lod.WalkSpeed;
            mob.a_file_smc = lod.SMC;
            mob.a_attack_area = lod.AttackArea;
            mob.a_flag = lod.Flag;
            mob.a_flag1 = lod.Flag1;
            mob.a_scale = lod.Scale;
            mob.a_size = lod.Size;
            if (lod.SkillMaster == 255)
                mob.a_skillmaster = -1;
            else
                mob.a_skillmaster = Convert.ToSByte(lod.SkillMaster);
            if (lod.SSkillMaster == 255)
                mob.a_sskill_master = -1;
            else
                mob.a_sskill_master = Convert.ToSByte(lod.SSkillMaster);
            mob.a_motion_run = lod.Run;
            mob.a_motion_walk = lod.Walk;
            mob.a_motion_idle = lod.Idle;
            mob.a_motion_idle2 = lod.Idle2;
            mob.a_motion_die = lod.Die;
            mob.a_motion_dam = lod.Damage;
            mob.a_motion_attack = lod.Attack;
            mob.a_motion_attack2 = lod.Attack2;
            mob.a_fireDelayCount = Convert.ToSByte(lod.FireDelayCount);
            mob.a_fireDelay0 = lod.FireDelay0;
            mob.a_fireDelay1 = lod.FireDelay1;
            mob.a_fireDelay2 = lod.FireDelay2;
            mob.a_fireDelay3 = lod.FireDelay3;
            mob.a_fireObject = Convert.ToSByte(lod.FireObject);
            mob.a_fireSpeed = lod.FireSpeed;
            if (lod.AttackType == 255)
                mob.a_attackType = -1;
            else 
                mob.a_attackType = Convert.ToSByte(lod.AttackType);
            mob.a_fireEffect0 = lod.FireEffect0;
            mob.a_fireEffect1 = lod.FireEffect1;
            mob.a_fireEffect2 = lod.FireEffect2;
            mob.a_rvr_grade = lod.RvRGrade;
            mob.a_rvr_value = lod.RvRValue;
        }
    }
}
