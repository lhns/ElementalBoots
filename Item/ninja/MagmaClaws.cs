using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items {
	public class MagmaClaws : LolHensItem {
		public int dustDelay = 0;
		public float brightness = 0.8f;
	
		public MagmaClaws(ModBase modbase, Item I) : base(modbase, I) {}

        public override void Effects(Player player) {
			player.kbGlove = true;
			player.meleeSpeed += 0.09f;
			player.meleeDamage += 0.09f;
			player.magmaStone = true;
			
			player.spikedBoots++;
        }
	}
}