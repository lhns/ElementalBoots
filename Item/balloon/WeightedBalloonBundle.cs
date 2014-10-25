using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items {
	public class WeightedBalloonBundle : LolHensItem {
		public WeightedBalloonBundle(ModBase modbase, Item I) : base(modbase, I) {}

        public override void Effects(Player player) {
			player.doubleJump = true;
			player.doubleJump2 = true;
			player.doubleJump3 = true;
			player.doubleJump4 = true;
			player.jumpBoost = true;
			player.noFallDmg = true;
        }
	}
}