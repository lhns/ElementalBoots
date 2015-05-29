using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using TAPI;

namespace LolHens.Items
{
    public class BrokenFrostSlimeWandPart: LolHensItem
    {
        public override void InitType(Type type)
        {
            NPCDef.byName["Vanilla:King Slime"].AddDrop(item, 1);
        }
    }
}
