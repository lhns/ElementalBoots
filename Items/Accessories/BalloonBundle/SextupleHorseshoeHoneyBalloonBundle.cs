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
    class SextupleHorseshoeHoneyBalloonBundle : BalloonBundle
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            item.value = 8 * Value.GOLD;
            item.rare = 9;

            Cloud = Blizzard = Sandstorm = Tornado = Fart = Sail = true;
            Horseshoe = true;
            Honey = true;
        }

        public override void AddRecipes()
        {
            upgradeOf = new int[] { mod.ItemType("SextupleHoneyBalloonBundle"), mod.ItemType("SextupleHorseshoeBalloonBundle") };
            upgradeRequires = new int[] { ItemID.LuckyHorseshoe, ItemID.HoneyComb };

            base.AddRecipes();
        }
    }
}
