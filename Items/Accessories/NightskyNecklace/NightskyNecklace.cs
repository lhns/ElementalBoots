using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.NightskyNecklace
{
    //[AutoloadEquip(EquipType.Neck)]
    class NightskyNecklace : CompoundAccessory
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            
            item.value = 30*Value.GOLD;
            item.rare = 8;
        }

        public override IList<Item> GetCompoundAccessories()
        {
            return new List<Item>
            {
                ElementalBootsMod.instance.GetItemType(ItemID.MoonShell),
                ElementalBootsMod.instance.GetItemType(ItemID.StarVeil)
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