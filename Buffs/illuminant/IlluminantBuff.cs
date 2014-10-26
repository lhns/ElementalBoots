using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace LolHens.Buffs {
    public class IlluminantBuff : TAPI.ModBuff {
        public override void Effects(Player player, int index) {
			Lighting.AddLight((int)(player.Center.X / 16f), (int)(player.Center.Y / 16f), 0.9f, 0.5f, 0.95f);
		}
		
        public override void Effects(NPC npc, int index) {
			Lighting.AddLight((int)(npc.Center.X / 16f), (int)(npc.Center.Y / 16f), 0.9f, 0.5f, 0.95f);
		}
    }
}