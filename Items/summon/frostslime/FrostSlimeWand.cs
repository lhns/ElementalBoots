using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using TAPI;
using LolHens.NPCs;

namespace LolHens.Items
{
    public class FrostSlimeWand : LolHensItem
    {
        public override bool? UseItem(Player player)
        {
            base.UseItem(player);

            player.AddPet(NPCDef.byName["LolHens:FrostSlime"]);
            return null;
        }
    }
}
