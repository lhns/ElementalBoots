using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items {
	public class TrueDemonWings : ElementalWings {
		public TrueDemonWings(ModBase modbase, Item I) : base(modbase, I) {}
		
		public override void LoadTextures() {
			texture = modBase.textures["Item/wings/TrueDemonWingsPlayer.png"];
		}
		
		public override void PostLoadTextures() {
			wingID = LolHensBase.AddWings(item, texture, 140, 8, true);
		}
	}
}