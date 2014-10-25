using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items {
	public class ElementalWings : LolHensItem {
		public Texture2D texture = null;
		public int wingID;
		
		public ElementalWings(ModBase modbase, Item I) : base(modbase, I) {}
		
		public override void LoadTextures() {
			texture = modBase.textures["Item/wings/ElementalWingsPlayer.png"];
		}
		
		public override void PostLoadTextures() {
			wingID = LolHensBase.AddWings(item, texture, 600, 8, true);
		}

        public override void Effects(Player player) {
			player.wings = wingID;
        }
	}
}