using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibEventManagerCSharp;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Illuminant
{
    class IlluminantStarNecklace: Accessory
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            item.value = 15 * Value.GOLD;
            item.rare = 6;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.StarVeil);
            recipe.AddIngredient(mod, "IlluminantPearl");
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        private EventListener respawnListener, postHurtListener;

        public override void OnUnEquip(Player player)
        {
            base.OnUnEquip(player);

            if (respawnListener != null) respawnListener.Unregister();
            if (postHurtListener != null) postHurtListener.Unregister();
        }

        public override void OnEquip(Player player)
        {
            base.OnEquip(player);

            if (respawnListener != null) respawnListener.Unregister();
            if (postHurtListener != null) postHurtListener.Unregister();

            respawnListener = Events.Registry().Register((Events.PlayerPostRespawn e) =>
            {
                e.player.player.statMana = e.player.player.statManaMax;
            });

            postHurtListener = Events.Registry().Register((Events.PlayerPostHurt e) =>
            {
                player.AddBuff(mod.GetBuff("IlluminantBuff").Type, 900, false);
            });
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            player.starCloak = true;
            player.longInvince = true;
        }
    }
}
