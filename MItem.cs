using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots
{
    public abstract class MItem: ModItem
    {
        internal long LastEquippedTime { get; private set; }

        public bool Equipped { get; internal set; }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var mPlayer = player.GetModPlayer<MPlayer>(mod);

            LastEquippedTime = mPlayer.Time;

            if (!mPlayer.EquippedItems.Contains(this))
            {
                mPlayer.EquippedItems.Add(this);
                OnEquip(player);
            }
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