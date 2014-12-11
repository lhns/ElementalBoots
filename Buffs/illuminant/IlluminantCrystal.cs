using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;
using LolHens.Projectiles;

namespace LolHens.Buffs
{
    public class IlluminantCrystal : LolHensBuff
    {
        private int damage = 0;
        private float knockBack = 0;
        private LolHensProjectile illuminantCrystal = null;

        public override void Start(CodableEntity entity, int index)
        {
            base.Start(entity, index);
            Item item = trigger as Item;
            if (item != null)
            {
                damage = item.damage;
                knockBack = item.knockBack;
            }
            illuminantCrystal = ProjDef.byName["LolHens:IlluminantCrystal"].New(entity.Center.X, entity.Center.Y, 0, 0, damage, knockBack, entity.whoAmI);
        }
    }
}
