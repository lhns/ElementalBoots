using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.FieryShell
{
    class FieryShell : CompoundAccessory
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            
            item.value = 10 * Value.GOLD;
            item.rare = 6;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.NeptunesShell);
            recipe.AddIngredient(ItemID.LavaCharm);
            recipe.AddTile(mod, "InventorsWorkshop");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override IList<Item> GetCompoundAccessories()
        {
            return new List<Item>
            {
                ElementalBootsMod.instance.GetItemType(ItemID.NeptunesShell),
                ElementalBootsMod.instance.GetItemType(ItemID.LavaCharm)
            };
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);
            
            player.lavaMax += 300;

            if (player.lavaWet)
            {
                player.releaseJump = true;
                player.wings = 0;
                player.merman = true;
                player.accFlipper = true;
                player.AddBuff(BuffID.Merfolk, 2, true);
            }
        }
    }
}