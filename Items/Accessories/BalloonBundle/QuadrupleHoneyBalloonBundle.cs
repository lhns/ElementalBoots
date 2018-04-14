using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.BalloonBundle
{
    [AutoloadEquip(EquipType.Balloon)]
    class QuadrupleHoneyBalloonBundle : BalloonBundle
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            item.value = 6 * Value.GOLD;
            item.rare = 9;

            Cloud = Blizzard = Sandstorm = Tornado = true;
            Honey = true;
        }

        public override void AddRecipes()
        {
            upgradeOf = new int[] { mod.ItemType("QuadrupleBalloonBundle") };
            upgradeRequires = new int[] { ItemID.HoneyComb };

            base.AddRecipes();
        }
    }
}
