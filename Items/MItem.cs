using Terraria;
using Terraria.ModLoader;
using LibEventManagerCSharp;

namespace ElementalBoots.Items
{
    abstract class MItem: ModItem, IEquipped
    {
        public bool Equipped { get; internal set; }

        private EventListener chestGeneratedEvent;

        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();

            chestGeneratedEvent = Events.Registry().Register((Events.ChestGenerated e) =>
            {
                OnChestGenerated(e.chestInfo);
            });
        }

        public virtual void OnChestGenerated(ChestInfo chestInfo) { }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            var mPlayer = player.GetModPlayer<MPlayer>(mod);

            mPlayer.UpdateEquipped(this);
        }

        public virtual void OnEquip(Player player)
        {
            Equipped = true;
        }

        public virtual void OnUnEquip(Player player)
        {
            Equipped = false;
        }

        private long _lastEquippedTime;

        public void SetLastEquippedTime(long time)
        {
            _lastEquippedTime = time;
        }

        public long GetLastEquippedTime()
        {
            return _lastEquippedTime;
        }
    }
}