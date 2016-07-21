using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots
{
    class MPlayer: ModPlayer
    {
        internal ISet<MItem> EquippedItems = new HashSet<MItem>();

        public long Time { get; private set; }

        private void CheckUnEquippedItems()
        {
            var newEquippedItems = new HashSet<MItem>();

            foreach (var equippedItem in EquippedItems)
            {
                if (equippedItem.LastEquippedTime < Time)
                {
                    equippedItem.OnUnEquip(player);
                }
                else
                {
                    newEquippedItems.Add(equippedItem);
                }
            }

            EquippedItems = newEquippedItems;
        }

        public override void PreUpdate()
        {
            CheckUnEquippedItems();

            Time += 1;
        }

        public void ApplyAccessoryEffects(Item item)
        {
            player.VanillaUpdateEquip(item);
            bool flag1 = false, flag2 = false, flag3 = false;
            player.VanillaUpdateAccessory(item, true, ref flag1, ref flag2, ref flag3);
        }
    }
}
