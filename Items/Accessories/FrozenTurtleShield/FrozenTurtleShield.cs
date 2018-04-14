using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ElementalBoots;

namespace ElementalBoots.Items.Accessories.FrozenTurtleShield
{
    [AutoloadEquip(EquipType.Shield)]
    class FrozenTurtleShield: CompoundAccessory
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            
            item.value = 10 * Value.GOLD;
            item.rare = 8;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FrozenTurtleShell);
            recipe.AddIngredient(ItemID.PaladinsShield);
            recipe.AddTile(mod, "InventorsWorkshop");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override IList<Item> GetCompoundAccessories()
        {
            return new List<Item>
            {
                ElementalBootsMod.instance.GetItemType(ItemID.PaladinsShield),
                ElementalBootsMod.instance.GetItemType(ItemID.FrozenTurtleShell)
            };
        }
    }
}
