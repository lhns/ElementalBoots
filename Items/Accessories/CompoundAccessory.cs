using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories
{
    public abstract class CompoundAccessory : MItem
    {
        public abstract IList<Item> GetCompoundAccessories();

        public override void UpdateAccessory2(Player player, bool hideVisual)
        {
            base.UpdateAccessory2(player, hideVisual);

            foreach (var accessory in GetCompoundAccessories())
            {
                player.GetModPlayer<MPlayer>(mod).ApplyAccessoryEffects(accessory);
            }
        }
    }
}