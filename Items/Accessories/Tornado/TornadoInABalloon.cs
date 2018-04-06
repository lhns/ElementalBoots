using System;
using System.Collections.Generic;
using ElementalBoots;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Tornado
{
    [AutoloadEquip(EquipType.Balloon)]
    class TornadoInABalloon : CompoundAccessory
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            
            item.value = 1*Value.GOLD;
            item.rare = 4;
        }

        public override IList<Item> GetCompoundAccessories()
        {
            return new List<Item>
            {
                ElementalBootsMod.instance.GetItemType(ItemID.ShinyRedBalloon),
                ElementalBootsMod.instance.GetItemType("TornadoInABottle")
            };
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "TornadoInABottle");
            recipe.AddIngredient(ItemID.ShinyRedBalloon);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}