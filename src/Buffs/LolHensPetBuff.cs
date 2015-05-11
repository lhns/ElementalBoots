using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TAPI;
using Terraria;

namespace LolHens.Buffs
{
    public class LolHensPetBuff: LolHensBuff
    {
        public NPC petNPC = null;

        public override void Effects(CodableEntity entity, int index)
        {
            base.Effects(entity, index);

            if (petNPC != null)
            {
                Player player = entity as Player;
                if (player == null) return;

                for (int i = 0; i < Main.npc.Length - 1; i++)
                {
                    if (Main.npc[i].active && Main.npc[i].type == petNPC.type && Main.npc[i].target == player.whoAmI) return;
                }

                int npcId = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, petNPC.type);
                Main.npc[npcId].target = player.whoAmI;
                Main.npc[npcId].netUpdate = true;
                if (Main.netMode == 2 && npcId < 200) NetMessage.SendData(23, -1, -1, "", npcId);
            }
        }
    }
}
