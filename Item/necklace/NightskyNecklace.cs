using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items {
	public class NightskyNecklace : LolHensItem {
		public NightskyNecklace(ModBase modbase, Item I) : base(modbase, I) {}

        public override void Effects(Player player) {
			player.accMerman = true;
			player.wolfAcc = true;
			
			player.starCloak = true;
			player.longInvince = true;
        }
	}
}