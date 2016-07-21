using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots.Items
{
    public abstract class MItem: ModItem
    {
        internal long LastEquippedTime { get; private set; }

        public bool Equipped { get; internal set; }

        public sealed override void UpdateAccessory(Player player, bool hideVisual)
        {
            var mPlayer = player.GetModPlayer<MPlayer>(mod);

            LastEquippedTime = mPlayer.Time;

            if (!mPlayer.EquippedItems.Contains(this))
            {
                mPlayer.EquippedItems.Add(this);
                OnEquip(player);
            }

            UpdateAccessory2(player, hideVisual);
        }

        public virtual void UpdateAccessory2(Player player, bool hideVisual)
        {
        }

        public virtual void OnEquip(Player player)
        {
            Equipped = true;
        }

        public virtual void OnUnEquip(Player player)
        {
            Equipped = false;
        }
    }
}