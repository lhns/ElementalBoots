using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using LibEventManagerCSharp;
using Microsoft.Xna.Framework;

namespace ElementalBoots.Items.Accessories.Bands
{
    class BandOfStamina: Accessory
    {
        private EventListener postHurtListener;

        public override void SetDefaults()
        {
            base.SetDefaults();
        }

        public override void OnChestGenerated(ChestInfo chestInfo)
        {
            base.OnChestGenerated(chestInfo);

            if (chestInfo.height == ChestInfo.Height.SKY)
                chestInfo.AddLoot(item, 0.1f, true);
        }

        public override void OnUnEquip(Player player)
        {
            base.OnUnEquip(player);
            
            if (postHurtListener != null) postHurtListener.Unregister();
        }

        private int time = 0;

        public override void OnEquip(Player player)
        {
            base.OnEquip(player);
            
            if (postHurtListener != null) postHurtListener.Unregister();
            
            postHurtListener = Events.Registry().Register((Events.PlayerPostHurt e) =>
            {
                time = 0;
            });
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            time++;

            var seconds = time / 45;

            int chance = 0;

            if (seconds >= 20)
            {
                chance = 15;
            }
            else if (seconds >= 10)
            {
                chance = 5;
            }
            else if (seconds >= 3)
            {
                chance = 2;
            }
            else if (seconds >= 1)
            {
                chance = 1;
            }

            player.meleeCrit += chance;
            player.rangedCrit += chance;
            player.magicCrit += chance;
        }
    }
}
