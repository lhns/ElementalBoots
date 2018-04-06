using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots.Items.Accessories
{
    abstract class CompoundAccessory : MItem
    {
        public abstract IList<Item> GetCompoundAccessories();

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            base.UpdateAccessory(player, hideVisual);

            foreach (var accessory in GetCompoundAccessories())
            {
                player.GetModPlayer<MPlayer>(mod).ApplyAccessoryEffects(accessory);
            }
        }
    }
}