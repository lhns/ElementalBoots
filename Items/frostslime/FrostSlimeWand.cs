using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using TAPI;

namespace LolHens.Items
{
    public class FrostSlimeWand: LolHensItem
    {
        public override bool? UseItem(Player player)
        {
            base.UseItem(player);

            player.AddBuff(BuffDef.byName["LolHens:FrostSlime"], 600);
            return null;
        }
    }
}
