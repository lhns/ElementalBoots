using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibEventManagerCSharp;
using Terraria;
using Terraria.ID;

namespace ElementalBoots.Items.Accessories.Illuminant
{
    class IlluminantStarNecklace: MItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.value = 15 * Value.GOLD;
            item.rare = 6;
            item.accessory = true;
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

            respawnListener = Events.registry.Register((Events.PlayerPostRespawn e) =>
            {
                e.player.player.statMana = e.player.player.statManaMax;
            });

            postHurtListener = Events.registry.Register((Events.PlayerPostHurt e) =>
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
