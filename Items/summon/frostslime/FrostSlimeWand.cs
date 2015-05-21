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
        public override void InitType(Type type)
        {
            modBase.eventRegistry.Register((LolHensEvent.ChestGenerated e) => { if (e.chestInfo.height == ChestInfo.Height.CAVERN) e.chestInfo.AddLoot(item, 0.05f, true); });
        }

        public override bool? UseItem(Player player)
        {
            base.UseItem(player);

            player.AddPet(NPCDef.byName["LolHens:FrostSlime"]);
            return null;
        }
    }
}
