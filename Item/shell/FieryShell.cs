using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items {
	public class FieryShell : LolHensItem {
		public FieryShell(ModBase modbase, Item I) : base(modbase, I) {}

        public override void Effects(Player player) {
			player.accMerman = true;
			player.lavaMax += 720;
			
			if (player.lavaWet) {
				player.releaseJump = true;
				player.wings = 0;
				player.merman = true;
				player.accFlipper = true;
				player.AddBuff(34, 2, true);
			}
        }
	}
}