using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Terraria;
using TAPI;
using LolHens.Buffs;

namespace LolHens.NPCs
{
    public class LolHensPet : LolHensNPC
    {
        public int petBuff;
        public int teleportDistance = -1;

        public LolHensPetBuff buff;
        public Player owner;

        public sealed override void AI()
        {
            if (buff != null && owner != null && buff.IsActive() && owner.active && npc.active)
            {
                buff.BuffTime = 100;

                TryTeleport();

                PetAI();
            }
            else
            {
                npc.active = false;
            }
        }

        public virtual void PetAI() { }

        protected virtual void TryTeleport()
        {
            if (teleportDistance > 0)
            {
                if ((owner.position - npc.position).Length() > teleportDistance && Teleport(owner.position))
                {
                    npc.position = owner.position;
                }
            }
        }

        public virtual bool Teleport(Vector2 position) { return true; }
    }
}
