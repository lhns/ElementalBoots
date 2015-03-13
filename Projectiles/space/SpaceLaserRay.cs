using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolHens.Projectiles
{
    public class SpaceLaserRay : LolHensProjectile
    {
        public int type;
        public float splitsLeft = 16;

        public override void Init()
        {
            projectile.hurtsTiles = false;
        }
    }
}
