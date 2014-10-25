using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Items {
	public class TrueAngelWings : TrueDemonWings {
		public TrueAngelWings(ModBase modbase, Item I) : base(modbase, I) {}
		
		public override void LoadTextures() {
			texture = modBase.textures["Item/wings/TrueAngelWingsPlayer.png"];
		}
	}
}