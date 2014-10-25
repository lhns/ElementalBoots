using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items {
	public class GreenHorseshoeBalloon : LolHensItem {
		public GreenHorseshoeBalloon(ModBase modbase, Item I) : base(modbase, I) {}

        public override void Effects(Player player) {
			player.doubleJump4 = true;
			player.jumpBoost = true;
			
			player.noFallDmg = true;
        }
	}
}