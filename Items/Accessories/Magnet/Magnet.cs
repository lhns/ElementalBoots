using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories.Magnet
{
    class Magnet: Accessory
    {
        public override void SetDefaults()
        {
            base.SetDefaults();

            item.value = 1 * Value.GOLD;
            item.rare = 2;
        }

        public override void OnChestGenerated(ChestInfo chestInfo)
        {
            base.OnChestGenerated(chestInfo);

            if ((chestInfo.height == ChestInfo.Height.UNDERGROUND || chestInfo.height == ChestInfo.Height.CAVERN)
                    && (chestInfo.style == ChestInfo.Style.GOLD || chestInfo.style == ChestInfo.Style.GOLD_LOCKED))
                chestInfo.AddLoot(item, 0.05f, true);
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            Player.defaultItemGrabRange += 200;
        }
    }
}
