using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Wings.LaserWings
{
    [AutoloadEquip(EquipType.Wings)]
    class YellowLaserWings : LaserWings
    {
        protected override float lightIndex()
        {
            return 4;
        }

        public override void AddRecipes()
        {
            correspondingPhaseblade = ItemID.YellowPhaseblade;

            base.AddRecipes();
        }
    }
}
