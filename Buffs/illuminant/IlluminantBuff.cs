using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;
using LolHens.Items;

namespace LolHens.Buffs
{
    public class IlluminantBuff : LolHensBuff
    {
        public override void Start(CodableEntity entity, int index)
        {
            base.Start(entity, index);
            //ShootCrystal();
        }

        private void ShootCrystal()
        {
            Item item = trigger as Item;
            if (item != null) Projectile.NewProjectile(entity.Center.X, entity.Center.Y, 0, 0, ProjDef.byName["LolHens:IlluminantCrystal"].type, item.damage, item.knockBack, entity.whoAmI);
        }

        public override void Effects(CodableEntity entity, int index)
        {
            base.Effects(entity, index);
            Lighting.AddLight((int)(entity.Center.X / 16f), (int)(entity.Center.Y / 16f), 0.9f, 0.5f, 0.95f);
        }
    }
}