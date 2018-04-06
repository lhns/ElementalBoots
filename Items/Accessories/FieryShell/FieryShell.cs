using System.Collections.Generic;
using Terraria;
using Terraria.ID;

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