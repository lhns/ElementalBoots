﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolHens.Projectiles
{
    public class SpaceLaserRay : LolHensProjectile
    {
        public override void Init()
        {
            projectile.hurtsTiles = false;
        }

        public override void AI()
        {
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
        }
    }
}