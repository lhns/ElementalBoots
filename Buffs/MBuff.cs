using System.Collections.Specialized;
using Terraria;
using Terraria.ModLoader;

namespace ElementalBoots.Buffs
{
    public abstract class MBuff: ModBuff, IEquipped
    {
        public bool Debuff
        {
            get { return Main.debuff[Type]; }
            set { Main.debuff[Type] = value; }
        }

        public override void Update(Player player, ref int buffIndex)
        {
            var mPlayer = player.GetModPlayer<MPlayer>(mod);

            mPlayer.UpdateEquipped(this);

            Update2((Entity) player, ref buffIndex);
        }

        public override void Update(NPC npc, ref int buffIndex)
        {
            Update2((Entity) npc, ref buffIndex);
        }

        public virtual void Update2(Entity entity, ref int buffIndex)
        {
        }

        public override bool ReApply(Player player, int time, int buffIndex)
        {
            return ReApply((Player) player, time, buffIndex);
        }

        public override bool ReApply(NPC npc, int time, int buffIndex)
        {
            return ReApply((Entity)npc, time, buffIndex);
        }

        public virtual bool ReApply(Entity entity, int time, int buffIndex)
        {
            return false;
        }

        public virtual void OnEquip(Player player)
        {
        }

        public virtual void OnUnEquip(Player player)
        {
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