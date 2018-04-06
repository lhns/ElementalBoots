using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ElementalBoots.Items;
using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots
{
    class MPlayer : ModPlayer
    {
        public long Time { get; private set; }

        private ISet<IEquipped> _equippedItems = new HashSet<IEquipped>();
        
        public void UpdateEquipped(IEquipped equipped)
        {
            equipped.SetLastEquippedTime(Time);

            if (!_equippedItems.Contains(equipped))
            {
                _equippedItems.Add(equipped);

                equipped.OnEquip(player);
            }
        }

        private void UpdateUnEquipped()
        {
            var newEquippedItems = new HashSet<IEquipped>();

            foreach (var equipped in _equippedItems)
            {
                if (equipped.GetLastEquippedTime() < Time)
                {
                    equipped.OnUnEquip(player);
                }
                else
                {
                    newEquippedItems.Add(equipped);
                }
            }

            _equippedItems = newEquippedItems;
        }

        public override void PreUpdate()
        {
            UpdateUnEquipped();

            Time += 1;
        }

        private int id = -1;

        public int GetID()
        {
            if (id > -1) return id;
            for (var i = 0; i < Main.player.Length; i++)
            {
                if (Main.player[i] == player)
                {
                    id = i;
                    return i;
                }
            }
            throw new Exception("Player not found!");
        }

        public void ApplyAccessoryEffects(Item item, bool hideVisual = false)
        {
            player.VanillaUpdateEquip(item);
            bool flag1 = false, flag2 = false, flag3 = false;
            player.VanillaUpdateAccessory(GetID(), item, hideVisual, ref flag1, ref flag2, ref flag3);
        }
    }
}
