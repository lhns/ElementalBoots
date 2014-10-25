using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Projectiles
{
    public class LolHensProjectile : ModProjectile
    {
        public LolHensProjectile() {
			PreLoadTextures();
			if (!Main.dedServ) LoadTextures();
		}
		
		public static Color GetLightColor(Vector2 position) {
			return Lighting.GetColor((int)(position.X / 16f), (int)(position.Y / 16f));
		}
		
		public static Vector2 RotateVector(Vector2 vecToRot, Vector2 origin, float rot) {
			float newPosX = (float)(Math.Cos(rot) * (vecToRot.X - origin.X) - Math.Sin(rot) * (vecToRot.Y - origin.Y) + origin.X);
			float newPosY = (float)(Math.Sin(rot) * (vecToRot.X - origin.X) + Math.Cos(rot) * (vecToRot.Y - origin.Y) + origin.Y);
			return new Vector2(newPosX, newPosY);
		}
		
		public virtual void PreLoadTextures() {}
		
		public virtual void LoadTextures() {}
    }
}