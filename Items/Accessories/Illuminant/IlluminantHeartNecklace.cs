using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using LibEventManagerCSharp;

namespace ElementalBoots.Items.Accessories.Illuminant
{
    class IlluminantHeartNecklace: Accessory
    {
        public override void SetDefaults()
        {
            base.SetDefaults();
            
            item.value = 8 * Value.GOLD;
            item.rare = 5;

            /*item.damage = 20;
            item.knockBack = 1;*/
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SweetheartNecklace);
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
                e.player.player.statLife = e.player.player.statLifeMax;
            });

            postHurtListener = Events.Registry().Register((Events.PlayerPostHurt e) =>
            {
                player.AddBuff(mod.GetBuff("IlluminantBuff").Type, 900, false);
                player.AddBuff(mod.GetBuff("IlluminantCrystal").Type, 300, false);
                player.AddBuff(BuffID.Panic, 600, false);
            });
        }
    }
}
