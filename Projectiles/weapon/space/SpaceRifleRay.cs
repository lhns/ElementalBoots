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
        private int length = 0;
        public LolHensProjectile beam;

        public override void AI()
        {
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
            if (beam == null && length > 0)
            {
                beam = ProjDef.byName["LolHens:SpaceLaserRayBeam"].New(projectile.Center.X, projectile.Center.Y, projectile.velocity.X, projectile.velocity.Y, projectile.damage, projectile.knockBack, projectile.whoAmI);
                (beam as SpaceRifleRay).length = length - 1;
            }
        }
    }
}
