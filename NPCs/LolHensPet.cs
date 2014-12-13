using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Terraria;
using TAPI;

namespace LolHens.NPCs
{
    public class LolHensPet: LolHensNPC
    {
        public int petBuff = -1;
        public Player owner = null;
        public int teleportDistance = -1;

        public override void AI()
        {
            bool active = false;
            if (petBuff > -1 && npc.target > -1 && npc.target < Main.player.Length)
            {
                owner = Main.player[npc.target];
                int buffIndex = owner.BuffIndex(petBuff);
                if (buffIndex > -1)
                {
                    active = true;
                    owner.buffTime[buffIndex] = 100;
                }
            }
            if (!active) npc.active = false;

            if (npc.active)
            {
                if (teleportDistance > -1)
                {
                    if ((owner.position - npc.position).Length() > teleportDistance && Teleport(owner.position))
                    {
                        npc.position = owner.position;
                    }
                }
            }
        }

        public virtual bool Teleport(Vector2 position) { return true; }
    }
}
