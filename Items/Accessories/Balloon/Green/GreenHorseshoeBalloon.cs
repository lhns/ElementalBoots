using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Balloon.Green
{
    class GreenHorseshoeBalloon: ModItem
    {
        public override void SetDefaults()
        {
            item.name = "Green Horseshoe Balloon2";
            item.maxStack = 1;
            item.value = 90 * Value.SILVER;
            item.rare = 4;
            item.accessory = true;
            item.toolTip = "Allows the holder to double jump\nNegates fall damage";
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.doubleJumpFart = true;
            player.jumpBoost = true;

            player.noFallDmg = true;
        }

        public override void AddRecipes()
        {
            /*ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, "ExampleItem");
            recipe.SetResult(this);
            recipe.AddRecipe();*/
        }
    }
}
