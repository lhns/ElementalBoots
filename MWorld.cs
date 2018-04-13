using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ElementalBoots
{
    class MWorld: ModWorld
    {
        public override void PostWorldGen()
        {
            ChestInfo.OnChestsGenerate();
        }
    }
}
