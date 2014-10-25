using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items {
	public class IlluminantStarNecklace : LolHensItem {
		public bool playerDead = false;
		public IlluminantStarNecklace(ModBase modbase, Item I) : base(modbase, I) { }

        public override void Effects(Player player) {
			base.Effects(player);
			
			player.starCloak = true;
			player.longInvince = true;
        }
		
		public override void PlayerDied(Player player) {
			player.statMana = player.statManaMax;
		}
		
		public override void DamagePlayer(NPC npc, Player owner, int hitDir, ref int damage, ref bool crit, ref float critMult) {
			owner.AddBuff(BuffDef.type["LolHens:IlluminantBuff"], 900, false);
		}
	}
}