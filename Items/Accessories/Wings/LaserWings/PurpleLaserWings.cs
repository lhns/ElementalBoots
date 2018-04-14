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
    class PurpleLaserWings : LaserWings
    {
        protected override float lightIndex()
        {
            return 1;
        }

        public override void AddRecipes()
        {
            correspondingPhaseblade = ItemID.PurplePhaseblade;

            base.AddRecipes();
        }
    }
}
