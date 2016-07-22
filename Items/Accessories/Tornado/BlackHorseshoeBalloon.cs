using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Tornado
{
    public class BlackHorseshoeBalloon : TornadoInABalloon
    {
        public override void SetDefaults()
        {
            item.name = "Black Horseshoe Balloon";
            item.maxStack = 1;
            item.value = 1*Value.GOLD;
            item.rare = 4;
            item.accessory = true;
            item.toolTip = "Allows the holder to double jump\nNegates fall damage";
        }

        public override void UpdateAccessory2(Player player, bool hideVisual)
        {
            base.UpdateAccessory2(player, hideVisual);

            player.noFallDmg = true;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "TornadoInABalloon");
            recipe.AddIngredient(ItemID.LuckyHorseshoe);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}