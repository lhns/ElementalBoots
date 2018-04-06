using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using LibEventManagerCSharp;

namespace ElementalBoots.Items.Accessories.Illuminant
{
    class IlluminantHeartNecklace: MItem
    {
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
                e.player.player.statLife = e.player.player.statLifeMax;
            });

            postHurtListener = Events.registry.Register((Events.PlayerPostHurt e) =>
            {
                player.AddBuff(mod.GetBuff("IlluminantBuff").Type, 900, false);
                player.AddBuff(mod.GetBuff("IlluminantCrystal").Type, 300, false);
                player.AddBuff(BuffID.Panic, 600, false);
            });
        }

        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.value = 8 * Value.GOLD;
            item.rare = 5;
            item.accessory = true;

            item.damage = 20;
            item.knockBack = 1;
        }
    }
}
