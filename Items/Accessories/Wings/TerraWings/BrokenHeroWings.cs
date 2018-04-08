using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Wings.TerraWings
{
    [AutoloadEquip(EquipType.Wings)]
    class BrokenHeroWings: Wings
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            wingTimeMax = 30;

            constantAscend *= 2;
            ascentWhenRising *= 2;
            ascentWhenFalling *= 2;
            maxAscentSpeedMultiplier *= 4;
            maxHorizontalSpeedMultiplier *= 4;
            horizontalAccelerationMultiplier *= 4;
        }
    }
}
