using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LolHens.Projectiles
{
    public class Smoke : LolHensProjectile
    {
        public override void Init()
        {
            base.Init();
            projectile.hurtsTiles = false;
        }
    }
}
