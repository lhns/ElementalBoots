using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;

namespace LolHens.Projectiles
{
    public class SpaceRifleRay : LolHensProjectile
    {
        const float brightness = 0.4f;

        public override void Init()
        {
            projectile.hurtsTiles = false;
        }

        public override void AI()
        {
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;

            Lighting.AddLight((int)(projectile.Center.X / 16f), (int)(projectile.Center.Y / 16f), 1f * brightness, 0f * brightness, 0f * brightness);
        }
    }
}
