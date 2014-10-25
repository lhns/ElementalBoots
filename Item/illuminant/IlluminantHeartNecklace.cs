using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items {
	public class IlluminantHeartNecklace : LolHensItem {
		public IlluminantHeartNecklace(ModBase modbase, Item I) : base(modbase, I) { }

        public override void Effects(Player player) {
			base.Effects(player);
			
			player.panic = true;
        }
		
		public override void PlayerDied(Player player) {
			player.statLife = player.statLifeMax;
		}
		
		public override void DamagePlayer(NPC npc, Player owner, int hitDir, ref int damage, ref bool crit, ref float critMult) {
			owner.AddBuff(BuffDef.type["LolHens:IlluminantBuff"], 900, false);
			owner.AddBuff(BuffDef.type["Vanilla:Panic!"], 600, false);
			
			Projectile.NewProjectile(owner.Center.X, owner.Center.Y, 0, 0, ProjDef.byName["LolHens:IlluminantCrystal"].type, item.damage, item.knockBack, owner.whoAmI);
		}
	}
}