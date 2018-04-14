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
    class QuintupleBalloonBundle : BalloonBundle
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            item.value = 8 * Value.GOLD;
            item.rare = 9;

            Cloud = Blizzard = Sandstorm = Tornado = Fart = true;
        }

        public override void AddRecipes()
        {
            upgradeOf = new int[] { mod.ItemType("QuadrupleBalloonBundle") };
            upgradeRequires = new int[] { ItemID.FartInABalloon };

            base.AddRecipes();
        }
    }
}
