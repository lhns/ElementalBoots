using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TAPI;
using Terraria;

namespace LolHens.Buffs
{
    class IlluminantCrystal : LolHensBuff
    {
        public override void Start(CodableEntity entity, int index)
        {
            base.Start(entity, index);
            ShootCrystal();
        }

        private void ShootCrystal()
        {
            Item item = trigger as Item;
            if (item != null) Projectile.NewProjectile(entity.Center.X, entity.Center.Y, 0, 0, ProjDef.byName["LolHens:IlluminantCrystal"].type, item.damage, item.knockBack, entity.whoAmI);
        }

    }
}
