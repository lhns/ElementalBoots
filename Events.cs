using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibEventManagerCSharp;

namespace ElementalBoots
{
    class Events
    {
        public static EventRegistry Registry()
        {
            return ElementalBootsMod.instance.eventRegistry;
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
    }
}
