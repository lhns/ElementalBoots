using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items {
	public class TerraWings : ElementalWings {
		public TerraWings(ModBase modbase, Item I) : base(modbase, I) {}
		
		public override void LoadTextures() {
			texture = modBase.textures["Item/wings/TerraWingsPlayer.png"];
		}
		
		public override void PostLoadTextures() {
			wingID = LolHensBase.AddWings(item, texture, 300, 8, true);
		}
	}
}