using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using TAPI;
using Terraria;
using LolHens.Projectiles;
using LolHens.NPCs;

namespace LolHens.Buffs
{
    public class FrostSlimeBuff : LolHensPetBuff
    {
        public override void Start(CodableEntity entity, int index)
        {
            base.Start(entity, index);
            petNPC = NPCDef.byName["LolHens:FrostSlime"];
        }
    }
}
