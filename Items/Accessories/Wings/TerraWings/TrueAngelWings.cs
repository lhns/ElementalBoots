using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Wings.TerraWings
{
    [AutoloadEquip(EquipType.Wings)]
    class TrueAngelWings: Wings
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            glowing = true;

            wingTimeMax = 140;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AngelWings);
            recipe.AddIngredient(mod, "BrokenHeroWings");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
