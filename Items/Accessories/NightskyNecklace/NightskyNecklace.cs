using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.NightskyNecklace
{
    public class NightskyNecklace : CompoundAccessory
    {
        public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
        {
            //equips.Add(EquipType.Neck);

            return true;
        }

        public override void SetDefaults()
        {
            item.name = "Nightsky Necklace";
            item.maxStack = 1;
            item.value = 30*Value.GOLD;
            item.rare = 8;
            item.accessory = true;
            item.toolTip = "Causes stars to fall and increases length of invincibility after taking damage\nTurns the holder into a werewolf at the night and a merfolk when entering water";
        }

        public override IList<Item> GetCompoundAccessories()
        {
            return new List<Item>
            {
                Utils.GetItem(ItemID.MoonShell),
                Utils.GetItem(ItemID.StarVeil)
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