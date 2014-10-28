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

        new public LolHensBase modBase;
        public CodableEntity entity;
        public CodableEntity trigger;
        public int time = 0;

        public sealed override void Start(Player player, int index) { Start(player as CodableEntity, index); }

        public sealed override void Start(NPC npc, int index) { Start(npc as CodableEntity, index); }

        public virtual void Start(CodableEntity entity, int index)
        {
            modBase = base.modBase as LolHensBase;
            this.entity = entity;
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
    }
}
