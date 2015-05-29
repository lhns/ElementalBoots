using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolHens.Projectiles
{
    public class FrostProjectile: LolHensProjectile
    {
        public override void AI()
        {
            projectile.rotation = (float)System.Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + (float)(Math.PI / 4);
            projectile.velocity.Y += 0.1f;
        }
    }
}
