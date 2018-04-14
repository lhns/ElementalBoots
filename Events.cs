using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibEventManagerCSharp;
using Terraria.DataStructures;

namespace ElementalBoots
{
    class Events
    {
        public static EventRegistry Registry()
        {
            return ElementalBootsMod.instance.eventRegistry;
        }

        public class ChestGenerated : Event
        {
            public readonly ChestInfo chestInfo;

            public ChestGenerated(ChestInfo chestInfo)
            {
                this.chestInfo = chestInfo;
            }
        }

        public class PlayerPostRespawn : Event
        {
            public readonly MPlayer player;

            public PlayerPostRespawn(MPlayer player)
            {
                this.player = player;
            }
        }

        public class PlayerPostHurt : Event
        {
            public readonly MPlayer player;
            public readonly bool pvp, quiet;
            public readonly double damage;
            public readonly int hitDirection;
            public readonly bool crit;

            public PlayerPostHurt(MPlayer player, bool pvp, bool quiet, double damage, int hitDirection, bool crit)
            {
                this.player = player;
                this.pvp = pvp;
                this.quiet = quiet;
                this.damage = damage;
                this.hitDirection = hitDirection;
                this.crit = crit;
            }
        }

        public class ModifyPlayerDrawData : Event
        {
            public readonly MPlayer player;
            public readonly List<DrawData> playerDrawData;

            public ModifyPlayerDrawData(MPlayer player, List<DrawData> playerDrawData)
            {
                this.player = player;
                this.playerDrawData = playerDrawData;
            }
        }
    }
}
