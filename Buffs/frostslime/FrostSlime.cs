using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;
using LolHens.Projectiles;

namespace LolHens.Buffs
{
    public class FrostSlime : LolHensBuff
    {
        public override void Effects(CodableEntity player, int index)
        {
            base.Effects(player, index);

            for (int num36 = 0; num36 < 200; num36++) if (Main.npc[num36].active && Main.npc[num36].type == NPCDef.byName["LolHens:FrozenSlime"].type && Main.npc[num36].ai[0] == player.whoAmI) return;

            int npcId = NPC.NewNPC((int)player.Center.X, (int)player.Center.Y, NPCDef.byName["LolHens:FrozenSlime"].type);
            Main.npc[npcId].ai[0] = player.whoAmI;
            Main.npc[npcId].netUpdate = true;
            if (Main.netMode == 2 && npcId < 200) NetMessage.SendData(23, -1, -1, "", npcId);
        }
    }
}
