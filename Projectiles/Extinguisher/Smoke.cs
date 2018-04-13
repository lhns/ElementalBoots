using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace ElementalBoots.Projectiles.Extinguisher
{
    class Smoke: MProjectile
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            projectile.timeLeft = 100;
            projectile.friendly = true;
            projectile.hostile = false;
            projectile.tileCollide = true;
            projectile.damage = 1;
            projectile.magic = true;
        }

        public override bool? CanCutTiles()
        {
            return false;
        }
    }
}
