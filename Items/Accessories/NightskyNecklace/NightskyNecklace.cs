using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.NightskyNecklace
{
    //[AutoloadEquip(EquipType.Neck)]
    public class NightskyNecklace : CompoundAccessory
    {
        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.value = 30*Value.GOLD;
            item.rare = 8;
            item.accessory = true;
        }

        public override IList<Item> GetCompoundAccessories()
        {
            return new List<Item>
            {
                Main.item[ItemID.MoonShell],
                Main.item[ItemID.StarVeil]
            };
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.MoonShell);
            recipe.AddIngredient(ItemID.StarVeil);
            recipe.AddTile(mod, "InventorsWorkshop");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}