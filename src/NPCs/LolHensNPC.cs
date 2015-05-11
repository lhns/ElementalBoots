using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TAPI;
using Terraria;

namespace LolHens.NPCs
{
    public class LolHensNPC: ModNPC
    {
        new public LolHensBase modBase;

        public sealed override void Initialize()
        {
            modBase = base.modBase as LolHensBase;
            modBase.npcs.Add(this);
            Init();
            if (!Main.dedServ) InitTextures();
            InitPost();
        }

        public virtual void Init() { }

        public virtual void InitTextures() { }

        public virtual void InitPost() { }
    }
}
