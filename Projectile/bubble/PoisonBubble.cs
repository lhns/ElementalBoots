using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Projectiles {
    public class PoisonBubble : LolHensProjectile {
		public Texture2D texture;
		public float texSize;
		public int color, size;
		public bool toxic = true;
	
        public PoisonBubble(ModBase modBase, Projectile p) : base(modBase, p) {
			p.hurtsTiles = false;
			
			if (size == 1) {
				texSize = 10;
			} else if (size == 2) {
				texSize = 20;
			} else if (size == 3) {
				texSize = 32;
			} else if (size == 4) {
				texSize = 48;
			} else if (size == 5) {
				texSize = 64;
			} else {
				texSize = 1;
			}
		}
		
		public override void PreLoadTextures() {
			if (toxic) {
				color = 5;
			} else {
				color = Main.rand.Next(4) + 1;
			}
			
			if (Main.rand.Next(300) == 0) {
				size = 5;
			} else if (Main.rand.Next(60) == 0) {
				size = 4;
			} else if (Main.rand.Next(20) == 0) {
				size = 3;
			} else if (Main.rand.Next(5) == 0) {
				size = 2;
			} else {
				size = 1;
			}
		}
		
		public override void LoadTextures() {
			texture = modBase.textures["Projectile/bubble/bubble" + color + size + ".png"];
		}

		public override void AI() {
			projectile.alpha = 50;
			projectile.velocity.X = (projectile.velocity.X * 50f + Main.windSpeed * 2f + (float)Main.rand.Next(-10, 11) * 0.1f) / 51f;
			projectile.velocity.Y = (projectile.velocity.Y * 50f + -0.25f + (float)Main.rand.Next(-10, 11) * 0.2f) / 51f;
			projectile.rotation = projectile.velocity.X * 0.3f;
			
			if (projectile.timeLeft > 0)
			{
				if (Main.rand.Next(2) == 0)
				{
					projectile.timeLeft--;
				}
				if (Main.rand.Next(50) == 0)
				{
					projectile.timeLeft -= 5;
				}
				if (Main.rand.Next(100) == 0)
				{
					projectile.timeLeft -= 10;
				}
			}
		}
		
		public override bool PreDraw(SpriteBatch sb) {
			DrawProjectile(sb, texture, projectile, null, 0, 0, false);
			return false;
		}

		public override bool OnTileCollide(ref Vector2 velocityChange) {
			return true;
		}
		
		public override void DamageNPC(NPC npc, int hitDir, ref int damage, ref float knockback, ref bool crit, ref float critMult) {
			npc.AddBuff(BuffDef.type["Vanilla:Poisoned"], 300, false);
		}
		
		public override void DamagePlayer(Player p, int hitDir, ref int damage, ref bool crit, ref float critMult) {
			p.AddBuff(BuffDef.type["Vanilla:Poisoned"], 300, false);
		}

        public override void PostKill() {
            if(Main.netMode != 2) {
				projectile.alpha = 255;
				
				float dustR = 0;
				float dustG = 0;
				float dustB = 0;
				if (color == 1) {
					dustR = 105;
					dustG = 172;
					dustB = 255;
				} else if (color == 2) {
					dustR = 228;
					dustG = 255;
					dustB = 133;
				} else if (color == 3) {
					dustR = 179;
					dustG = 133;
					dustB = 255;
				} else if (color == 4) {
					dustR = 255;
					dustG = 133;
					dustB = 200;
				} else if (color == 5) {
					dustR = 122;
					dustG = 204;
					dustB = 70;
				}
				dustR /= 255;
				dustG /= 255;
				dustB /= 255;
				
                float num = texSize * projectile.scale * 0.8f;
				int i = 0;
				while ((float)i < num) {
					int num6 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), (int)(texSize * projectile.scale), (int)(texSize * projectile.scale), 176, 0, 0, 80, new Color(dustR, dustG, dustB), 1f);
					Main.dust[num6].noGravity = true;
					Main.dust[num6].alpha = 100;
					Main.dust[num6].scale = projectile.scale;
					i++;
				}
            }
        }

		public static void DrawProjectile(SpriteBatch sb, Texture2D texture, Projectile p, Color? overrideColor = null, float offsetX = 0f, float offsetY = 0f, bool flip = true) {
			int direction = flip ? p.direction : 1;
			offsetX += (-texture.Width * 0.5f);
			Color lightColor = overrideColor != null ? (Color)overrideColor : p.GetAlpha(GetLightColor(p.Center));
			Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
			Vector2 offset = RotateVector(p.Center, p.Center + new Vector2(direction == -1 ? offsetX : offsetY, direction == 1 ? offsetX : offsetY), p.rotation - 2.355f) - p.Center;
			sb.Draw(texture, p.Center - Main.screenPosition + offset, new Rectangle(0, 0, texture.Width, texture.Height), lightColor, p.rotation, origin, p.scale, direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0f);
		}
    }
}