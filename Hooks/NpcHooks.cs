using System;
using Terraria;
namespace Hooks
{
	public static class NpcHooks
	{
		public delegate void StrikeNpcD(NpcStrikeEventArgs e);
        public delegate void SpawnNpcD(NpcSpawnEventArgs e);
		public delegate void TriggerPressurePlateD(NpcTriggerPressurePlateEventArgs e);
		public delegate void UseDoorD(NpcUseDoorEventArgs e);
		public static event SetDefaultsD<NPC, int> SetDefaultsInt;
		public static event SetDefaultsD<NPC, string> SetDefaultsString;
        public static event SetDefaultsD<NPC, int> NetDefaults;
		public static event NpcHooks.StrikeNpcD StrikeNpc;
	    public static event SpawnNpcD SpawnNpc;
		public static event TriggerPressurePlateD TriggerPressurePlate;
		public static event UseDoorD UseDoor;

		public static void OnSetDefaultsInt(ref int npctype, NPC npc)
		{
			if (NpcHooks.SetDefaultsInt == null)
			{
				return;
			}
			SetDefaultsEventArgs<NPC, int> setDefaultsEventArgs = new SetDefaultsEventArgs<NPC, int>
			{
				Object = npc, 
				Info = npctype
			};
			NpcHooks.SetDefaultsInt(setDefaultsEventArgs);
			npctype = setDefaultsEventArgs.Info;
		}
		public static void OnSetDefaultsString(ref string npcname, NPC npc)
		{
			if (NpcHooks.SetDefaultsString == null)
			{
				return;
			}
			SetDefaultsEventArgs<NPC, string> setDefaultsEventArgs = new SetDefaultsEventArgs<NPC, string>
			{
				Object = npc, 
				Info = npcname
			};
			NpcHooks.SetDefaultsString(setDefaultsEventArgs);
			npcname = setDefaultsEventArgs.Info;
		}
        public static void OnNetDefaults(ref int nettype, NPC npc)
        {
            if (NpcHooks.NetDefaults == null)
            {
                return;
            }
            SetDefaultsEventArgs<NPC, int> setDefaultsEventArgs = new SetDefaultsEventArgs<NPC, int>
            {
                Object = npc,
                Info = nettype
            };
            NpcHooks.NetDefaults(setDefaultsEventArgs);
            nettype = setDefaultsEventArgs.Info;
        }
		public static bool OnStrikeNpc(NPC npc, ref int damage, ref float knockback, ref int hitdirection, ref bool crit, ref bool noEffect, ref double retdamage)
		{
			if (NpcHooks.StrikeNpc == null)
			{
				return false;
			}
			NpcStrikeEventArgs npcStrikeEventArgs = new NpcStrikeEventArgs
			{
				Npc = npc, 
				Damage = damage, 
				KnockBack = knockback, 
				HitDirection = hitdirection, 
				Critical = crit,
                NoEffect = noEffect,
                ReturnDamage = retdamage
			};
			NpcHooks.StrikeNpc(npcStrikeEventArgs);
			crit = npcStrikeEventArgs.Critical;
			damage = npcStrikeEventArgs.Damage;
			knockback = npcStrikeEventArgs.KnockBack;
			hitdirection = npcStrikeEventArgs.HitDirection;
		    noEffect = npcStrikeEventArgs.NoEffect;
		    retdamage = npcStrikeEventArgs.ReturnDamage;
			return npcStrikeEventArgs.Handled;
		}

        public static bool OnSpawnNpc(NPC npc )
        {
            if( SpawnNpc == null )
            {
                return false;
            }
            NpcSpawnEventArgs npcSpawnEventArgs = new NpcSpawnEventArgs
                                                        {
                                                            Npc = npc
                                                        };
            NpcHooks.SpawnNpc(npcSpawnEventArgs);
            return npcSpawnEventArgs.Handled;
        }

		public static bool OnTriggerPressurePlate(NPC npc, int x, int y)
		{
			if (NpcHooks.TriggerPressurePlate == null)
			{
				return false;
			}
			NpcTriggerPressurePlateEventArgs triggerPressurePlateArgs = new NpcTriggerPressurePlateEventArgs
			{
				Npc = npc,
				X = x,
				Y = y
			};
			NpcHooks.TriggerPressurePlate(triggerPressurePlateArgs);
			return triggerPressurePlateArgs.Handled;
		}
		public static bool OnUseDoor(NPC npc, int x, int y, bool isOpening)
		{
			if (NpcHooks.UseDoor == null)
			{
				return false;
			}
			NpcUseDoorEventArgs npcUseDoorArgs = new NpcUseDoorEventArgs
			{
				Npc = npc,
				X = x,
				Y = y,
				IsOpening = isOpening
			};
			NpcHooks.UseDoor(npcUseDoorArgs);
			return npcUseDoorArgs.Handled;
		}
	}
}
