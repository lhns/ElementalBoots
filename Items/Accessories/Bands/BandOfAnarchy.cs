using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Bands
{
    class BandOfAnarchy : CompoundAccessory
    {
        public override IList<Item> GetCompoundAccessories()
        {
            return new List<Item>
            {
                ElementalBootsMod.instance.GetItemType("BandOfStamina"),
                ElementalBootsMod.instance.GetItemType(ItemID.AvengerEmblem),
                ElementalBootsMod.instance.GetItemType(ItemID.BandofStarpower),
                ElementalBootsMod.instance.GetItemType(ItemID.BandofRegeneration)
            };
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.AvengerEmblem);
            recipe.AddIngredient(ItemID.BandofRegeneration);
            recipe.AddIngredient(ItemID.BandofStarpower);
            recipe.AddIngredient(mod, "BandOfStamina");
            recipe.AddTile(mod, "InventorsWorkshop");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
