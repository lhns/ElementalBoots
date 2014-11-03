using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Projectiles
{
    public class PoisonBubble : LolHensProjectile
    {
        public Texture2D texture;
        public float texSize;
        public int color, size;
        public bool toxic = true;

        public override void Init()
        {
            projectile.hurtsTiles = false;

            if (Main.rand.Next(300) == 0)
            {
                size = 5;
            }
            else if (Main.rand.Next(60) == 0)
            {
                size = 4;
            }
            else if (Main.rand.Next(20) == 0)
            {
                size = 3;
            }
            else if (Main.rand.Next(5) == 0)
            {
                size = 2;
            }
            else
            {
                size = 1;
            }

            if (size == 1)
            {
                texSize = 10;
            }
            else if (size == 2)
            {
                texSize = 20;
            }
            else if (size == 3)
            {
                texSize = 32;
            }
            else if (size == 4)
            {
                texSize = 48;
            }
            else if (size == 5)
            {
                texSize = 64;
            }
            else
            {
                texSize = 1;
            }

            if (toxic)
            {
                color = 5;
            }
            else
            {
                color = Main.rand.Next(4) + 1;
            }
        }

        public override void InitTextures()
        {
            texture = modBase.textures["Projectiles/bubble/textures/bubble" + color + size];
        }

        public override void AI()
        {
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

        public override bool PreDraw(SpriteBatch sb)
        {
            DrawProjectile(sb, texture, projectile, null, 0, 0, false);
            return false;
        }

        public override bool OnTileCollide(ref Vector2 velocityChange) { return true; }

        public override void Damage(CodableEntity entity, int hitDir, ref int damage, ref float knockback, ref bool crit, ref float critMult) { if (toxic) entity.AddBuff("Vanilla:Poisoned", 300, projectile, false); }

        public override void PostKill()
        {
            if (Main.netMode != 2)
            {
                projectile.alpha = 255;

                float dustR = 0;
                float dustG = 0;
                float dustB = 0;
                if (color == 1)
                {
                    dustR = 105;
                    dustG = 172;
                    dustB = 255;
                }
                else if (color == 2)
                {
                    dustR = 228;
                    dustG = 255;
                    dustB = 133;
                }
                else if (color == 3)
                {
                    dustR = 179;
                    dustG = 133;
                    dustB = 255;
                }
                else if (color == 4)
                {
                    dustR = 255;
                    dustG = 133;
                    dustB = 200;
                }
                else if (color == 5)
                {
                    dustR = 122;
                    dustG = 204;
                    dustB = 70;
                }
                dustR /= 255;
                dustG /= 255;
                dustB /= 255;

                float num = texSize * projectile.scale * 0.8f;
                int i = 0;
                while ((float)i < num)
                {
                    int num6 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), (int)(texSize * projectile.scale), (int)(texSize * projectile.scale), 176, 0, 0, 80, new Color(dustR, dustG, dustB), 1f);
                    Main.dust[num6].noGravity = true;
                    Main.dust[num6].alpha = 100;
                    Main.dust[num6].scale = projectile.scale;
                    i++;
                }
            }
        }

        public static void DrawProjectile(SpriteBatch spriteBatch, Texture2D texture, Projectile projectile, Color? overrideColor = null, float offsetX = 0f, float offsetY = 0f, bool flip = true)
        {
            int direction = flip ? projectile.direction : 1;
            offsetX += (-texture.Width * 0.5f);
            Color color = overrideColor != null ? (Color)overrideColor : projectile.GetAlpha(GetLightColor(projectile.Center));
            Vector2 origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            Vector2 offset = RotateVector(projectile.Center, projectile.Center + new Vector2(direction == -1 ? offsetX : offsetY, direction == 1 ? offsetX : offsetY), projectile.rotation - 2.355f) - projectile.Center;
            spriteBatch.Draw(texture,
                projectile.Center - Main.screenPosition + offset,
                new Rectangle(0, 0, texture.Width, texture.Height),
                color,
                projectile.rotation,
                origin,
                projectile.scale,
                direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0f);
        }
    }
}