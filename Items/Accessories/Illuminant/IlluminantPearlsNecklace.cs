using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Illuminant
{
    class IlluminantPearlsNecklace : CompoundAccessory
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            
            item.value = 30 * Value.GOLD;
            item.rare = 8;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "IlluminantHeartNecklace");
            recipe.AddIngredient(mod, "IlluminantStarNecklace");
            recipe.AddIngredient(mod, "IlluminantPearl");
            recipe.AddTile(mod, "InventorsWorkshop");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override IList<Item> GetCompoundAccessories()
        {
            return new List<Item>
            {
                ElementalBootsMod.instance.GetItemType("IlluminantHeartNecklace"),
                ElementalBootsMod.instance.GetItemType("IlluminantStarNecklace")
            };
        }
    }
}
