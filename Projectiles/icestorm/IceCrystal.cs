using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TAPI;
using Terraria;

namespace LolHens.Projectiles
{
    public class IceCrystal : LolHensProjectile
    {
        public int type;
        public float splitsLeft = 16;

        public override void Init()
        {
            projectile.hurtsTiles = false;

            type = Main.rand.Next(3);
            projectile.timeLeft = (int)((float)projectile.timeLeft * (0.5f + Main.rand.NextFloat() * 4));

            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }

        public override void InitTextures()
        {
            projectile.Texture = modBase.textures["Projectiles/icestorm/textures/IceCrystal" + type];
            projectile.width = projectile.Texture.Width;
            projectile.height = projectile.Texture.Height;
        }

        public override void AI()
        {
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            if (splitsLeft > 1 && Main.rand.Next(30) == 0)
            {
                float rotation = (Main.rand.NextFloat() - 0.5f) * 0.12f;

                Vector2 velocity = projectile.velocity.Rotate(rotation) * (Main.rand.NextFloat() * 0.5f + 0.75f);

                int projId = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, velocity.X, velocity.Y, projectile.type, projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);

                Projectile proj = Main.projectile[projId];
                if (proj != null && proj.active)
                {
                    proj.oldVelocity = new Vector2(projectile.velocity.X, projectile.velocity.Y);

                    IceCrystal crystal = proj.AsLolHensProjectile() as IceCrystal;
                    crystal.splitsLeft = splitsLeft / 2f;

                    Dust.NewDust(projectile.position, projectile.width, projectile.height, 13, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1.2f);
                }
                projectile.velocity = projectile.velocity.Rotate(-rotation);
                splitsLeft /= 2f;
            }
        }

        public override bool OnTileCollide(ref Vector2 velocityChange) { return true; }

        public override void Damage(CodableEntity entity, int hitDir, ref int damage, ref float knockback, ref bool crit, ref float critMult)
        {
            entity.AddBuff("Vanilla:Frostburn", 300, projectile, false);
            entity.AddBuff("Vanilla:Chilled", 100, projectile, false);
        }

        public override void PostKill()
        {
            Collision.HitTiles(projectile.position, projectile.velocity, projectile.width, projectile.height);
            //Main.PlaySound(13, (int)projectile.position.X, (int)projectile.position.Y, 1);
            for (int m = 0; m < 3; m++)
            {
                Dust.NewDust(projectile.position, projectile.width, projectile.height, 13, projectile.velocity.X * 0.2f, projectile.velocity.Y * 0.2f, 100, default(Color), 1.2f);
            }
        }
    }
}