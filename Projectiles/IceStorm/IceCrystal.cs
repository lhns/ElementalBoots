using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ElementalBoots.Projectiles.IceStorm
{
    class IceCrystal: MProjectile
    {
        private int type;
        private float splitsLeft = 16;

        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.timeLeft = 200;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.damage = 5;
            projectile.magic = true;
            projectile.penetrate = 100;
            projectile.MaxUpdates = 4;

            type = Main.rand.Next(3);
        }

        public override bool? CanCutTiles()
        {
            return false;
        }

        private Texture2D GetTexture()
        {
            return mod.GetTexture("Projectiles/IceStorm/IceCrystal" + type);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            var texture = GetTexture();
            var rect = new Rectangle(0, 0, texture.Width, texture.Height);
            spriteBatch.Draw(texture, projectile.Center - Main.screenPosition, rect, projectile.GetAlpha(lightColor), projectile.rotation, rect.Size() / 2, projectile.scale, SpriteEffects.None, 0f);

            return false;
        }

        public override void AI()
        {
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            if (splitsLeft > 1 && Main.rand.Next(30) == 0)
            {
                float rotation = (Main.rand.NextFloat() - 0.5f) * 0.12f;

                Vector2 velocity = projectile.velocity.RotatedBy(rotation) * (Main.rand.NextFloat() * 0.5f + 0.75f);

                int projId = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, velocity.X, velocity.Y, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);

                Projectile proj = Main.projectile[projId];
                if (proj != null && proj.active)
                {
                    proj.oldVelocity = new Vector2(projectile.velocity.X, projectile.velocity.Y);

                    IceCrystal crystal = proj.modProjectile as IceCrystal;
                    crystal.splitsLeft = splitsLeft / 2f;

                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 13, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1.2f);
                }
                projectile.velocity = projectile.velocity.RotatedBy(-rotation);
                splitsLeft /= 2f;
            }

            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 0.44f * 0.5f, 0.92f * 0.5f, 1f * 0.5f);
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            return true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            base.OnHitNPC(target, damage, knockback, crit);

            target.AddBuff(BuffID.Frostburn, 300, false);
            target.AddBuff(BuffID.Chilled, 100, false);
        }

        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            base.OnHitPlayer(target, damage, crit);

            target.AddBuff(BuffID.Frostburn, 300, false);
            target.AddBuff(BuffID.Chilled, 100, false);
        }

        public override void Kill(int timeLeft)
        {
            base.Kill(timeLeft);

            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);

            for (int m = 0; m < 3; m++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 13, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1.2f);
            }
        }
    }
}
