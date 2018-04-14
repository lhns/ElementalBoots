using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Ninja
{
    [AutoloadEquip(EquipType.HandsOn)]
    class MagmaClimbingGear: CompoundAccessory
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            
            item.value = 12 * Value.GOLD;
            item.rare = 8;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.FireGauntlet);
            recipe.AddIngredient(ItemID.TigerClimbingGear);
            recipe.AddTile(mod, "InventorsWorkshop");
            recipe.SetResult(this);
            recipe.AddRecipe();

            recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "MagmaClaws");
            recipe.AddIngredient(ItemID.ShoeSpikes);
            recipe.AddTile(mod, "InventorsWorkshop");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override IList<Item> GetCompoundAccessories()
        {
            return new List<Item>
            {
                ElementalBootsMod.instance.GetItemType(ItemID.FireGauntlet),
                ElementalBootsMod.instance.GetItemType(ItemID.TigerClimbingGear)
            };
        }
    }
}
