using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.BalloonBundle
{
    [AutoloadEquip(EquipType.Balloon)]
    class QuadrupleHorseshoeHoneyBalloonBundle : BalloonBundle
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            item.value = 6 * Value.GOLD;
            item.rare = 9;

            Cloud = Blizzard = Sandstorm = Tornado = true;
            Horseshoe = true;
            Honey = true;
        }
    }
}
