﻿using Terraria;
using Terraria.ID;

namespace ElementalBoots.Items.Accessories.FieryShell
{
    public class FieryShell : MItem
    {
        public override void SetDefaults()
        {
            item.maxStack = 1;
            item.value = 10*Value.GOLD;
            item.rare = 6;
            item.accessory = true;
        }

        public override void UpdateAccessory2(Player player, bool hideVisual)
        {
            base.UpdateAccessory2(player, hideVisual);

            player.accMerman = true;
            player.lavaMax += 720;

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