using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace ElementalBoots.Projectiles.PoisonBubble
{
    class PoisonBubble : MProjectile
    {
        private int size;

        public override void SetDefaults()
        {
            base.SetDefaults();
            
            projectile.timeLeft = 2000;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.damage = 1;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.MaxUpdates = 1;

            size = RandomSize();
        }

        private int RandomSize()
        {
            if (Main.rand.Next(300) == 0)
            {
                return 64;
            }
            else if (Main.rand.Next(60) == 0)
            {
                return 48;
            }
            else if (Main.rand.Next(20) == 0)
            {
                return 32;
            }
            else if (Main.rand.Next(5) == 0)
            {
                return 20;
            }
            else
            {
                return 10;
            }
        }

        private Texture2D GetTexture()
        {
            return mod.GetTexture("Projectiles/PoisonBubble/PoisonBubble" + size);
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

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            DrawProjectile(spriteBatch, lightColor, GetTexture(), projectile, null, 0, 0, false);

            return false;
        }

        public static void DrawProjectile(SpriteBatch spriteBatch, Color lightColor, Texture2D texture, Projectile projectile, Color? overrideColor = null, float offsetX = 0f, float offsetY = 0f, bool flip = true)
        {
            var direction = flip ? projectile.direction : 1;
            offsetX += (-texture.Width * 0.5f);
            var color = overrideColor != null ? (Color)overrideColor : projectile.GetAlpha(lightColor);
            var origin = new Vector2((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
            var offset = projectile.Center.RotatedBy(projectile.rotation - 2.355f, projectile.Center + new Vector2(direction == -1 ? offsetX : offsetY, direction == 1 ? offsetX : offsetY)) - projectile.Center;
            var rect = new Rectangle(0, 0, texture.Width, texture.Height);
            var flipped = direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition + offset, rect, color, projectile.rotation, origin, projectile.scale, flipped, 0f);
        }

        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);

            if (Main.netMode == 2) return;

            projectile.alpha = 255;

            Color dustColor = new Color(122 / 255, 204 / 255, 70 / 255);

            for (int i = 0; i < size * projectile.scale * 0.8f; i++)
            {
                int num6 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), (int)(size * projectile.scale), (int)(size * projectile.scale), 176, 0, 0, 80, dustColor, 1f);
                Main.dust[num6].noGravity = true;
                Main.dust[num6].alpha = 100;
                Main.dust[num6].scale = projectile.scale;
                i++;
            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return true;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);

            target.AddBuff(BuffID.Poisoned, 300, false);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            base.OnHitPlayer(target, damage, crit);

            target.AddBuff(BuffID.Poisoned, 300, false);
        }
    }
}
