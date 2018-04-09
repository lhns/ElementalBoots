using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Weapon.SpikyBallBlaster
{
    class SpikyBall: GlobalItem
    {
        public override void SetDefaults(Item item)
        {
            base.SetDefaults(item);

            if (item.type == ItemID.SpikyBall)
            {
                item.ammo = ItemID.SpikyBall;
                item.shoot = ProjectileID.SpikyBall;
            }
        }
    }
}
