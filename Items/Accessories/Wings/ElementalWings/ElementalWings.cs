using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Wings.ElementalWings
{
    [AutoloadEquip(EquipType.Wings)]
    class ElementalWings: Wings
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            glowing = true;

            wingTimeMax = 600;

            constantAscend *= 4;
            ascentWhenRising *= 6;
            ascentWhenFalling *= 4;
            maxAscentSpeedMultiplier *= 3;
            maxHorizontalSpeedMultiplier *= 6;
            horizontalAccelerationMultiplier *= 4;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LeafWings);
            recipe.AddIngredient(ItemID.FrozenWings);
            recipe.AddIngredient(ItemID.FlameWings);
            recipe.AddIngredient(ItemID.GhostWings);
            recipe.AddIngredient(ItemID.SoulofFlight, 30);
            recipe.AddTile(mod, "InventorsWorkshop");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
