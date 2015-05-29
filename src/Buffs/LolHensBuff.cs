using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TAPI;
using Terraria;

namespace LolHens.Buffs
{
    public class LolHensBuff : ModBuff
    {
        public static CodableEntity lastTrigger = null;

        new public MBase modBase;
        public int type;
        public CodableEntity target;

        public CodableEntity trigger;
        public int time = 0;

        public sealed override void Start(Player player, int index) { Start(player as CodableEntity, index); }

        public sealed override void Start(NPC npc, int index) { Start(npc as CodableEntity, index); }

        public virtual void Start(CodableEntity target, int index)
        {
            modBase = base.modBase as MBase;

            type = GetBuffType();

            this.target = target;

            trigger = lastTrigger;
            lastTrigger = null;

            Init();

            if (!Main.dedServ) InitTextures();

            InitPost();
        }

        public virtual void Init() { }

        public virtual void InitTextures() { }

        public virtual void InitPost() { }

        public sealed override void Effects(Player player, int index) { Effects(player as CodableEntity, index); }

        public sealed override void Effects(NPC npc, int index) { Effects(npc as CodableEntity, index); }

        public virtual void Effects(CodableEntity entity, int index) { time++; }

        public sealed override void ReApply(Player player, int index) { ReApply(player as CodableEntity, index); }

        public sealed override void ReApply(NPC npc, int index) { ReApply(npc as CodableEntity, index); }

        public virtual void ReApply(CodableEntity entity, int index) { }

        private int GetBuffType()
        {
            foreach (KeyValuePair<int, BuffDef> buff in BuffDef.buffs)
                if (buff.Value.modBuffType == GetType()) return buff.Key;
            return -1;
        }

        public bool IsActive()
        {
            return target.HasBuff(type);
        }

        public int GetIndex()
        {
            return target.GetBuffIndex(type);
        }

        public int BuffTime
        {
            get
            {
                if (target is Player)
                {
                    Player player = target as Player;
                    return player.buffTime[GetIndex()];
                }
                else if (target is NPC)
                {
                    NPC npc = target as NPC;
                    return npc.buffTime[GetIndex()];
                }
                return 0;
            }
            set
            {
                if (target is Player)
                {
                    Player player = target as Player;
                    player.buffTime[GetIndex()] = value;
                }
                else if (target is NPC)
                {
                    NPC npc = target as NPC;
                    npc.buffTime[GetIndex()] = value;
                }
            }
        }
    }
}
